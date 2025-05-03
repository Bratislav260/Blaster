using UnityEditor;
using UnityEngine;

[SelectionBase]
public abstract class Enemy : MonoBehaviour, IDamangeable, IMoveable, IColorful, IAnimateable
{
    #region --- Подклассы Enemy ---
    public Locator locator { get; private set; }
    public EnemyAI enemyAI { get; private set; }
    public AbstrMovement enemyMovement;
    public EnemyIdling enemyIdling;
    #endregion

    #region --- Параметры ---
    [field: SerializeField] public float maxHealth { get; set; }
    public float currentHealth;
    [field: SerializeField] public float moveSpeed { get; set; }

    public new Rigidbody2D rigidbody { get; set; }
    public SpriteRenderer spriteRenderer { get; set; }
    public bool isMoving { get; set; }
    [field: SerializeField] public Color color { get; set; }
    public bool isAnimationTrue { get; set; } = false;
    private bool isDead = false;

    #endregion

    #region --- Радиусы Enemy ---
    [field: SerializeField, Range(0, 20)] public float chaseRadius { get; private set; } = 19;
    [field: SerializeField, Range(0, 15)] public float attackRadius { get; private set; } = 14;
    [field: SerializeField, Range(0, 10)] public float retreatRadius { get; private set; } = 9;
    [field: SerializeField, Range(0, 5)] public float meleeRadius { get; private set; } = 4f;
    #endregion

    public virtual void Initialize()
    {
        currentHealth = maxHealth;
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        enemyMovement = GetComponent<AbstrMovement>();
        enemyMovement.Initialize(this);
        enemyIdling = GetComponent<EnemyIdling>();

        locator = new Locator(transform);
        enemyAI = new EnemyAI(this);

        EnemyController.Instance.Add(this);
    }

    public virtual void EnemyUpdate()
    {
        if (transform != null)
        {
            enemyAI.stateMachine.currentEnemyStateUpdate();
        }
    }

    public virtual void EnemyFixedUpdate()
    {
        if (transform != null)
        {
            enemyAI.stateMachine.currentEnemyStateFixedUpdate();
        }
    }

    public virtual void Attack() { }

    public void GetDamage(float damage)
    {
        currentHealth -= damage;

        if (!isDead && currentHealth <= 0)
        {
            Dead();
            isDead = true;
        }
    }

    private void Dead()
    {
        ParticleManager.Instance.CallPartical("Destroy", transform, isChangeColor: true);
        EnemyController.Instance.Remove(this);
        Destroy(gameObject);
    }

    // private void OnDrawGizmos()
    // {
    //     Handles.color = Color.green;
    //     Handles.DrawWireDisc(transform.position, transform.forward, chaseRadius);

    //     Handles.color = Color.red;
    //     Handles.DrawWireDisc(transform.position, transform.forward, attackRadius);

    //     Handles.color = Color.blue;
    //     Handles.DrawWireDisc(transform.position, transform.forward, retreatRadius);

    //     Handles.color = Color.yellow;
    //     Handles.DrawWireDisc(transform.position, transform.forward, meleeRadius);
    // }
}
