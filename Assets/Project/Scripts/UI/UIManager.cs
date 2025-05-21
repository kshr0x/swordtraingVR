using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("HUD Elements")]
    [SerializeField] private Canvas hudCanvas;
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Floating Message")]
    [SerializeField] private MessagePanel msgPanel;

    public void Initialize() => ToggleHUD(false);
    public void BlinkScore() { /* визуальный мигающий эффект */ }

    public void ToggleHUD(bool state) => hudCanvas.enabled = state;

    public void UpdateScore(int current, int target)
        => scoreText.text = $"{current} / {target}";

    public void ShowMessage(string txt, float time = 1.5f)
        => msgPanel.Show(txt, time);
}