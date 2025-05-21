#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem.Users;

[InitializeOnLoad]
public static class DeviceSimulatorHints
{
    static DeviceSimulatorHints()
    {
        EditorApplication.playModeStateChanged += s =>
        {
            if (s == PlayModeStateChange.EnteredPlayMode)
            {
                if (InputUser.all.Count == 0)
                    Debug.Log("[DeviceSimulator] Чтобы выбрать контроллер, нажмите T или Y, затем WASD/мышь для перемещения.");
            }
        };
    }
}
#endif
