using UnityEngine;

public class GrowUpScript : MonoBehaviour
{
    public float growRate;
    private CharacterMovementScript characterMovementScript;
    private CameraMovementScript cameraMovementScript;

    // Start is called before the first frame update
    void Start()
    {
        characterMovementScript = GameObject.Find("Character").GetComponent<CharacterMovementScript>();
        cameraMovementScript = GameObject.Find("Main Camera").GetComponent<CameraMovementScript>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        characterMovementScript.GrowUp(growRate);
        cameraMovementScript.ChangeSize(growRate);
        Destroy(this.gameObject);
    }
}