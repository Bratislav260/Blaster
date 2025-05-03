using UnityEngine;

public class ClimbController : MonoBehaviour
{
    [SerializeField] private float climbSpeed;
    private new Rigidbody2D rigidbody;
    private float defaultGravityScale;
    public int chainsCount = 0;

    public void Initialize(Rigidbody2D rigidbody)
    {
        this.rigidbody = rigidbody;
        defaultGravityScale = rigidbody.gravityScale;
    }

    public void SetClimbMode()
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.gravityScale = 0f;
    }

    public void ResetClimbMode()
    {
        rigidbody.gravityScale = defaultGravityScale;
    }

    public void OnClimbMove(Vector3 moveDirection)
    {
        rigidbody.velocity = moveDirection * climbSpeed;
    }
}
