using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] EnemyController enemyController;
    [SerializeField] CellController cellController;
    [SerializeField] TeleportController teleportController;
    [SerializeField] UIController uIController;
    [SerializeField] ParticleManager particleManager;
    [SerializeField] SoundSystem soundSystem;

    private void Awake()
    {
        particleManager.Initialize();
        soundSystem.Initialize();
        uIController.Initialize();

        player.Initialize();
        enemyController.Initialize();
        cellController.Initialize();

        teleportController.StartTeleporting();

        SoundSystem.Instance.Music("BackgroundMusic").Play();
    }

    private void Update()
    {
        if (player.gameObject.activeSelf)
            player.PlayerUpdate();

        enemyController.EnemiesUpdate();
    }

    private void FixedUpdate()
    {
        if (player.gameObject.activeSelf)
            player.PlayerFixedUpdate();

        enemyController.EnemiesFixedUpdate();
    }
}
