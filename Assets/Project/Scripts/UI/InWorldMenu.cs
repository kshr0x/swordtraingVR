using UnityEngine;

public class InWorldMenu : MonoBehaviour
{
    [SerializeField] private ModeSelector modeSel;
    [SerializeField] private WeaponSelector weaponSel;
    private UIModule _ui;

    public void Initialize(UIModule ui)
    {
        _ui = ui;
        modeSel.OnScenarioChosen += s => _ui.RaiseModeSelected(s);
        weaponSel.OnWeaponChosen += w => _ui.RaiseWeaponSelected(w);
    }

    public void Toggle(bool state) => gameObject.SetActive(state);
}