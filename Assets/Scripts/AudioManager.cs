using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField]
	private float ambientVolume;

	[SerializeField]
	private float effectsVolume;

	private static bool firstRun = true;
	private AudioSource ambient;
	private AudioSource gun;

	public static AudioManager instance;

	private void Awake()
	{
		if (!firstRun)
		{
			Destroy(this);
			return;
		}
		firstRun = false;
		DontDestroyOnLoad(this.gameObject);

		instance = this;
		InitAudioSources();
		PlayAmbientAudio(SoundLocation.AmbientSoundTest);
	}

	public enum SoundLocation
	{
		GunShotTest,
		AmbientSoundTest
	}

	public string GetSoundLocation(SoundLocation sl)
	{
		return "Audio/" + sl.ToString();
	}

	private void InitAudioSources()
	{
		ambient = this.gameObject.AddComponent<AudioSource>();
		gun = this.gameObject.AddComponent<AudioSource>();
	}

	public void PlayAmbientAudio(SoundLocation sl)
	{
		Debug.Log(GetSoundLocation(sl));
		ambient.clip = Resources.Load<AudioClip>(GetSoundLocation(sl));
		ambient.loop = true;
		ambient.volume = ambientVolume;
		ambient.Play();
	}

	public void PlayGunAudio(SoundLocation sl)
	{
		gun.loop = false;
		gun.volume = effectsVolume;
		gun.PlayOneShot(Resources.Load<AudioClip>(GetSoundLocation(sl)));
	}
}
