using UnityEngine;

public class SongTriggerScript : MonoBehaviour
{
    private bool isActive;
    private SongScript songScript;

    void Start()
    {
        songScript = GameObject.Find("Song").GetComponent<SongScript>();
        isActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isActive)
        {
            songScript.IncreaseSpeed();
            isActive = true;
        }
    }
}
