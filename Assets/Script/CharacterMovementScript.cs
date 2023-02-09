using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovementScript : MonoBehaviour
{
    private bool isAlive;
    public int jumpStrenght;
    public float speed;
    public float currentSpeed;
    public float deceleration;
    private float timeCounter;
    private int direction;
    private int growthSpeed;
    private int size;
    private bool isWalking;
    private bool isJumping;
    private bool isGrounded;
    private bool fallPlayed;
    private Vector2 inputVector;
    private Vector3 characterSize;
    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip grownAudio;
    [SerializeField]
    private AudioClip shrinkAudio;
    [SerializeField]
    private AudioClip jumpAudio;
    [SerializeField]
    private AudioClip fallAudio;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        isWalking = false;
        isGrounded = false;
        isJumping = false;
        fallPlayed = false;
        currentSpeed = 0;
        direction = 1;
        growthSpeed = 4;
        size = 1;
        timeCounter = 0.1f;
        characterSize = transform.localScale;

        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        List<Collider2D> colliders = new List<Collider2D>();
        rigidbody2D.GetContacts(colliders);

        animator.SetBool("isWalking", isWalking);
        timeCounter -= Time.deltaTime;

        if (colliders.Any(c => c.tag == "Floor") && timeCounter <= 0)
        {
            isJumping = false;
            isGrounded = true;
            animator.SetBool("isJumping", false);
            
            if (!fallPlayed)
            {
                if (!audioSource.isPlaying || audioSource.clip.name != "Yoi")
                {
                    audioSource.clip = fallAudio;
                    audioSource.Play();
                }
                fallPlayed = true;
            }
        }
        else if(isJumping)
        {
            //isGrounded = false;
            //fallPlayed = false;
            //animator.SetBool("isJumping", true);
        }

        if (characterSize.x != transform.localScale.x && characterSize.y != transform.localScale.y)
        {
            var x = Mathf.MoveTowards(Mathf.Abs(transform.localScale.x), Mathf.Abs(characterSize.x), growthSpeed * Time.deltaTime) * direction;
            var y = Mathf.MoveTowards(transform.localScale.y, characterSize.y, growthSpeed * Time.deltaTime);

            transform.localScale = new Vector3(x, y, 0);
        }
    }

    private void FixedUpdate()
    {
        if(isAlive && isWalking)
        {
            rigidbody2D.velocity = new Vector2(inputVector.x * speed, rigidbody2D.velocity.y);
        }

        currentSpeed = CalculateSpeed();

        rigidbody2D.velocity = new Vector2(currentSpeed, rigidbody2D.velocity.y);

        if(currentSpeed != 0)
        {
            if (transform.localScale.x >= 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * direction, transform.localScale.y, 0);
            }
            else
            {
                if (direction < 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * 1, transform.localScale.y, 0);
                }

                if (direction > 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 0);
                }
            }
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(isAlive && context.performed && isGrounded)
        {
            isGrounded = false;
            fallPlayed = false;
            animator.SetBool("isJumping", true);
            isJumping = true;
            timeCounter = 0.1f;
            audioSource.clip = jumpAudio;
            audioSource.Play();
            rigidbody2D.AddForce(Vector2.up * jumpStrenght, ForceMode2D.Impulse);
        }
    }

    public void Walk(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inputVector = context.ReadValue<Vector2>();
            isWalking = true;
        }

        if(context.canceled)
        {
            isWalking = false;
        }
    }

    public void GrowUp(float growRate)
    {
        if (isAlive)
        {
            audioSource.clip = grownAudio;
            audioSource.Play();
            characterSize = new Vector3(Mathf.Abs(transform.localScale.x) + growRate, transform.localScale.y + growRate, 0);
            size ++;

            IncreaseJumpStrengh((int)growRate);
        }
    }

    public void ShrinkDown(float growRate)
    {
        if (isAlive)
        {
            audioSource.clip = shrinkAudio;
            audioSource.Play();

            
            characterSize = new Vector3(Mathf.Abs(transform.localScale.x) + growRate, Mathf.Abs(transform.localScale.y + growRate), 0);
            size--;

            IncreaseJumpStrengh((int)growRate);
        }
    }

    public void StopMoving()
    {
        isAlive = false;
    }

    public int GetSize()
    {
        return size;
    }

    private void IncreaseJumpStrengh(int increaseRate)
    {
        if (increaseRate > 0)
        {
            jumpStrenght += 10;
        }
        else
        {
            jumpStrenght -= 10;
        }
    }

    private float CalculateSpeed()
    {
        if (rigidbody2D.velocity.x < 0)
        {
            direction = -1;
            return Mathf.Clamp(rigidbody2D.velocity.x + deceleration, speed * -10, 0);
        }

        if(rigidbody2D.velocity.x > 0)
        {
            direction = 1;
            return Mathf.Clamp(rigidbody2D.velocity.x - deceleration, 0, speed * 10);
        }

        return 0;
    }
}