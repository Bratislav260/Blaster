using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour, IDamangeable, IMoveable, IColorful, IAnimateable
{
    #region --- Подклассы Player ---
    public UnitMovement playerMovement { get; private set; }
    public PlayerInputHandler inputHandler { get; private set; }
    public WeaponController weaponController { get; private set; }
    private WeaponSwitch weaponSwitch;
    public AttackController attackController { get; private set; }
    public ClimbController climbController { get; private set; }
    [field: SerializeField] public Inventory inventory { get; private set; }
    public PlayerAnimation playerAnimation { get; private set; }
    #endregion

    #region --- Параметры ---
    [field: SerializeField] public float maxHealth { get; set; }
    public float currentHealth;
    [field: SerializeField] public int maxLifes { get; private set; }
    private float currentLife;

    public new Rigidbody2D rigidbody { get; set; }
    public SpriteRenderer spriteRenderer { get; set; }
    public Transform weaponPivot { get; private set; }
    public bool isMoving { get; set; } = true;
    [field: SerializeField] public Color color { get; set; }
    [SerializeField] private float respawnTime;
    public bool isAnimationTrue { get; set; } = false;
    private bool isInvulnerable = false;
    #endregion

    public void Initialize()
    {
        currentHealth = maxHealth;
        currentLife = maxLifes;
        weaponPivot = transform.GetChild(0);

        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        playerMovement = GetComponent<UnitMovement>();
        inputHandler = new PlayerInputHandler(this);
        weaponController = GetComponent<WeaponController>();
        weaponSwitch = new WeaponSwitch(this);
        attackController = GetComponent<AttackController>();
        climbController = GetComponent<ClimbController>();
        climbController.Initialize(rigidbody);

        playerAnimation = GetComponent<PlayerAnimation>();
        playerAnimation.Initialize(spriteRenderer);

        inventory.Initialize();
        attackController.Initialize(this, weaponPivot);
        playerMovement.Initialize(this);
        weaponController.Initialize(this);

        UIEventManager.HealbarUIUpdate(currentHealth / maxHealth);
    }

    public void PlayerUpdate()
    {
        if (!isAnimationTrue)
        {
            weaponController.WeaponRotate();
        }
        weaponSwitch.SwitchWeapon(inputHandler.mouseScrollY);
    }

    public void PlayerFixedUpdate()
    {
        if (isMoving)
        {
            if (inputHandler.inputControls.Player.Movement.enabled)
            {
                playerMovement.Move(inputHandler.MoveInputVector);
            }
            else if (inputHandler.inputControls.Player.Climb.enabled)
            {
                climbController.OnClimbMove(inputHandler.MoveInputVector);
            }
        }
    }

    public void SetIsMoving()
    {
        isMoving = !isMoving;
    }

    public void GetDamage(float damage)
    {
        if (!isInvulnerable)
        {
            currentHealth -= damage;
            playerAnimation.PlayDamageAnimation();
            UIEventManager.HealbarUIUpdate(currentHealth / maxHealth);

            if (currentHealth <= 0)
            {
                currentLife--;
                UIEventManager.HeartUIUpdate((int)currentLife);
                Dead();
            }
        }

    }

    private void Dead()
    {
        // Debug.Log("Player dead");
        ParticleManager.Instance.CallPartical("Destroy", transform, isChangeColor: true);

        if (currentLife > 0)
        {
            SoundSystem.Instance.Sound("Heart").Play();
            isInvulnerable = true;
            playerAnimation.PlayDeadAnimation(respawnTime);
            Invoke(nameof(Respawn), respawnTime);
        }
        else if (currentLife <= 0)
        {
            SoundSystem.Instance.Sound("Death").Play();
            GameManager.Instance.GameOver();
            inputHandler.OnDisable();
            gameObject.SetActive(false);
        }
    }

    private void Respawn()
    {
        currentHealth = maxHealth;
        isInvulnerable = false;
        UIEventManager.HealbarUIUpdate(currentHealth / maxHealth);
    }

    private void OnDestroy()
    {
        inputHandler.OnDisable();
    }
}