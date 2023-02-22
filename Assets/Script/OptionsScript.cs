using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{
    [SerializeField]
    private Toggle fullscreenToggle;
    [SerializeField]
    private Toggle vsyncToggle;
    [SerializeField]
    private List<ResolutionScript> resolutions;
    private TMP_Text resolutionsLabel;
    private int selectedResolution;

    // Start is called before the first frame update
    void Start()
    {
        resolutionsLabel = GameObject.Find("Resolution Label").GetComponent<TMP_Text>();

        fullscreenToggle.isOn = Screen.fullScreen;

        vsyncToggle.isOn = QualitySettings.vSyncCount > 0;
        
        var currentResolution = resolutions.Select((r, i) => new { resolution = r, index = i }).Where(r => r.resolution.GetHorizontal() == PlayerPrefs.GetInt("res_hor") && r.resolution.GetVertical() == PlayerPrefs.GetInt("res_ver")).SingleOrDefault();

        if (currentResolution != null)
        {
            selectedResolution = currentResolution.index;
        }
        else
        {
            selectedResolution = 3;
        }
        
        resolutionsLabel.text = resolutions[selectedResolution].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResolutionLeft()
    {
        selectedResolution--;
        
        if(selectedResolution < 0)
        {
            selectedResolution = 0;
        }

        resolutionsLabel.text = resolutions[selectedResolution].ToString();
    }

    public void ResolutionRight()
    {
        selectedResolution++;
        
        if (selectedResolution > resolutions.Count - 1)
        {
            selectedResolution = resolutions.Count - 1;
        }

        resolutionsLabel.text = resolutions[selectedResolution].ToString();
    }

    public void Apply()
    {
        Screen.SetResolution(resolutions[selectedResolution].GetHorizontal(), resolutions[selectedResolution].GetVertical(), fullscreenToggle.isOn);

        if (vsyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }

        PlayerPrefs.SetInt("res_hor", resolutions[selectedResolution].GetHorizontal());
        PlayerPrefs.SetInt("res_ver", resolutions[selectedResolution].GetVertical());
    }

    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }
}