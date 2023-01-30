using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovementScript : MonoBehaviour
{
    private bool isAlive;
    public int jumpStrenght;
    public float speed;
    public float currentSpeed;
    public float deceleration;
    private int direction;
    private bool isWalking;
    private Vector2 inputVector;
    private Rigidbody2D rigidbody2D;
    
    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        isWalking = false;
        currentSpeed = 0;
        direction = 1;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            transform.localScale = new Vector3(transform.localScale.x * direction, transform.localScale.y, 0);
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(isAlive && context.performed)
        {
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
            transform.localScale = new Vector3(transform.localScale.x + growRate, transform.localScale.y + growRate, 0);
            IncreaseJumpStrengh((int)growRate);
        }
    }

    private void IncreaseJumpStrengh(int increaseRate)
    {
        jumpStrenght += increaseRate;
    }

    private float CalculateSpeed()
    {
        if (rigidbody2D.velocity.x < 0)
        {
            direction = -1;
            return Mathf.Clamp(rigidbody2D.velocity.x + deceleration, speed * -10, 0);
        }
        else
        {
            return Mathf.Clamp(rigidbody2D.velocity.x - deceleration, 0, speed * 10);
        }
    }
}