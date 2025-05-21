using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class ControllerHaptics : MonoBehaviour
{
    [SerializeField] private InputActionProperty hapticAction;
    [SerializeField] private float amplitude = 0.5f;
    [SerializeField] private float duration = 0.1f;

    void OnEnable()  => hapticAction.action.performed += OnHaptic;
    void OnDisable() => hapticAction.action.performed -= OnHaptic;

    private void OnHaptic(InputAction.CallbackContext ctx)
    {
        if (ctx.control.device is XRControllerWithRumble ctrl)
            ctrl.SendImpulse(amplitude, duration);
    }
}
