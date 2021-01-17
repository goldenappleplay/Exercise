using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizzyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;

    private float direction;
    private float x_Axis, y_Axis;
    private bool isJumpPressed;


    const string Player_Idle = "Idle_L";
    const string Player_Run = "Run_L";
    const string Player_Jump = "Jumo";
    const string Player_Fall = "Falling";
    const string Player_Land = "Landing";
    const string Player_Stop = "Stopping";


    private string currentState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x_Axis = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumpPressed = true;
        }

       
    }

    private void FixedUpdate()
    {
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
    }

    private void LateUpdate()
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

    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState)
        {
            return;
        }
        animator.Play(newState);
        currentState = newState;
    }
}
