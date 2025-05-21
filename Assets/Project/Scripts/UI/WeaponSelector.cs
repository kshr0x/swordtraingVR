using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponSelector : MonoBehaviour
{
    [SerializeField] private WeaponConfig[] arsenal;
    [SerializeField] private TextMeshProUGUI label;
    private int _idx;
    public System.Action<WeaponConfig> OnWeaponChosen;

    public void Next() { _idx = (_idx + 1) % arsenal.Length; Refresh(); }
    public void Prev() { _idx = (_idx - 1 + arsenal.Length) % arsenal.Length; Refresh(); }
    public void Confirm() => OnWeaponChosen?.Invoke(arsenal[_idx]);

    private void Refresh() => label.text = arsenal[_idx].displayNameRu;
}