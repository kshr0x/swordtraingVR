using UnityEngine;

public class PhysicsModule : IModule
{
    private WeaponLoader _weaponLoader;

    public void Initialize()
    {
        _weaponLoader = Object.FindObjectOfType<WeaponLoader>();
    }

    public void Tick() { /* здесь может быть глобальная физика */ }

    public void Shutdown() { }

    public void UpdateWeaponParams(WeaponConfig cfg)
    {
        if (_weaponLoader) _weaponLoader.Equip(cfg);
    }
}