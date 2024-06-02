using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Constants
    private const string IS_WALKING = "IsWalking";
    private const string IS_RUNNING = "IsRunning";

    // Variables
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    private float speed = 2.0f;
    private float walkSpeed = 2.0f;
    private float runSpeed = 4.0f;
    private bool isMoving = false;
    private bool isWalking = false;
    private bool isRunning = false;
    private Vector2 moveDir = Vector2.zero;


    // Start is called before the first frame update
    void Start() { }


    // Update is called once per frame
    void Update()
    {
        HandleMovement();   
    }

    private void HandleMovement()
    {
        moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxis("Vertical"));
        moveDir = moveDir.normalized;
        transform.position += new Vector3(moveDir.x, moveDir.y, 0) * speed * Time.deltaTime;

        isMoving = moveDir != Vector2.zero;

        if (isMoving)
        {
            if (speed == walkSpeed)
            {
                isWalking = true;
            }
            else if (speed == runSpeed)
            {
                isRunning = true;
            }
        }

        if (isMoving && Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        else if (isMoving && !Input.GetKey(KeyCode.LeftShift))
        {
            speed = walkSpeed;
        }

        if (isMoving)
        {
            if (moveDir.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            animator.SetBool(IS_WALKING, false);
            animator.SetBool(IS_RUNNING, false);
        }

        if (isMoving && speed == walkSpeed)
        {
            animator.SetBool(IS_WALKING, true);
            animator.SetBool(IS_RUNNING, false);
        }
        else if (isMoving && speed == runSpeed)
        {
            animator.SetBool(IS_WALKING, false);
            animator.SetBool(IS_RUNNING, true);
        }
    }
}
