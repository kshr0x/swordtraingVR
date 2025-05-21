using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Weapon Config")]
public class WeaponConfig : ScriptableObject
{
    public string displayNameRu;
    public Sprite icon;

    [Header("Physical")]
    public float totalMassKg = 1.2f;
    public float bladeLengthM = 0.9f;
    public Vector3 centerOfMassLocal;
    [Header("Gameplay")]
    public float damageCoef = 1f;
    public AnimationCurve hapticProfile = AnimationCurve.Linear(0,0,1,1);
    [Header("Assets")]
    public GameObject weaponPrefab;
}
