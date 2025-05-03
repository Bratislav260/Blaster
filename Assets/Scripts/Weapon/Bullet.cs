using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletDamage;

    public void Shooted(Vector2 direction, float damage, float speed, float lifeTime = 2f)
    {
        bulletDamage = damage;
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.AddForce(direction * speed, ForceMode2D.Impulse);

        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<IDamangeable>(out var damageableObj))
        {
            damageableObj.GetDamage(bulletDamage);
        }
        Destroy(gameObject);
    }
}
