using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    private float leftLimit;
    private float rightLimit;
    private float upLimit;
    private float downLimit;
    private float cameraSize;
    private int zoomSpeed;    
    private Transform player;
    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        leftLimit = 0;
        rightLimit = 1000;
        upLimit = 1000;
        downLimit = -6;
        zoomSpeed = 4 * 3;

        player = GameObject.Find("Character").GetComponent<Transform>();
        camera = GetComponent<Camera>();

        cameraSize = camera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(player.position.x, leftLimit, rightLimit), Mathf.Clamp(player.position.y, downLimit, upLimit), transform.position.z);

        if(cameraSize != camera.orthographicSize)
        {
            camera.orthographicSize = Mathf.MoveTowards(camera.orthographicSize, cameraSize, zoomSpeed * Time.deltaTime);
        }
    }

    public void ChangeSize(float changeRate)
    {
        cameraSize = camera.orthographicSize + changeRate * 3;
    }
}