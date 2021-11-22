using UnityEngine;
using TMPro;
using DG.Tweening;

public class NotificationTxtUi : MonoBehaviour
{
    public TMP_Text content;

    public void SetContent(string content, float duration)
    {
        this.content.text = content;
        DOVirtual.Float(duration, 0f, duration, OnUpdate).OnComplete(()=> { Destroy(gameObject); });
    }

    private void OnUpdate(float value)
    {
        content.alpha = Mathf.Min(value, 1f);
    }
}
