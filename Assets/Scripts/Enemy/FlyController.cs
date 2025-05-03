using UnityEngine;

public class FlyController : AbstrMovement
{
    public override void Initialize(IMoveable unit)
    {
        this.unit = unit;
    }

    public override void Move(Vector2 moveDirection)
    {
        unit.rigidbody.velocity = moveDirection * moveSpeed;
    }
}
