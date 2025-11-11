using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityProgressBar;
using System.Collections.Generic;

public class UIController : MonoBehaviour
{
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject footing;
    [SerializeField] private List<GameObject> footingPetals;
    [SerializeField] private Slider rocketSlider;
    [SerializeField] private Slider footingSlider;
    [SerializeField] private GameObject fire;
    [SerializeField] private CharacterController rocketController;

    private float startXRocketScale;
    private float startYRocketScale;
    private float startZRocketScale;
    private float startXFootingScale;
    private float startYFootingScale;
    private float startZFootingScale;
    private float startXFireScale;
    private float startYFireScale;
    private float startZFireScale;
    private List<float> startFootingPetalsScales;
    private float startRocketHeight;
    private float startRocketRadius;
    private bool isGameActive = true;
    private bool isStartPanelActive = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        progressBar.Value = 0f;
        startPanel.SetActive(true);
        isStartPanelActive = true;
        InitializeStartPanel();
    }

    void Update() {
        if (Input.GetKey("escape")) Application.Quit();
    }
    }

    // Update is called once per frame
    public void ChangeProgressBarValue(float value)
    {
        progressBar.Value += value * 0.01f;
        if (progressBar.Value >= 1f )
        {
            progressBar.Value = 1f;
            this.ShowGameOver();
        }
        if (progressBar.Value <= 0f)
        {
            progressBar.Value = 0f;
        }
        textMeshProUGUI.text = (progressBar.Value * 100f).ToString("F2") + "%";
    }

    public void ShowGameOver()
    {
        isGameActive = false;
        gameOverPanel.SetActive(true);
    }
    public void CloseGameOverPanel()
    {
        gameOverPanel.SetActive(false);
        ChangeProgressBarValue(-100f);
        isGameActive = true;
        startPanel.SetActive(true);
        isStartPanelActive = true;
    }

    public void ApplyStartPanel()
    {
        startPanel.SetActive(false);
        isStartPanelActive = false;
    }

    public void InitializeStartPanel()
    {
        startXRocketScale = rocket.transform.localScale.x;
        startYRocketScale = rocket.transform.localScale.y;
        startZRocketScale = rocket.transform.localScale.z;
        startXFootingScale = footing.transform.localScale.x;
        startYFootingScale = footing.transform.localScale.y;
        startZFootingScale = footing.transform.localScale.z;
        startXFireScale = fire.transform.localScale.x;
        startYFireScale = fire.transform.localScale.y;
        startZFireScale = fire.transform.localScale.z;
        startRocketHeight = rocketController.height;
        startRocketRadius = rocketController.radius;
        startFootingPetalsScales = new List<float>();
        foreach (GameObject petal in footingPetals) {
            startFootingPetalsScales.Add(petal.transform.localScale.x);
            startFootingPetalsScales.Add(petal.transform.localScale.y);
            startFootingPetalsScales.Add(petal.transform.localScale.z);
        }
    }

    public void RocketSizeSliderValueChanged()
    {
        float rocketSize = (float)rocketSlider.value;
        if (rocketSize == 0) rocketSize = 0.5f;
        rocket.transform.localScale = new Vector3(startXRocketScale * (rocketSize/2f), startYRocketScale * (rocketSize/2f), startZRocketScale);
        fire.transform.localScale = new Vector3(startXFireScale * (rocketSize/2f), startYFireScale * (rocketSize/2f), startZFireScale);
        rocketController.height = startRocketHeight * (rocketSize/2f);
        rocketController.radius = startRocketRadius * (rocketSize/2f);
    }

    public void FootingSizeSliderValueChanged()
    {
        float footingSize = (float)footingSlider.value;
        if (footingSize == 0) footingSize = 0.5f;
        footing.transform.localScale = new Vector3(startXFootingScale * (footingSize/2f), startYFootingScale * (footingSize/2f), startZFootingScale * (footingSize/2f));
        for (int i = 0; i < footingPetals.Count; i++) {
            footingPetals[i].transform.localScale = new Vector3(startFootingPetalsScales[i*3] * (footingSize/2f), startFootingPetalsScales[i*3 + 1] * (footingSize/2f), startFootingPetalsScales[i*3 + 2] * (footingSize/2f));
        }
    }
    

    public bool IsGameActive()
    {
        return isGameActive;
    }

    public bool IsStartPanelActive()
    {
        return isStartPanelActive;
    }
}
