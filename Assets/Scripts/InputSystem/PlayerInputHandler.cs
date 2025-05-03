using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler
{
    public InputControls inputControls { get; private set; }
    private Player player;

    public Vector2 MoveInputVector { get; private set; }
    public float mouseScrollY { get; private set; }
    private bool isReady = false;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction attackAction;
    private InputAction mouseScrollAction;
    private InputAction climbAction;

    public PlayerInputHandler(Player player)
    {
        this.player = player;
        inputControls = new InputControls();

        moveAction = inputControls.Player.Movement;
        jumpAction = inputControls.Player.Jump;
        attackAction = inputControls.Player.Attack;
        mouseScrollAction = inputControls.Player.MouseScroll;

        climbAction = inputControls.Player.Climb;

        PlayerRegister();
        isReady = true;
        OnEnable();

        climbAction.Disable();
    }

    #region - Enable / Disable -
    private void OnEnable()
    {
        if (!isReady)
            return;

        inputControls.Player.Enable();
    }
    public void OnDisable()
    {
        PlayerUnRegister();
        inputControls.Disable();
    }
    #endregion

    /// <summary>
    /// Переключение с ходьбы на карабканье
    /// </summary>
    public void SwitchTheMoveToClimb(bool isClimb = true)
    {
        if (isClimb)
        {
            moveAction.Disable();
            jumpAction.Disable();
            climbAction.Enable();
        }
        else
        {
            climbAction.Disable();
            jumpAction.Enable();
            moveAction.Enable();
        }
    }

    private void PlayerRegister()
    {
        moveAction.performed += context => MoveInputVector = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInputVector = Vector2.zero;

        climbAction.performed += context => MoveInputVector = context.ReadValue<Vector2>();
        climbAction.canceled += context => MoveInputVector = Vector2.zero;

        jumpAction.performed += context => player.playerMovement.Jump();
        attackAction.performed += context => player.attackController.Attack();
        attackAction.canceled += context => player.attackController.AttackEnd();

        mouseScrollAction.performed += context => mouseScrollY = context.ReadValue<float>();
        mouseScrollAction.canceled += context => mouseScrollY = 0f;
    }

    private void PlayerUnRegister()
    {
        moveAction.performed -= context => MoveInputVector = context.ReadValue<Vector2>();
        moveAction.canceled -= context => MoveInputVector = Vector2.zero;

        climbAction.canceled += context => MoveInputVector = Vector2.zero;
        climbAction.performed += context => MoveInputVector = context.ReadValue<Vector2>();

        jumpAction.performed -= context => player.playerMovement.Jump();
        attackAction.performed -= context => player.attackController.Attack();
        attackAction.canceled -= context => player.attackController.AttackEnd();

        mouseScrollAction.performed -= context => mouseScrollY = context.ReadValue<float>();
        mouseScrollAction.canceled -= context => mouseScrollY = 0f;
    }

}