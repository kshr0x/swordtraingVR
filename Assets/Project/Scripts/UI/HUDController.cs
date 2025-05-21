using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private CanvasGroup comboFlash;
    [SerializeField] private float flashDuration = 0.2f;

    private int _currentScore;
    private int _targetScore;
    private float _remainingTime;
    private Coroutine _timerRoutine;
    private Coroutine _flashRoutine;

    public void SetScore(int current, int target)
    {
        _currentScore = current;
        _targetScore = target;
        scoreText.text = $"{_currentScore} / {_targetScore}";
    }

    public void StartCountdown(float seconds)
    {
        if (_timerRoutine != null) StopCoroutine(_timerRoutine);
        _timerRoutine = StartCoroutine(TimerRoutine(seconds));
    }

    public void SetTimer(float t)
    {
        _remainingTime = t;
        timerText.text = _remainingTime.ToString("F1") + "s";
    }

    private System.Collections.IEnumerator TimerRoutine(float sec)
    {
        _remainingTime = sec;
        while (_remainingTime > 0)
        {
            SetTimer(_remainingTime);
            yield return null;
            _remainingTime -= Time.deltaTime;
        }
        SetTimer(0);
        _timerRoutine = null;
    }

    public void PlayComboFlash()
    {
        if (_flashRoutine != null) StopCoroutine(_flashRoutine);
        _flashRoutine = StartCoroutine(Flash());
    }

    private System.Collections.IEnumerator Flash()
    {
        comboFlash.alpha = 1;
        float t = 0;
        while (t < flashDuration)
        {
            comboFlash.alpha = 1 - (t / flashDuration);
            t += Time.deltaTime;
            yield return null;
        }
        comboFlash.alpha = 0;
        _flashRoutine = null;
    }
}
