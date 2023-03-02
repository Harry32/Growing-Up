using UnityEngine;

public class CharacterAnimationScript : MonoBehaviour
{
    private Animator animator;
    private Animator effectsAnimator;
    private ParticleSystem particleSystem;
    private bool isWalking;

    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.Find("CharacterSprite").GetComponent<Animator>();
        effectsAnimator = GameObject.Find("EffectAnimations").GetComponent<Animator>();
        particleSystem = GetComponentInChildren<ParticleSystem>();

        isWalking = false;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isWalking", isWalking);
    }

    public void PlayWalkAnimation()
    {
        isWalking = true;
    }

    public void StopWalkAnimation()
    {
        isWalking = false;
    }

    public void PlayJumpAnimation()
    {
        animator.SetBool("isJumping", true);
        effectsAnimator.SetTrigger("Stretch");
        particleSystem.Play();
    }

    public void StopJumpAnimation()
    {
        animator.SetBool("isJumping", false);
        effectsAnimator.SetTrigger("Squash");
        particleSystem.Play();
    }
}