using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationScript : MonoBehaviour
{
    private Animator animator;
    private bool isWalking;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

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
    }

    public void StopJumpAnimation()
    {
        animator.SetBool("isJumping", false);
    }
}