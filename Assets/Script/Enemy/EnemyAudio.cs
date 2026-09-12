using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
	[Header("受伤/死亡")]
	public AudioClip hitClip;

	public AudioClip deadClip;

	public AudioClip dyingClip;

	[Header("攻击")]
	public AudioClip slashClip;

	public AudioClip chargeClip;

	public AudioClip roarClip;

	public AudioClip diveLandClip;

	[Header("移动")]
	public AudioClip jumpClip;

	public AudioClip landClip;

	[Header("状态")]
	public AudioClip staggerClip;

	public AudioClip castShockwaveClip;

	private AudioSource source;

	private void Awake()
	{
		source = GetComponent<AudioSource>();
	}

	public void PlayHit()
	{
		source.PlayOneShot(hitClip);
	}

	public void PlayDead()
	{
		source.PlayOneShot(deadClip);
	}

	public void PlayDying()
	{
		source.PlayOneShot(dyingClip);
	}

	public void PlaySlash()
	{
		source.PlayOneShot(slashClip);
	}

	public void PlayCharge()
	{
		source.PlayOneShot(chargeClip);
	}

	public void PlayRoar()
	{
		source.PlayOneShot(roarClip);
	}

	public void PlayDiveLand()
	{
		source.PlayOneShot(diveLandClip);
	}

	public void PlayJump()
	{
		source.PlayOneShot(jumpClip);
	}

	public void PlayLand()
	{
		source.PlayOneShot(landClip);
	}

	public void PlayStagger()
	{
		source.PlayOneShot(staggerClip);
	}

	public void PlayCastShockwave()
	{
		source.PlayOneShot(castShockwaveClip);
	}
}
