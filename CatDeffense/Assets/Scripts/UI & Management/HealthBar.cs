using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image barImage;          
    [SerializeField] private Sprite[] healthSprites;   

    public void SetHealth(float current, float max)
    {
        if (barImage == null || healthSprites == null || healthSprites.Length == 0)
            return;

        float ratio = current / max;

        int index = Mathf.RoundToInt((healthSprites.Length - 1) * ratio);

        index = Mathf.Clamp(index, 0, healthSprites.Length - 1);

        barImage.sprite = healthSprites[index];
    }
}
