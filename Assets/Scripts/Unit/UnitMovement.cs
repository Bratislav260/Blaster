using UnityEngine;

[RequireComponent(typeof(GroundChecker))]
public class UnitMovement : AbstrMovement
{
    [SerializeField] private float jumpForce;
    private GroundChecker groundChecker;

    public override void Initialize(IMoveable unit)
    {
        this.unit = unit;
        groundChecker = GetComponent<GroundChecker>();
    }

    public override void Move(Vector2 moveDirection)
    {
        Vector2 velocity = unit.rigidbody.velocity;
        velocity.x = moveDirection.x * moveSpeed;
        unit.rigidbody.velocity = velocity;
    }

    public void Jump()
    {
        if (groundChecker.IsOnGround())
        {
            unit.rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
