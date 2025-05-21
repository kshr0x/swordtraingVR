using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleManager : IModule
{
    private readonly List<IModule> _modules = new();
    public void AddModule(IModule m) => _modules.Add(m);
    public void Initialize() { foreach (var m in _modules) m.Initialize(); }
    public void Tick()       { foreach (var m in _modules) m.Tick(); }
    public void Shutdown()   { foreach (var m in _modules) m.Shutdown(); }
}
