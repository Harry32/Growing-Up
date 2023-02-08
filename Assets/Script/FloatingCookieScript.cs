using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class FloatingCookieScript : MonoBehaviour
{
    private float deltaTime;
    private float senoidalFrequency;
    private float senoidalAmplitude;
    private float sinkSpeed;
    private float recoverySpeed;
    private float timeCounter;
    private bool floating;
    private bool sinking;
    private bool recovering;
    private Vector3 positionOffset;
    private CharacterMovementScript characterMovementScript;

    // Start is called before the first frame update
    void Start()
    {
        characterMovementScript = GameObject.Find("Character").GetComponent<CharacterMovementScript>();

        senoidalFrequency = 0.4f;
        senoidalAmplitude = 0.2f;
        positionOffset = transform.position;
        sinkSpeed = 0.005f;
        recoverySpeed = 0.002f;
        timeCounter = 0.2f;
        floating = true;
        sinking = false;
        recovering = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (floating)
        {
            deltaTime += Time.deltaTime;
            transform.position = positionOffset;
            transform.position = new Vector3(transform.position.x, positionOffset.y + Mathf.Sin(senoidalFrequency * Mathf.PI * deltaTime) * senoidalAmplitude, 0);
        }

        if(sinking)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - sinkSpeed, 0);
        }

        timeCounter -= Time.deltaTime;
        
        if(recovering && timeCounter <= 0f)
        {
            if(transform.position.y == positionOffset.y)
            {
                recovering = false;
                floating = true;
            }

            transform.position = new Vector3(transform.position.x, Mathf.MoveTowards(transform.position.y, positionOffset.y, recoverySpeed * deltaTime), 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.name == "Character" && characterMovementScript.GetSize() > 1)
        {
            floating = false;
            recovering = false;
            sinking = true;
            timeCounter = 0.5f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.name == "Character")
        {
            floating = false;
            sinking = false;
            recovering = true;
        }
    }
}