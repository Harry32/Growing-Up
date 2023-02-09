using UnityEngine;

public class DropScript : MonoBehaviour
{
    public float shrinkRate;
    private Rigidbody2D rigidbody2D;
    private BoxCollider2D collider;
    private Animator animator;
    private CharacterMovementScript characterMovementScript;
    private CameraMovementScript cameraMovementScript;
    private AudioSource audioSource;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        characterMovementScript = GameObject.Find("Character").GetComponent<CharacterMovementScript>();
        cameraMovementScript = GameObject.Find("Main Camera").GetComponent<CameraMovementScript>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.volume = CalculateVolume();
        audioSource.Play();
        animator.enabled = true;
        collider.enabled = false;
        rigidbody2D.bodyType = RigidbodyType2D.Static;

        Destroy(this.gameObject, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioSource.volume = CalculateVolume();
        audioSource.Play();
        animator.enabled = true;
        collider.enabled = false;
        rigidbody2D.bodyType = RigidbodyType2D.Static;

        if (collision.name == "Character" && characterMovementScript.GetSize() > 0)
        {
            characterMovementScript.ShrinkDown(shrinkRate);
            cameraMovementScript.ChangeSize(shrinkRate);
        }

        Destroy(this.gameObject, 0.5f);
    }

    private float CalculateVolume()
    {
        int maxDistance = 20, minDistance = 1;
        float distance = Vector3.Distance(characterMovementScript.transform.position, transform.position);

        if (distance > maxDistance)
        {
            return 0;
        }

        if(distance < minDistance)
        {
            return 1;
        }

        return 1 - (distance - minDistance) / (maxDistance - minDistance);
    }
}