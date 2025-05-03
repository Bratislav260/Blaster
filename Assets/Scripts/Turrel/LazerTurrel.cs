using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LazerTurrel : Turrel
{
    private LineRenderer lazerLine;
    private Transform pointFire;
    [SerializeField] private LayerMask ignoreLayer;
    [SerializeField] private float startTime;
    [SerializeField] private float activeDuration = 5f;
    [SerializeField] private float inactiveDuration = 3f;
    [SerializeField] private float lazerDamage;
    [SerializeField] private float cooldown;
    public bool isInfinity = false;
    private Coroutine coroutine;
    private bool isAvableAttack = true;

    public override void Activate()
    {
        lazerLine = GetComponent<LineRenderer>();
        pointFire = transform.GetChild(0);
        lazerLine.enabled = false;
        Invoke(nameof(TurnOnLazer), startTime);
    }

    public override void Diactivate()
    {
        TurnOffLazer();
    }

    private IEnumerator Lazering(bool isInfinity = false)
    {
        while (true)
        {
            float activeTime = 0f;
            while (activeTime < activeDuration)
            {
                lazerLine.enabled = true;
                lazerLine.SetPosition(0, pointFire.position);

                RaycastHit2D[] hits = Physics2D.RaycastAll(pointFire.position, pointFire.right, 100f);
                Vector3 pointTarget = pointFire.position + pointFire.right * 100f;

                List<IDamangeable> idamagebleS = new List<IDamangeable>();
                foreach (var hit in hits)
                {
                    if ((ignoreLayer.value & (1 << hit.collider.gameObject.layer)) != 0)
                    {
                        if (hit.collider.TryGetComponent<IDamangeable>(out var idamageble))
                        {
                            idamagebleS.Add(idamageble);
                        }
                        continue;
                    }

                    pointTarget = hit.point;
                    break;
                }

                lazerLine.SetPosition(1, pointTarget);

                if (isAvableAttack)
                {
                    LazerDamage(idamagebleS);
                    isAvableAttack = false;
                    StartCoroutine(Cooldown());
                }

                activeTime += Time.deltaTime;
                yield return null;
            }

            lazerLine.enabled = false;
            if (isInfinity)
            {
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(inactiveDuration);
                SoundSystem.Instance.Sound("Lazer").Play();
            }
        }
    }

    private void LazerDamage(List<IDamangeable> idamagebleS)
    {
        foreach (IDamangeable idamageble in idamagebleS)
        {
            idamageble.GetDamage(lazerDamage);
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        isAvableAttack = true;
    }

    public void TurnOnLazer()
    {
        coroutine = StartCoroutine(Lazering(isInfinity));
    }

    public void TurnOffLazer()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }
}
