using UnityEngine;
using UnityEngine.InputSystem;

public class InputRigBootstrap : MonoBehaviour
{
    [SerializeField] private InputActionAsset actions;
    public void EnableInput() { if (actions != null) actions.Enable(); }
}