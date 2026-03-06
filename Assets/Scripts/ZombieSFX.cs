using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSFX : MonoBehaviour
{
    AudioSource audioSource;

	public AudioClip Idle;
	public AudioClip Chase;
	public AudioClip Attack;
	public AudioClip Die;

	public string zombieType;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();

		zombieType = this.gameObject.tag;

		Debug.Log(zombieType);
	}
}
