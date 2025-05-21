using UnityEngine;

public class XRModule : MonoBehaviour, IModule
{
    [SerializeField] private InputRigBootstrap bootstrap;
    public void Initialize() => bootstrap.EnableInput();
    public void Tick() { }
    public void Shutdown() { }
}