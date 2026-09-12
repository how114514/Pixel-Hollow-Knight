using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
	private PlayerInputControl inputControl;

	public Vector2 MoveInput { get; private set; }

	public bool JumpPressed { get; private set; }

	public bool JumpHeld { get; private set; }

	public bool DashPressed { get; private set; }

	public bool AttackPressed { get; private set; }

	public bool SkillPressed { get; private set; }

	public bool HealHeld { get; private set; }

	public bool RestartPressed { get; private set; }

	private void Awake()
	{
		inputControl = new PlayerInputControl();
	}

	private void OnEnable()
	{
		inputControl.Enable();
		inputControl.GamePlay.Move.performed += OnMove;
		inputControl.GamePlay.Move.canceled += OnMove;
		inputControl.GamePlay.Jump.started += OnJump;
		inputControl.GamePlay.Jump.canceled += OnJump;
		inputControl.GamePlay.Dash.started += OnDash;
		inputControl.GamePlay.Attack.started += OnAttack;
		inputControl.GamePlay.Skill.started += OnSkill;
		inputControl.GamePlay.Heal.started += OnHeal;
		inputControl.GamePlay.Heal.canceled += OnHeal;
		inputControl.GamePlay.ReStart.started += OnRestart;
	}

	private void OnDisable()
	{
		inputControl.GamePlay.Move.performed -= OnMove;
		inputControl.GamePlay.Move.canceled -= OnMove;
		inputControl.GamePlay.Jump.started -= OnJump;
		inputControl.GamePlay.Jump.canceled -= OnJump;
		inputControl.GamePlay.Dash.started -= OnDash;
		inputControl.GamePlay.Attack.started -= OnAttack;
		inputControl.GamePlay.Skill.started -= OnSkill;
		inputControl.GamePlay.Heal.started -= OnHeal;
		inputControl.GamePlay.Heal.canceled -= OnHeal;
		inputControl.GamePlay.ReStart.started -= OnRestart;
		inputControl.Disable();
	}

	private void LateUpdate()
	{
		JumpPressed = false;
		DashPressed = false;
		AttackPressed = false;
		SkillPressed = false;
		RestartPressed = false;
	}

	private void OnMove(InputAction.CallbackContext ctx)
	{
		MoveInput = ctx.ReadValue<Vector2>();
	}

	private void OnJump(InputAction.CallbackContext ctx)
	{
		if (ctx.started)
		{
			JumpPressed = true;
		}
		JumpHeld = !ctx.canceled;
	}

	private void OnDash(InputAction.CallbackContext ctx)
	{
		DashPressed = true;
	}

	private void OnAttack(InputAction.CallbackContext ctx)
	{
		AttackPressed = true;
	}

	private void OnSkill(InputAction.CallbackContext ctx)
	{
		SkillPressed = true;
	}

	private void OnHeal(InputAction.CallbackContext ctx)
	{
		HealHeld = ctx.started;
	}

	private void OnRestart(InputAction.CallbackContext ctx)
	{
		RestartPressed = true;
	}
}
