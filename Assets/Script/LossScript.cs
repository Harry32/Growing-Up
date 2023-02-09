using UnityEngine;

public class LossScript : MonoBehaviour
{
    private CharacterMovementScript characterMovementScript;
    private PanelScript panelScript;
    private AudioSource songAudioSource;
    [SerializeField]
    private AudioClip lossSong;

    // Start is called before the first frame update
    void Start()
    {
        characterMovementScript = GameObject.Find("Character").GetComponent<CharacterMovementScript>();
        panelScript = GameObject.Find("Panel").GetComponent<PanelScript>();
        songAudioSource = GameObject.Find("Song").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Character")
        {
            songAudioSource.Stop();
            songAudioSource.clip = lossSong;
            songAudioSource.pitch = 1;
            songAudioSource.loop = false;
            songAudioSource.Play();
            characterMovementScript.StopMoving();
            panelScript.ShowLossPanel();
        }
    }
}