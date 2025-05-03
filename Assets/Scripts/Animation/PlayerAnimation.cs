using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public void Initialize(SpriteRenderer spriteRenderer)
    {
        this.spriteRenderer = spriteRenderer;
    }

    public void PlayDamageAnimation()
    {
        StopAllCoroutines();
        StartCoroutine(DamageAnimation());
    }

    private IEnumerator DamageAnimation()
    {
        spriteRenderer.color = ParseHexColor("#F83E3E");
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

    public void PlayDeadAnimation(float respawnTime)
    {
        StopAllCoroutines();
        StartCoroutine(Dead(respawnTime));
    }

    private IEnumerator Dead(float respawnTime)
    {
        float elapsedTime = 0;
        while (elapsedTime < respawnTime)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.2f);

            elapsedTime += 0.4f;
            yield return null;
        }
        spriteRenderer.color = Color.white;
    }

    public void StartTeleportUI(float duration)
    {
        StartCoroutine(TeleportUI(duration));
    }

    private IEnumerator TeleportUI(float duration)
    {
        if (spriteRenderer == null)
            yield break;

        Color startColor = spriteRenderer.color;
        Color targetColor = ParseHexColor("#00E7FF");
        Vector3 startScale = transform.localScale;
        Vector3 newScale = new Vector3(1.2f, 1.2f, 1.2f);

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            Color newColor = Color.Lerp(startColor, targetColor, elapsedTime / duration);
            transform.localScale = Vector3.Lerp(startScale, newScale, elapsedTime / duration);
            spriteRenderer.color = newColor;

            yield return null;
        }

        spriteRenderer.color = targetColor;
        transform.localScale = newScale;

        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            Color newColor = Color.Lerp(targetColor, startColor, elapsedTime / duration);
            transform.localScale = Vector3.Lerp(newScale, startScale, elapsedTime / duration);
            spriteRenderer.color = newColor;

            yield return null;
        }

        transform.localScale = startScale;
        spriteRenderer.color = startColor;
    }

    private Color ParseHexColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString(hex, out Color color))
        {
            return color;
        }
        else
        {
            return Color.white;
        }
    }
}
