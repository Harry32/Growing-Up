using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudioScript : MonoBehaviour
{
    private bool playFall;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip growAudio;
    [SerializeField]
    private AudioClip shrinkAudio;
    [SerializeField]
    private AudioClip jumpAudio;
    [SerializeField]
    private AudioClip fallAudio;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        playFall = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayGrowAudio()
    {
        PlayAudio(growAudio);
    }

    public void PlayShrinkAudio()
    {
        PlayAudio(shrinkAudio);
    }

    public void PlayJumpAudio()
    {
        PlayAudio(jumpAudio);
    }

    public void PlayFallAudio()
    {
        if (playFall)
        {
            if (!audioSource.isPlaying || audioSource.clip.name != "Yoi")
            {
                PlayAudio(fallAudio);
            }

            playFall = false;
        }
    }

    public void CanPlayFall()
    {
        playFall= true;
    }

    private void PlayAudio(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}