using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    private CharacterMovementScript characterMovementScript;
    private PanelScript panelScript;
    private Transform player;
    private float triggerTopBound;
    [SerializeField]
    private GameObject splash;
    private AudioSource audioSource;
    private AudioSource songAudioSource;
    [SerializeField]
    private AudioClip winSong;

    void Start()
    {
        characterMovementScript = GameObject.Find("Character").GetComponent<CharacterMovementScript>();
        panelScript = GameObject.Find("Panel").GetComponent<PanelScript>();
        player = GameObject.Find("Character").GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
        songAudioSource = GameObject.Find("Song").GetComponent<AudioSource>();

        triggerTopBound = GetComponent<BoxCollider2D>().bounds.center.y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        List<Collider2D> contacts = new List<Collider2D>();

        audioSource.Play();
        songAudioSource.Stop();
        songAudioSource.clip = winSong;
        songAudioSource.pitch = 1;
        songAudioSource.loop = false;
        songAudioSource.Play();
        characterMovementScript.StopMoving();
        panelScript.ShowPanel();
        
        Destroy(Instantiate(splash, new Vector3(player.position.x, triggerTopBound + 0.8f, 0), transform.rotation), 0.6f);
    }
}