using UnityEngine;

public abstract class ScenarioBase : ScriptableObject
{
    protected PhysicsModule physics;
    protected LoggingModule logging;
    protected UIModule ui;

    public virtual void Initialize(PhysicsModule p, LoggingModule l, UIModule u)
    {
        physics = p; logging = l; ui = u;
        logging.Log("scenario_start", name);
    }
      /// <summary>Вызывается SceneController‑ом при старте сцены.</summary>
    public virtual void Initialize() { }

    /// <summary>Вызывается раз в кадр из SceneController.Tick()</summary>
    public virtual void Tick() { }

    /// <summary>Вызывается при выходе или завершении сценария.</summary>
    public virtual void Finish() { }

    /// <summary>Сигнал завершения (успех).</summary>
    protected void Complete()
        => ServiceLocator.Get<SceneController>()?.OnScenarioComplete(this);
}
