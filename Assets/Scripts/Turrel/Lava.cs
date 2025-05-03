using UnityEngine;

public class Lava : Turrel
{
    [SerializeField] private float force;
    private bool isAvaiable = false;

    public override void Activate()
    {
        isAvaiable = true;
    }

    public override void Diactivate()
    {
        isAvaiable = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamangeable>(out var idemageable) && isAvaiable)
        {
            idemageable.GetDamage(100);

            if (collision.gameObject.TryGetComponent<Rigidbody2D>(out var rb))
            {
                rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            }
        }
    }
}
