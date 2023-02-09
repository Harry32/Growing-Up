using UnityEngine;

public class SongScript : MonoBehaviour
{
    private bool increaseSpeed;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        increaseSpeed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (increaseSpeed && audioSource.clip.name == "Level 2")
        {
            audioSource.pitch = Mathf.MoveTowards(audioSource.pitch, 1.5f, 0.1f * Time.deltaTime);
        }
    }

    public void IncreaseSpeed()
    {
        increaseSpeed = true;
    }
}