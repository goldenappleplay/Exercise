using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizzyMovement : MonoBehaviour
{
    [SerializeField] private float speed, jumpHeight;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private Animator animator;
    internal LayerMask groundLayer;
    private float direction;
    private float x_Axis, y_Axis;
    private bool isJumpPressed, isGrounded;


    const string Player_Idle = "Idle_L";
    const string Player_Run = "Run_L";
    const string Player_Jump = "Jump";
    const string Player_Fall = "Falling";
    const string Player_Land = "Landing";
    const string Player_Stop = "Stopping";


    private string currentState;

    // Start is called before the first frame update
    void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        x_Axis = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumpPressed = true;
        }
        Debug.Log(isGrounded);
       
    }

    private void FixedUpdate()
    {
        
        CheckIfIsGrounded();
        
        
        if (x_Axis > 0)
        {
            rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        else if (x_Axis < 0)
        {
            rigidBody.velocity = new Vector2(-speed, rigidBody.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
               

        if (isJumpPressed == true && isGrounded)
        {
            rigidBody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            isJumpPressed = false;
            ChangeAnimationState(Player_Jump);
        }

        if (true)
        {

        }

    }

    private void LateUpdate()
    {
        if (isGrounded == true)
        {
            if (x_Axis != 0)
            {
                ChangeAnimationState(Player_Run);
            }
            else
            {
                ChangeAnimationState(Player_Idle);
            }
        }       
    }

    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState)
        {
            return;
        }
        animator.Play(newState);
        currentState = newState;
    }

    internal bool CheckIfIsGrounded()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, 0.2f, groundLayer);
        isGrounded = rayCastHit.collider;
        return isGrounded;
    }
}
