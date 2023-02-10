using UnityEngine;

public class LogoScript : MonoBehaviour
{
    private float deltaTime;
    private float senoidalFrequency;
    private float senoidalAmplitude;
    private Quaternion rotationOffset;

    // Start is called before the first frame update
    void Start()
    {
        senoidalFrequency = 0.8f;
        senoidalAmplitude = 0.3f;
        rotationOffset = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += Time.deltaTime;
        transform.rotation = rotationOffset;
        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, rotationOffset.z + Mathf.Sin(senoidalFrequency * Mathf.PI * deltaTime) * senoidalAmplitude, transform.rotation.w);
    }
}