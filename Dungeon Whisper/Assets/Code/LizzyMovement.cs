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


    private int Player_Idle = Animator.StringToHash("Idle_L");
    private int Player_Run = Animator.StringToHash("Run_L");
    private int Player_Jump = Animator.StringToHash("Jump");
    private int Player_Fall = Animator.StringToHash("Falling");
    private int Player_Land = Animator.StringToHash("Landing");
    private int Player_Stop = Animator.StringToHash("Stopping");


    private int currentState;
    private bool playJump_Anim = false, moving = false;
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
        Debug.Log(isGrounded + " - jump = " + isJumpPressed);
       
    }

    private void FixedUpdate()
    {
        
        CheckIfIsGrounded();
        
        
        if (x_Axis > 0)
        {
            rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            //moving = true;
        }
        else if (x_Axis < 0)
        {
            rigidBody.velocity = new Vector2(-speed, rigidBody.velocity.y);
            transform.localScale = new Vector2(1, 1);
            //moving = true;
        }


        if (isJumpPressed == true && isGrounded)
        {
            rigidBody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            isJumpPressed = false;
            playJump_Anim = true;
        }


        //if (rigidBody.velocity.x < 0.5f && isGrounded && x_Axis == 0)
        //{
        //    ChangeAnimationState(Player_Stop);
        //}

    }

    private void LateUpdate()
    {
        if (playJump_Anim == true)
        {
            ChangeAnimationState(Player_Jump);
            playJump_Anim = false;
        }


        else if (x_Axis != 0 && isGrounded)
        {
            ChangeAnimationState(Player_Run);
        }

        else if (isGrounded && rigidBody.velocity.x < 1f)
        {
            ChangeAnimationState(Player_Idle);
            //moving = false;
        }
        
    }

    private void ChangeAnimationState(int newState)
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
        RaycastHit2D rayCastHit = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
        isGrounded = rayCastHit.collider;
        return isGrounded;
    }
}
