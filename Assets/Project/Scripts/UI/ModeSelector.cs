using UnityEngine;
using TMPro;

public class ModeSelector : MonoBehaviour
{
    [SerializeField] private ScenarioBase[] scenarios;
    [SerializeField] private TextMeshProUGUI label;
    private int _idx;
    public System.Action<ScenarioBase> OnScenarioChosen;

    public void Next() { _idx = (_idx + 1) % scenarios.Length; Refresh(); }
    public void Prev() { _idx = (_idx - 1 + scenarios.Length) % scenarios.Length; Refresh(); }
    public void Confirm() => OnScenarioChosen?.Invoke(scenarios[_idx]);

    private void Refresh() => label.text = scenarios[_idx].name;
}
