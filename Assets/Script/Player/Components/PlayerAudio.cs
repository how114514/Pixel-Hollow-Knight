using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
	[Header("攻击")]
	public AudioClip attack1Clip;

	public AudioClip attack2Clip;

	public AudioClip attackUpClip;

	public AudioClip attackDownClip;

	[Header("技能")]
	public AudioClip upRoarClip;

	public AudioClip castShockwaveClip;

	public AudioClip diveClip;

	public AudioClip diveLandClip;

	[Header("移动")]
	public AudioClip doubleJumpClip;

	public AudioClip dashClip;

	public AudioClip blackDashClip;

	[Header("受伤/恢复")]
	public AudioClip healClip;

	public AudioClip healChargeClip;

	public AudioClip hurtClip;

	[Header("拼刀")]
	public AudioClip clash1Clip;

	public AudioClip clash2Clip;

	[Header("黑冲充能")]
	public AudioClip blackDashReadyClip;

	private AudioSource source;

	private void Awake()
	{
		source = GetComponent<AudioSource>();
	}

	public void PlayAttack1()
	{
		source.PlayOneShot(attack1Clip);
	}

	public void PlayAttack2()
	{
		source.PlayOneShot(attack2Clip);
	}

	public void PlayAttackHorizontal()
	{
		source.PlayOneShot((Random.value < 0.5f) ? attack1Clip : attack2Clip);
	}

	public void PlayAttackUp()
	{
		source.PlayOneShot(attackUpClip);
	}

	public void PlayAttackDown()
	{
		source.PlayOneShot(attackDownClip);
	}

	public void PlayUpRoar()
	{
		source.PlayOneShot(upRoarClip);
	}

	public void PlayShockwave()
	{
		source.PlayOneShot(castShockwaveClip);
	}

	public void PlayDive()
	{
		source.PlayOneShot(diveClip);
	}

	public void PlayDiveLand()
	{
		source.PlayOneShot(diveLandClip);
	}

	public void PlayDoubleJump()
	{
		source.PlayOneShot(doubleJumpClip);
	}

	public void PlayDash()
	{
		source.PlayOneShot(dashClip);
	}

	public void PlayBlackDash()
	{
		source.PlayOneShot(blackDashClip);
	}

	public void PlayHeal()
	{
		source.PlayOneShot(healClip);
	}

	public void PlayHealCharge()
	{
		source.PlayOneShot(healChargeClip);
	}

	public void PlayHurt()
	{
		source.PlayOneShot(hurtClip);
	}

	public void PlayClash()
	{
		source.PlayOneShot((Random.value < 0.5f) ? clash1Clip : clash2Clip);
	}

	public void PlayBlackDashReady()
	{
		source.PlayOneShot(blackDashReadyClip);
	}
}
