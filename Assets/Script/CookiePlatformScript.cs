using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookiePlatformScript : MonoBehaviour
{
    private ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Character")
        {
            Debug.Log("AAA");
            particleSystem.Play();
        }
    }
}