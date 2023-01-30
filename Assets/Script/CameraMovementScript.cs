using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    private Transform player;
    private float leftLimit;
    private float rightLimit;

    // Start is called before the first frame update
    void Start()
    {
        leftLimit = 0;
        rightLimit = 1000;

        player = GameObject.Find("Character").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(player.position.x, leftLimit, rightLimit), transform.position.y, transform.position.z);
    }
}