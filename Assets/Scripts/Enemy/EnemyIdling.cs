using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdling : MonoBehaviour
{
    private Enemy enemy;
    protected bool isAvableIdling = false;
    [SerializeField] private float changeInterval = 2f;
    [SerializeField] private int duration = 5;
    private int direction;
    private Vector2 currentDirection = Vector2.zero;
    private List<Vector2> directions = new List<Vector2>();
    private float baseLocalScale;

    public void Initialize(Enemy enemy, List<Vector2> directions)
    {
        this.enemy = enemy;
        isAvableIdling = true;
        this.directions = directions;

        baseLocalScale = enemy.transform.localScale.x;
    }

    public void StartIdling()
    {
        StopAllCoroutines();
        isAvableIdling = true;
        StartCoroutine(ChangeDirection());
    }

    public void Idling()
    {
        if (isAvableIdling)
        {
            enemy.enemyMovement.Move(currentDirection);
            enemy.enemyMovement.FlipSprite();
            SetWeaponDirection();
        }
    }

    public void Stopidling()
    {
        StopAllCoroutines();
        enemy.transform.localScale = new Vector2(baseLocalScale, baseLocalScale);
        isAvableIdling = false;
    }

    public virtual IEnumerator ChangeDirection()
    {
        while (true)
        {
            direction = Random.Range(0, directions.Count);
            currentDirection = directions[direction];
            yield return new WaitForSeconds(changeInterval);
            currentDirection = Vector2.zero;

            int time = Random.Range(duration - 3, duration + 3);
            yield return new WaitForSeconds(time);
        }
    }

    private void SetWeaponDirection()
    {
        if (direction == 0)
        {
            enemy.transform.localScale = new Vector2(baseLocalScale, enemy.transform.localScale.y);
            // enemy.weaponSpriteRenderer.flipY = false;
        }
        else if (direction == 2)
        {
            enemy.transform.localScale = new Vector2(-baseLocalScale, enemy.transform.localScale.y);
            // enemy.weaponSpriteRenderer.flipY = true;
        }
    }
}
