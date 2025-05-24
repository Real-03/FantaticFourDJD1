using System.Collections;
using UnityEngine;

public class SpriteFlash : MonoBehaviour
{
    private static SpriteFlash instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public static void Flash(SpriteRenderer spriteRenderer, float flashDuration = 0.1f, int flashCount = 2)
    {
        if (instance == null)
        {
            Debug.LogError("SpriteFlash em falta");
            return;
        }

        instance.StartCoroutine(instance.FlashRoutine(spriteRenderer, flashDuration, flashCount));
    }

    private IEnumerator FlashRoutine(SpriteRenderer spriteRenderer, float duration, int count)
    {
        Color originalColor = spriteRenderer.color;
        Color flashColor = new Color(255,255,255, 1f); // Opacidade reduzida

        for (int i = 0; i < count; i++)
        {
            spriteRenderer.color = flashColor;
            yield return new WaitForSeconds(duration);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(duration);
        }
    }
}
