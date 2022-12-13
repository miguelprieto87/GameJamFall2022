using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{

    [Header("Slider")]
    [SerializeField] private SliderController slider;

    [Header("Stats")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private float currentJumpHeight;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float currentMovementSpeed;

    private float horizontal;
    [HideInInspector]
    public bool isFacingRight = true;
    private bool grounded;
    private float jumpDelay;

    [Header("Components")]
    [SerializeField] private Rigidbody myRB;

    void Start()
    {
        currentMovementSpeed = movementSpeed;
        currentJumpHeight = jumpHeight;
        myRB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (grounded)
        {
            myRB.velocity =  new Vector2(horizontal * currentMovementSpeed, myRB.velocity.y);

        }
        else
        {
            myRB.AddForce(new Vector3(horizontal * currentMovementSpeed, 0, 0));

        }
        if (!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if(isFacingRight && horizontal < 0f)
        {
            Flip();
        }
    }

    public void MovePlayer(InputAction.CallbackContext context)
    { 
        horizontal = context.ReadValue<Vector2>().x;

    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!grounded || jumpDelay > Time.time) return;
        slider.IncreaseSlider();
        myRB.AddForce(transform.up * currentJumpHeight, ForceMode.Impulse);
        jumpDelay = Time.time + .5f;
        print("jumping");
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Ground"))
        {
            grounded = true;
            jumpDelay = Time.time + .05f;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Ground"))
        {
            grounded = false;

        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void UpdateStats(float modifier)
    {
        currentJumpHeight = jumpHeight * modifier;
        currentMovementSpeed = movementSpeed + modifier;
    }
}
