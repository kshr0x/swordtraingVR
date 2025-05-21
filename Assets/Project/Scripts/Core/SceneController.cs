using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController
{
    private readonly PhysicsModule _physics;
    private readonly LoggingModule _logging;
    private readonly UIModule _ui;
    private ScenarioBase _currentScenario;

    public SceneController(PhysicsModule phys, LoggingModule log, UIModule ui)
    {
        _physics = phys; _logging = log; _ui = ui;
        _ui.OnModeSelected += RunScenario;
        _ui.OnWeaponSelected += OnWeaponChosen;
    }

    public void RunScenario(ScenarioBase asset)
    {
        if (_currentScenario) Object.Destroy(_currentScenario);
        _currentScenario = Object.Instantiate(asset);
        _currentScenario.Initialize(_physics, _logging, _ui);
        _ui.ToggleHUD(true);
    }

    private void OnWeaponChosen(WeaponConfig cfg)
    {
        _ui.ShowMessage($"Выбран {cfg.displayNameRu}");
        _physics.UpdateWeaponParams(cfg);
    }

        public void OnScenarioComplete(ScenarioBase scenario)
    {
        Debug.Log($"Scenario {scenario.name} finished");
        // TODO: switch back to menu / log results
    }
}

