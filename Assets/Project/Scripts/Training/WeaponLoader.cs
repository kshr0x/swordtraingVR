using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponLoader : MonoBehaviour
{
    [SerializeField] private Transform handAttachPoint;
    private XRGrabInteractable _current;

    public void Equip(WeaponConfig cfg)
    {
        if (_current) Destroy(_current.gameObject);
        var go = Instantiate(cfg.weaponPrefab, handAttachPoint);
        _current = go.GetComponent<XRGrabInteractable>();

        // прокинуть физ-параметры
        if (go.TryGetComponent(out SwordPhysics sp))
        {
            var data = new WeaponRuntimeData
            {
                mass = cfg.totalMassKg,
                centerOfMass = cfg.centerOfMassLocal,
                damageCoef = cfg.damageCoef,
                hapticCurve = cfg.hapticProfile
            };
        }
    }
}