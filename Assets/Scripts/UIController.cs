using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityProgressBar;

public class UIController : MonoBehaviour
{
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        progressBar.Value = 0f;
    }

    // Update is called once per frame
    public void ChangeProgressBarValue(float value)
    {
        progressBar.Value += value * 0.01f;
        if (progressBar.Value >= 1f )
        {
            progressBar.Value = 1f;
        }
        if (progressBar.Value <= 0f)
        {
            progressBar.Value = 0f;
        }
        textMeshProUGUI.text = (progressBar.Value * 100f).ToString("F2") + "%";
    }
}
