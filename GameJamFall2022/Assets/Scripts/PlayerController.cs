using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider), typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    public AudioClip jumpAudio;
    private AudioSource audioSource;
    private bool isWalking = false;

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
    private bool isDead = false;

    [Header("Components")]
    [SerializeField] private Rigidbody myRB;

    void Start()
    {
        isDead = false;
        currentMovementSpeed = movementSpeed;
        currentJumpHeight = jumpHeight;
        myRB = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (grounded)
        {
            myRB.velocity = new Vector2(horizontal * currentMovementSpeed, myRB.velocity.y);
            if (horizontal != 0 && !isWalking)
            {
                isWalking = true;
                StartCoroutine(PlayWalkSound());
            }
            else if (horizontal == 0)
            {
                isWalking = false;
                StopAllCoroutines();
            }
        }
        else
        {
            myRB.AddForce(new Vector3(horizontal * currentMovementSpeed, 0, 0));
            isWalking = false;
            StopAllCoroutines();
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

    private IEnumerator PlayWalkSound()
    {
        while (true)
        {
            audioSource.Play();
            yield return new WaitForSeconds(0.6f);
        }
    }

    public void MovePlayer(InputAction.CallbackContext context)
    { 
        if (isDead) return;
        horizontal = context.ReadValue<Vector2>().x;
        

    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!grounded || jumpDelay > Time.time || isDead) return;
        slider.IncreaseSlider();
        myRB.AddForce(transform.up * currentJumpHeight, ForceMode.Impulse);
        jumpDelay = Time.time + .5f;
        audioSource.PlayOneShot(jumpAudio);
        print("jumping");
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Ground"))
        {
            grounded = true;
            jumpDelay = Time.time + .05f;
        }

        if (collision.CompareTag("Shredder"))
        {
            Die();
        }

        if (collision.CompareTag("End"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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

    private void Die()
    {
        isDead = true;
        FindObjectOfType<MenuController>().index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("DeathScene");

    }
}
