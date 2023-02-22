using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelScript : MonoBehaviour
{
    const float MOVEMENT_DURATION = 1.5f;
    private float time;
    private float speed;
    private bool showPanel;
    private bool hidePanel;
    private string currentScene;
    [SerializeField]
    private AnimationCurve movementCurve;
    private TransitionScript transitionScript;
    private TextMeshProUGUI title;
    private GameObject buttonNextLevel;
    private Vector3 refPosition;
    private Vector3 centerPosition;
    private float progress;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        showPanel = false;
        hidePanel = false;
        currentScene = SceneManager.GetActiveScene().name;
        transitionScript = GameObject.Find("Canvas Transition").GetComponent<TransitionScript>();
        title = GetComponentInChildren<TextMeshProUGUI>();
        buttonNextLevel = GameObject.Find("Next Level Button");
        transform.position = new Vector3(transform.position.x, Screen.height + 450, 0);
        refPosition = transform.position;
        centerPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        progress = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(showPanel)
        {
            progress += Mathf.Clamp(Time.deltaTime / MOVEMENT_DURATION, 0, 1);

            time += Time.deltaTime;
            speed = movementCurve.Evaluate(time);

            transform.position = Vector3.Lerp(refPosition, centerPosition, progress);

            if(progress >= 1)
            {
                progress = 0;
                showPanel = false;
            }
        }
        
        if (hidePanel)
        {
            progress += Mathf.Clamp(Time.deltaTime / (MOVEMENT_DURATION - 0.5f), 0, 1);

            time += Time.deltaTime;
            speed = movementCurve.Evaluate(time);
            
            transform.position = Vector3.Lerp(centerPosition, refPosition, progress);

            if (progress >= 1)
            {
                progress = 0;
                hidePanel = false;
            }
        }
    }

    public void ShowPanel()
    {
        showPanel = true;
        hidePanel = false;
    }

    public void ShowLossPanel()
    {
        showPanel = true;
        hidePanel = false;
        title.SetText("Level Failed");
        buttonNextLevel.SetActive(false);
    }

    public void HidePanel()
    {
        showPanel = false;
        hidePanel = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(currentScene);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void NextLevel()
    {
        string nextScene = currentScene.Split(" ")[0] + " " + (Convert.ToInt32(currentScene.Split(" ")[1]) + 1);
        time = 0;
        HidePanel();

        transitionScript.TransitionOut(nextScene);
    }
}