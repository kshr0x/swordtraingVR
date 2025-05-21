using System;
using UnityEngine;

public class UIModule : MonoBehaviour, IModule
{
    public event System.Action<ScenarioBase> OnModeSelected;
    public event System.Action<WeaponConfig> OnWeaponSelected;

    [SerializeField] private UIManager uiManager;
    [SerializeField] private InWorldMenu inWorldMenu;

    public void Initialize()
    {
        uiManager.Initialize();
        inWorldMenu.Initialize(this);
    }

    public void Tick() { }
    public void Shutdown() { }

    // — события из дочерних виджетов —
    internal void RaiseModeSelected(ScenarioBase scenario)   => OnModeSelected?.Invoke(scenario);
    internal void RaiseWeaponSelected(WeaponConfig weapon)   => OnWeaponSelected?.Invoke(weapon);

    public void ToggleHUD(bool state) => uiManager.ToggleHUD(state);
    public void ShowMessage(string txt, float time = 1.5f) => uiManager.ShowMessage(txt, time);
    public void UpdateScore(int current, int target)       => uiManager.UpdateScore(current, target);

    public void BlinkScore() => uiManager?.BlinkScore();

}