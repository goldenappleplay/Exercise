using System;
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


    //private int Player_Idle = Animator.StringToHash("Idle_L");
    //private int Player_Run = Animator.StringToHash("Run_L");
    //private int Player_Jump = Animator.StringToHash("Jump");
    //private int Player_Fall = Animator.StringToHash("Falling");
    //private int Player_Land = Animator.StringToHash("Landing");
    //private int Player_Stop = Animator.StringToHash("Stopping");

    //private Vector2 playerVelocity;
    private bool playJump_Anim = false, moving = false;
    internal LizzyStates state = LizzyStates.idle;


    // Start is called before the first frame update
    void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        x_Axis = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumpPressed = true;
        }

        Debug.Log(state);
    }

    private void FixedUpdate()
    {
        CheckIfIsGrounded();


        if (x_Axis < 0)
        {
            rigidBody.velocity = new Vector2(-speed, rigidBody.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        else if (x_Axis > 0)
        {
            rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }


        if (isJumpPressed == true)
        {
            rigidBody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);

            isJumpPressed = false;
        }
    }

    private void LateUpdate()
    {
        ChangeAnimationState();
        animator.SetInteger("state", (int)state);
    }

    private void ChangeAnimationState()
    {
        //if (rigidBody.velocity.y > 0.5f && isGrounded != true)
        //{

        //}

        if (isGrounded == true)
        {

            if (rigidBody.velocity.y > 1f)
            {
                state = LizzyStates.jumping;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Falling") && state == LizzyStates.falling && playerCollider.IsTouchingLayers(groundLayer))
            {
                //if (rigidBody.velocity.y == 0)
                //{
                state = LizzyStates.landing;
                //}               
            }
            else if ((x_Axis != 0 && animator.GetCurrentAnimatorStateInfo(0).IsName("Landing")) || (x_Axis != 0 && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle_L")) || (x_Axis != 0 && animator.GetCurrentAnimatorStateInfo(0).IsName("Stopping")))
            {
                state = LizzyStates.running;
            }
            else if (x_Axis == 0 && Math.Abs(rigidBody.velocity.x) < 3f && Math.Abs(rigidBody.velocity.x) > 1f && animator.GetCurrentAnimatorStateInfo(0).IsName("Run_L"))
            {
                state = LizzyStates.stopping;
            }
            else
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Landing") || animator.GetCurrentAnimatorStateInfo(0).IsName("Stopping"))
                {
                    state = LizzyStates.idle;
                }               
            }
        }

        if (rigidBody.velocity.y < -2f && animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") || rigidBody.velocity.y < -2f && animator.GetCurrentAnimatorStateInfo(0).IsName("Run_L"))
        {
            state = LizzyStates.falling;
        }
        
    }

    internal bool CheckIfIsGrounded()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
        isGrounded = rayCastHit.collider;
        return isGrounded;
    }
}
