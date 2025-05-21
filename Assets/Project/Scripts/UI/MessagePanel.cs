using UnityEngine;
using TMPro;

public class MessagePanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup cg;
    [SerializeField] private TextMeshProUGUI label;

    public void Show(string text, float time)
    {
        StopAllCoroutines();
        label.text = text;
        StartCoroutine(FadeRoutine(time));
    }

    private System.Collections.IEnumerator FadeRoutine(float t)
    {
        cg.alpha = 1;
        yield return new WaitForSeconds(t);
        while (cg.alpha > 0)
        {
            cg.alpha -= Time.deltaTime;
            yield return null;
        }
    }
}
