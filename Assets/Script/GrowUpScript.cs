using UnityEngine;

public class GrowUpScript : MonoBehaviour
{
    public float growRate;
    private float deltaTime;
    private float senoidalFrequency;
    private float senoidalAmplitude;
    private Vector3 positionOffset;
    private CharacterMovementScript characterMovementScript;
    private CameraMovementScript cameraMovementScript;

    // Start is called before the first frame update
    void Start()
    {
        characterMovementScript = GameObject.Find("Character").GetComponent<CharacterMovementScript>();
        cameraMovementScript = GameObject.Find("Main Camera").GetComponent<CameraMovementScript>();

        senoidalFrequency = 1f;
        senoidalAmplitude = 0.5f;
        positionOffset = transform.position;
    }

    void Update()
    {
        deltaTime += Time.deltaTime;
        transform.position = positionOffset;
        transform.position = new Vector3(transform.position.x, positionOffset.y + Mathf.Sin(senoidalFrequency * Mathf.PI * deltaTime) * senoidalAmplitude, 0);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        characterMovementScript.GrowUp(growRate);
        cameraMovementScript.ChangeSize(growRate);
        Destroy(this.gameObject);
    }
}