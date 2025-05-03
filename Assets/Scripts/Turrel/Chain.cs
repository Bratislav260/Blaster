using UnityEngine;

public class Chain : Turrel
{
    private bool isAvaiable = false;
    private Player player;

    public override void Activate()
    {
        isAvaiable = true;
    }

    public override void Diactivate()
    {
        isAvaiable = false;

        if (player != null)
            DisableClimbMode();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isAvaiable)
        {
            player = collision.gameObject.GetComponent<Player>();
            player.inputHandler.SwitchTheMoveToClimb();
            player.climbController.SetClimbMode();
            player.climbController.chainsCount += 1;

            // Debug.Log($"CHAINED {player.climbController.chainsCount}");
            SoundSystem.Instance.Sound("Chain").Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isAvaiable)
        {
            // Debug.Log($"UN-CHAINED {player.climbController.chainsCount}");
            if (player.climbController.chainsCount < 2)
            {
                DisableClimbMode();
            }
            else
            {
                player.climbController.chainsCount -= 1;
            }
        }
    }

    private void DisableClimbMode()
    {
        player.inputHandler.SwitchTheMoveToClimb(false);
        player.climbController.ResetClimbMode();
        player.climbController.chainsCount = 0;
    }
}
