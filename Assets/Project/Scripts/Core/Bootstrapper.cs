using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private ScriptableObject defaultScenario;

    private ModuleManager _moduleMgr;
    private SceneController _sceneCtrl;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        _moduleMgr = new ModuleManager();
        ServiceLocator.Register(_moduleMgr);

        var physics = new PhysicsModule();
        var logging = new LoggingModule();
        var ui      = FindObjectOfType<UIModule>();

        _moduleMgr.AddModule(physics);
        _moduleMgr.AddModule(logging);
        _moduleMgr.AddModule(ui);
        _moduleMgr.Initialize();

        _sceneCtrl = new SceneController(physics, logging, ui);
        ServiceLocator.Register(_sceneCtrl);

        if (defaultScenario is ScenarioBase scenarioAsset)
            _sceneCtrl.RunScenario(scenarioAsset);
    }

    private void Update() => _moduleMgr.Tick();
    private void OnApplicationQuit() => _moduleMgr.Shutdown();
}
