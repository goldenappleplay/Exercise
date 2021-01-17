using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;

    private Vector2 moveDirection;
    private Vector2 lastMoveDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        Animate();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ProcessInput()
    {
        float move_X = Input.GetAxisRaw("Horizontal");
        float move_Y = Input.GetAxisRaw("Vertical");

        if ((move_X == 0 && move_Y == 0) && moveDirection.x != 0 || moveDirection.y != 0)
        {
            lastMoveDirection = moveDirection;
        }

        moveDirection = new Vector2(move_X, move_Y).normalized;
    }

    private void Move()
    {
        rigidBody.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private void Animate()
    {
        animator.SetFloat("AnimMoveX", moveDirection.x);
        animator.SetFloat("AnimMoveY", moveDirection.y);
        animator.SetFloat("AnimMoveMagnitude", moveDirection.magnitude);

        animator.SetFloat("AnimLastMoveX", lastMoveDirection.x);
        animator.SetFloat("AnimLastMoveY", lastMoveDirection.y);

    }
}
