using System.Collections;
using UnityEngine;

public class FlyPlatform : Turrel
{
    private Vector3 firstPosition;
    private Vector3 secondPosition;
    private Vector3 targetPosition;
    private Coroutine coroutine;
    private bool isAvaiable = false;
    [SerializeField] private float transitionDuration;

    private void Awake()
    {
        firstPosition = transform.GetChild(0).position;
        secondPosition = transform.GetChild(1).position;
        transform.position = firstPosition;
    }

    public override void Activate()
    {
        targetPosition = (targetPosition == firstPosition) ? secondPosition : firstPosition;

        coroutine = StartCoroutine(Flying());
        isAvaiable = true;
    }

    public override void Diactivate()
    {
        isAvaiable = false;
        UnchildPlayer();
        StopCoroutine(coroutine);
    }

    private IEnumerator Flying()
    {
        while (true)
        {
            SoundSystem.Instance.Sound("Platforma").Play();
            while (Vector2.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, transitionDuration * Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }

            SoundSystem.Instance.Sound("Platforma").Stop();
            targetPosition = (targetPosition == firstPosition) ? secondPosition : firstPosition;
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isAvaiable)
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isAvaiable)
        {
            if (gameObject.activeSelf && collision.gameObject.activeInHierarchy && gameObject.activeInHierarchy)
            {
                collision.transform.SetParent(null);
            }
        }
    }

    private void UnchildPlayer()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<Player>(out var player))
            {
                player.gameObject.transform.SetParent(null);
            }
        }
    }
}
