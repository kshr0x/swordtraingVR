using UnityEngine;

/// <summary>
///  Отвечает за «физический» анализ движений меча.
///  • Слушает Rigidbody для получения линейной скорости.
///  • Сравнивает векторы forward-оси, чтобы вычислить угол замаха.
///  • При выполнении условий генерирует событие OnSwing.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class SwordPhysics : MonoBehaviour
{
    /// <summary>
    /// Срабатывает при корректном замахе.
    /// SwordSwingData содержит: сторону (Left/Right), скорость, угол.
    /// </summary>
    public System.Action<SwordSwingData> OnSwing;     

    [Header("Требования к замаху")]
    [Tooltip("Минимальная линейная скорость клинка, м/с")]
    [SerializeField] private float minSwingVel = 2f;

    private Vector3   _lastForward;   // forward-ось на предыдущем кадре
    private Rigidbody _rb;            // кэш компонента Rigidbody

    void Awake()
    {
        _rb          = GetComponent<Rigidbody>();
        _lastForward = transform.forward;   // сохраняем начальное направление
    }

    void FixedUpdate()                       // вызывается каждый PhysX-тик
    {
        // 1. Угол между текущим forward и прошлым
        float angle = Vector3.Angle(_lastForward, transform.forward);

        // 2. Мгновенная скорость меча
        float vel   = _rb.velocity.magnitude;

        // 3. Проверяем, превышены ли оба порога
        if (angle > 15f && vel > minSwingVel)
        {
            /* Определяем, из какой стороны пришёл замах.
               Берём локальную X-компоненту скорости:
               < 0  → замах слева   (Left)
               > 0  → замах справа  (Right)           */
            var localVel = transform.InverseTransformDirection(_rb.velocity);
            var side     = localVel.x < 0 ? SwingSide.Left : SwingSide.Right;

            // 4. Генерируем событие повышенного уровня
            OnSwing?.Invoke(new SwordSwingData
            {
                side     = side,
                velocity = vel,
                angle    = angle
            });
        }

        // 5. Подготовка к следующему кадру
        _lastForward = transform.forward;
    }
}
