using System.Collections.Generic;
using System.Linq;
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
    private bool isGrounded;
    private Vector2 inputVector;
    private Vector3 characterSize;
    private Rigidbody2D rigidbody2D;
    private CharacterAudioScript characterBehaviorScript;
    private CharacterAnimationScript characterAnimationScript;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        characterBehaviorScript = GetComponent<CharacterAudioScript>();
        characterAnimationScript = GetComponent<CharacterAnimationScript>();

        isAlive = true;
        isWalking = false;
        isGrounded = false;
        currentSpeed = 0;
        direction = 1;
        growthSpeed = 4;
        size = 1;
        timeCounter = 0.1f;
        characterSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        List<Collider2D> colliders = new List<Collider2D>();
        rigidbody2D.GetContacts(colliders);

        timeCounter -= Time.deltaTime;

        if (colliders.Any(c => c.tag == "Floor") && timeCounter <= 0)
        {
            isGrounded = true;
            characterAnimationScript.StopJumpAnimation();
            characterBehaviorScript.PlayFallAudio();
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
            characterBehaviorScript.CanPlayFall();
            characterAnimationScript.PlayJumpAnimation();
            timeCounter = 0.1f;
            characterBehaviorScript.PlayJumpAudio();
            rigidbody2D.AddForce(Vector2.up * jumpStrenght, ForceMode2D.Impulse);
        }
    }

    public void Walk(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inputVector = context.ReadValue<Vector2>();
            isWalking = true;
            characterAnimationScript.PlayWalkAnimation();
        }

        if(context.canceled)
        {
            isWalking = false;
            characterAnimationScript.StopWalkAnimation();
        }
    }

    public void GrowUp(float growRate)
    {
        if (isAlive)
        {
            characterBehaviorScript.PlayGrowAudio();
            characterSize = new Vector3(Mathf.Abs(transform.localScale.x) + growRate, transform.localScale.y + growRate, 0);
            size ++;

            IncreaseJumpStrengh((int)growRate);
        }
    }

    public void ShrinkDown(float growRate)
    {
        if (isAlive)
        {
            characterBehaviorScript.PlayShrinkAudio();


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