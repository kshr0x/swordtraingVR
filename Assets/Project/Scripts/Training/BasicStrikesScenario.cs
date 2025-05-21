using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Scenario/Basic Strikes")]
public class BasicStrikesScenario : ScenarioBase
{
    [SerializeField] private int strikesToWin = 5;
    private int _hits;
    private SwordPhysics _sword;

    public override void Initialize()
    {
        _hits = 0;
        _sword = Object.FindObjectOfType<SwordPhysics>();
        if (_sword) _sword.OnSwing += OnSwing;
    }

    public override void Finish()
    {
        if (_sword) _sword.OnSwing -= OnSwing;
    }

    private void OnSwing(SwordSwingData data)
    {
        _hits++;
        ServiceLocator.Get<UIModule>()?.BlinkScore();
        if (_hits >= strikesToWin) Complete();
    }
}