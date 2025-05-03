using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Mine : Turrel, IColorful
{
    private bool isAvailableToExplode = false;
    [SerializeField] private List<string> explodeTags = new List<string>();
    [SerializeField] private float explosionRange;
    [SerializeField] private float explosionForce;
    [SerializeField] private float explosionDamage;
    public Color color { get; set; }

    public override void Activate()
    {
        isAvailableToExplode = true;
    }

    public override void Diactivate()
    {
        isAvailableToExplode = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (explodeTags.Contains(collision.gameObject.tag) && isAvailableToExplode)
        {
            Explode();
        }
    }

    private void Explode()
    {
        Collider2D[] explodead = Physics2D.OverlapCircleAll(transform.position, explosionRange);

        foreach (Collider2D unit in explodead)
        {
            if (unit.TryGetComponent<IDamangeable>(out var damangeableUnit))
            {
                damangeableUnit.GetDamage(explosionDamage);
            }

            if (unit.TryGetComponent<IMoveable>(out var moveableUnit))
            {
                moveableUnit.isMoving = false;
                Vector2 explosionDirection = (moveableUnit.rigidbody.position - (Vector2)transform.position).normalized;
                // if (explosionDirection.magnitude > 0)
                {
                    // float rangeExplosionForce = explosionForce / explosionDirection.magnitude;
                    moveableUnit.rigidbody.velocity = Vector2.zero;
                    moveableUnit.rigidbody.AddForce(explosionDirection * explosionForce, ForceMode2D.Impulse);
                }

                if (moveableUnit != null)
                    StartCoroutine(moveableUnit.WaitForStop());
            }
        }

        isAvailableToExplode = false;
        SoundSystem.Instance.Sound("Death").Play();
        Destroy(gameObject, 3f);
    }

    // private void OnDrawGizmos()
    // {
    //     Handles.color = Color.red;
    //     Handles.DrawWireDisc(transform.position, transform.forward, explosionRange);
    // }
}
