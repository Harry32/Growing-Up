using UnityEngine;

public class DropScript : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private BoxCollider2D collider;
    private Animator animator;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.enabled = true;
        collider.enabled = false;
        rigidbody2D.bodyType = RigidbodyType2D.Static;

        Destroy(this.gameObject, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.enabled = true;
        collider.enabled = false;
        rigidbody2D.bodyType = RigidbodyType2D.Static;

        Destroy(this.gameObject, 0.5f);
    }
}