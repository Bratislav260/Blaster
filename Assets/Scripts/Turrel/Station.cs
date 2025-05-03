using UnityEngine;

public class Station : Turrel
{
    [SerializeField] private Enemy enemyPrefub;
    [SerializeField] private float repeatTime;

    public override void Activate()
    {
        InvokeRepeating(nameof(SummonEnemy), 3, repeatTime);
    }

    // public void Awake()
    // {
    //     InvokeRepeating(nameof(SummonEnemy), 3, repeatTime);
    // }

    public override void Diactivate()
    {
        CancelInvoke();
    }

    private void SummonEnemy()
    {
        Enemy newEnemy = Instantiate(enemyPrefub, transform.position, Quaternion.identity);
        newEnemy.Initialize();
    }
}
