using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionScript : MonoBehaviour
{
    private Camera camera;
    private Image transitionImage;
    private float transitionSpeed;
    private bool startTransitionIn;
    private bool startTransitionOut;
    private string nextScene;

    // Start is called before the first frame update
    void Start()
    {
        transitionImage = GameObject.Find("Transition").GetComponent<Image>();
        transitionImage.material.SetFloat("_Cutoff", -0.6f);
        transitionSpeed = 2f;
        startTransitionIn = true;
        startTransitionOut = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTransitionIn)
        {
            transitionImage.material.SetFloat("_Cutoff", Mathf.MoveTowards(transitionImage.material.GetFloat("_Cutoff"), 1.1f, transitionSpeed * Time.deltaTime));
            

            if (transitionImage.material.GetFloat("_Cutoff") >= 1f)
            {
                startTransitionIn = false;
            }
        }

        if(startTransitionOut)
        {
            if (transitionImage.material.GetFloat("_Cutoff") >= 1f)
            {
                transitionImage.transform.rotation = new Quaternion(transitionImage.transform.rotation.x, transitionImage.transform.rotation.y, 0, transitionImage.transform.rotation.w);
            }

            transitionImage.material.SetFloat("_Cutoff", Mathf.MoveTowards(transitionImage.material.GetFloat("_Cutoff"), -0.1f - transitionImage.material.GetFloat("_Edge_Smoothing"), transitionSpeed * Time.deltaTime));

            if (transitionImage.material.GetFloat("_Cutoff") <= -0.6f)
            {
                startTransitionOut = false;
                SceneManager.LoadScene(nextScene);
            }
        }
    }

    public void TransitionOut(string destination)
    {
        nextScene = destination;
        startTransitionOut = true;
    }
}