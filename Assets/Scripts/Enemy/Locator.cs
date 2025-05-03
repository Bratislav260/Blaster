using UnityEngine;

public class Locator
{
    private Transform unit;
    private Transform target;

    public Locator(Transform unit)
    {
        this.unit = unit;
    }

    public bool Locate(float locateRadius, float innerRadius = 0)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(unit.position, locateRadius);

        foreach (Collider2D collider in colliders)
        {
            float distance = Vector2.Distance(unit.transform.position, collider.transform.position);

            if (collider.gameObject.tag == "Player" && distance <= locateRadius && distance >= innerRadius)
            {
                if (IsPathClear(collider.transform.position, LayerMask.GetMask("Platform")))
                {
                    target = collider.transform;
                    return true;
                }
            }
        }

        target = null;
        return false;
    }

    public bool IsPathClear(Vector2 target, LayerMask obstacleMask)
    {
        Vector2 direction = (target - (Vector2)unit.transform.position).normalized;
        float distance = Vector2.Distance(unit.transform.position, target);

        RaycastHit2D hit = Physics2D.Raycast(unit.transform.position, direction, distance, obstacleMask);

        if (hit.collider != null)
        {
            return false;
        }

        return true;
    }

    public Transform GetTarget()
    {
        return target;
    }

    public Vector3 GetTargetDirection(bool isDirectionToTarget = true)
    {
        if (isDirectionToTarget)
        {
            return (target.position - unit.position).normalized;
        }
        else
        {
            return (unit.position - target.position).normalized;
        }
    }
}
