using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float checkDistance = 0.1f;
    [SerializeField] private Transform checkPoint;
    private bool isGrounded;

    public bool IsOnGround()
    {
        isGrounded = Physics2D.Raycast(checkPoint.position, Vector2.down, checkDistance, groundLayer);
        return isGrounded;
        // Debug.DrawRay(transform.position, Vector2.down * checkDistance, isGrounded ? Color.green : Color.red);
    }
}
