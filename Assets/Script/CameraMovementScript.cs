using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    private float leftLimit;
    private float rightLimit;
    private Transform player;
    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        leftLimit = 0;
        rightLimit = 1000;

        player = GameObject.Find("Character").GetComponent<Transform>();
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(player.position.x, leftLimit, rightLimit), transform.position.y, transform.position.z);
    }

    public void ChangeSize(float changeRate)
    {
        camera.orthographicSize += changeRate;
    }
}