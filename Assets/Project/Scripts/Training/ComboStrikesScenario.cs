using UnityEngine;

/// <summary>
/// Сценарий «Комбинированные удары».
///
/// • pattern — линейный шаблон сторон (Left, Right, Up …).
/// • После успешного Swing переходим к следующему элементу.
/// • Если интервал между ударами > maxInterval → сбрасываем цепочку.
/// </summary>
[CreateAssetMenu(menuName = "Combat/Scenario/Combo Strikes")]
public class ComboStrikesScenario : ScenarioBase
{
    // ---------- Параметры, настраиваемые из инспектора ----------

    [Tooltip("Очередность сторон замаха")]
    public SwingSide[] pattern = { SwingSide.Left, SwingSide.Right, SwingSide.Up };

    [Tooltip("Максимальный интервал между ударами, с")]
    public float maxInterval = 0.7f;      // = 63 кадров при 90 Гц

    // ---------- Служебные поля ----------

    private int         _index;   // текущая позиция в шаблоне
    private float       _timer;   // время с предыдущего удара
    private SwordPhysics _sword;  // ссылка на трекер клинка
    private UIModule    _ui;      // для визуальной обратной связи

    // ---------- Жизненный цикл сценария ----------

    public override void Initialize()
    {
        _index = 0;
        _timer = 0f;

        // Ищем SwordPhysics в сцене (можно сделать DI через инспектор)
        _sword = Object.FindObjectOfType<SwordPhysics>();

        // Берём ссылку на UI через ServiceLocator
        _ui = ServiceLocator.Get<UIModule>();

        // Подписываемся на события замаха
        if (_sword) _sword.OnSwing += OnSwing;
    }

    /// <summary>
    /// Вызывается каждый кадр (SceneController вызывает Scenario.Tick)
    /// </summary>
    public override void Tick()
    {
        _timer += Time.deltaTime;

        // Если время ожидания превысило допуск — комбо сбрасывается
        if (_timer > maxInterval)
            ResetCombo();
    }

    public override void Finish()
    {
        // Отписываемся, чтобы не утекли делегаты
        if (_sword) _sword.OnSwing -= OnSwing;
    }

    // ---------- Основная логика ----------

    private void OnSwing(SwordSwingData swing)
    {
        /* 1. Проверяем, совпадает ли сторона удара с требуемой
              в текущем элементе шаблона. */
        if (swing.side != pattern[_index])
        {
            ResetCombo();
            return;                     // выход — попытка не засчитана
        }

        /* 2. Удар корректный: визуально подсветим успех
              (метод BlinkScore можно позже заменить анимацией). */
        _ui?.BlinkScore();

        /* 3. Продвигаем индекс, обнуляем таймер */
        _index++;
        _timer = 0f;

        /* 4. Если прошли весь шаблон → сценарий считается выполненным */
        if (_index >= pattern.Length)
            Complete();                 // метод унаследован от ScenarioBase
    }

    /// <summary>Возврат к началу цепочки после ошибки или тайм-аута.</summary>
    private void ResetCombo()
    {
        _index = 0;
        _timer = 0f;
    }
}
