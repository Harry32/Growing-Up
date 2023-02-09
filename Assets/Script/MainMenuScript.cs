using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    private Image transitionImage;
    private float transitionSpeed;
    private bool startTransition;

    void Start()
    {
        transitionImage = GameObject.Find("Transition").GetComponent<Image>();
        transitionImage.material.SetFloat("_Cutoff", 1);
        transitionSpeed = 2f;
        startTransition = false;
    }

    void Update()
    {
        if (startTransition)
        {
            transitionImage.material.SetFloat("_Cutoff", Mathf.MoveTowards(transitionImage.material.GetFloat("_Cutoff"), -0.1f - transitionImage.material.GetFloat("_Edge_Smoothing"), transitionSpeed * Time.deltaTime));

            if (transitionImage.material.GetFloat("_Cutoff") == -0.6f)
            {
                SceneManager.LoadScene("Level 1");
            }
        }
        else
        {
            transitionImage.material.SetFloat("_Cutoff", Mathf.MoveTowards(transitionImage.material.GetFloat("_Cutoff"), 1.1f, transitionSpeed * Time.deltaTime));
        }

    }

    [ContextMenu("Transition")]
    public void Transition()
    {
        startTransition = !startTransition;
    }

    public void StartGame()
    {
        startTransition = true;
        
    }

    public void Credits()
    {
        SceneManager.LoadScene("Level 4");
    }

    public void Quit()
    {
        Application.Quit();
    }
}