using UnityEngine;
using UnityEngine.InputSystem;          // доступ к списку устройств Input System
using UnityEngine.InputSystem.XR;       // XRControllerWithRumble

/// <summary>
///  Обработчик столкновения клинков.
///  • При OnCollisionEnter воспроизводит звук удара.
///  • Отправляет короткий хаптический импульс на оба контроллера.
///  • Мгновенно игнорирует коллизию, чтобы лезвия не «залипали».
[RequireComponent(typeof(AudioSource))]
public class CollisionResolver : MonoBehaviour
{
    // ----------- Настройки, задаваемые в инспекторе --------------

    [SerializeField] private AudioClip hitClip;     // звук удара металла
    [SerializeField] private float hapticAmp = 0.6f; // амплитуда вибро (0–1)
    [SerializeField] private float hapticDur = 0.07f; // длительность импульса в секундах

    // ----------- Служебные поля -----------------------------------

    private AudioSource _audio;

    void Awake()
    {
        // Кэшируем AudioSource, чтобы не делать GetComponent каждый удар
        _audio = GetComponent<AudioSource>();
    }

    // ----------- Основная обработка коллизии ----------------------

    void OnCollisionEnter(Collision col)
    {
        /* 1. Звук удара ------------------------------------------ */
        if (hitClip) _audio.PlayOneShot(hitClip);

        /* 2. Хаптика: пробегаем по всем XR-устройствам и отправляем импульс
              только тем, что поддерживают вибро (XRControllerWithRumble).   */
        foreach (var dev in InputSystem.devices)
        {
            if (dev is XRControllerWithRumble rumble)
            {
                rumble.SendImpulse(hapticAmp, hapticDur);
            }
        }

        /* 3. Сброс контакта: на один PhysX-так отключаем коллизию между
              своим коллайдером и оппонентом, чтобы лезвия не «залипли».   */
        var myCollider = GetComponent<Collider>();
        Physics.IgnoreCollision(col.collider, myCollider, true);
        Physics.IgnoreCollision(col.collider, myCollider, false);  // включаем обратно
    }
}
