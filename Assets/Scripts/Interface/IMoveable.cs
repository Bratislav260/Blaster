using System.Collections;
using UnityEngine;

public interface IMoveable
{
    public bool isMoving { get; set; }
    public Rigidbody2D rigidbody { get; set; }
    public SpriteRenderer spriteRenderer { get; set; }

    public IEnumerator WaitForStop()
    {
        rigidbody.drag = 5f;

        yield return FinishedAfterTime();

        if (rigidbody == null)
            yield break;

        while (rigidbody != null && rigidbody.velocity.sqrMagnitude > 0.1f)
        {
            yield return null;
        }

        if (rigidbody != null)
        {
            rigidbody.drag = 0;
            isMoving = true;
        }
    }

    public IEnumerator FinishedAfterTime()
    {
        int timer = 20;

        while (timer > 0)
        {
            timer -= 2;
            yield return null;
        }

        if (rigidbody != null)
        {
            rigidbody.drag = 0;
            isMoving = true;
        }
    }
}
