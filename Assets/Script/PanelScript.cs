using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelScript : MonoBehaviour
{
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
    }

    // Update is called once per frame
    void Update()
    {
        if(showPanel && transform.position.y > 550)
        {
            speed = movementCurve.Evaluate(time);
            time += Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y - speed);
        }
        
        if (hidePanel && transform.position.y < 2000)
        {
            speed = movementCurve.Evaluate(time);
            time += Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y + 5);
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
        buttonNextLevel.active = false;
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
        hidePanel = true;

        transitionScript.TransitionOut(nextScene);
    }
}