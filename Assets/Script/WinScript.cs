using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    private CharacterMovementScript characterMovementScript;
    private Transform player;
    private float triggerTopBound;
    [SerializeField]
    private GameObject splash;

    void Start()
    {
        characterMovementScript = GameObject.Find("Character").GetComponent<CharacterMovementScript>();
        player = GameObject.Find("Character").GetComponent<Transform>();

        triggerTopBound = GetComponent<BoxCollider2D>().bounds.center.y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        List<Collider2D> contacts = new List<Collider2D>();

        characterMovementScript.StopMoving();
        
        Destroy(Instantiate(splash, new Vector3(player.position.x, triggerTopBound + 0.8f, 0), transform.rotation), 0.6f);
    }
}
