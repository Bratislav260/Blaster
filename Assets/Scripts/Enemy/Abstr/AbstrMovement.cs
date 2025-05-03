using UnityEngine;

public abstract class AbstrMovement : MonoBehaviour
{
    protected IMoveable unit;
    [SerializeField] protected float moveSpeed;

    public abstract void Initialize(IMoveable unit);

    public abstract void Move(Vector2 moveDirection);

    public virtual void FlipSprite()
    {
        if (unit.rigidbody.velocity.x > 0)
        {
            unit.spriteRenderer.flipX = false;
        }
        else if (unit.rigidbody.velocity.x < 0)
        {
            unit.spriteRenderer.flipX = true;
        }
    }
}
