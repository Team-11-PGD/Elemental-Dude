using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	public Sound[] SoundEffects;
	public Sound[] AmbianceSounds;
	public Sound[] MonsterSounds;
	public Sound[] GunSounds;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(this.gameObject);

		InitAmbianceAudioSources();
		InitGunSoundSources();
	}

	private void InitAmbianceAudioSources()
	{
		foreach (Sound s in AmbianceSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}

	private void InitGunSoundSources()
	{
		foreach (Sound s in GunSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}


	public void PlaySoundEffect(GameObject gameobj, string name)
	{
		Sound s = Array.Find(SoundEffects, sound => sound.name == name);
		if (s == null)
		{
			Debug.LogError("Oopsie woopsie, the sound: " + name + " does not exist!");
			return;
		}

		s.source = gameobj.gameObject.AddComponent<AudioSource>();
		s.source.clip = s.clip;

		s.source.volume = s.volume;
		s.source.pitch = s.pitch;
		s.source.loop = s.loop;

		s.source.Play();
		Destroy(s.source, s.source.clip.length);
	}

	public void PlayMonsterSound(GameObject gameobj, string name)
	{
		Sound s = Array.Find(MonsterSounds, sound => sound.name == name);
		if (s == null)
		{
			Debug.LogError("Oopsie woopsie, the sound: " + name + " does not exist!");
			return;
		}

		s.source = gameobj.gameObject.AddComponent<AudioSource>();
		s.source.clip = s.clip;

		s.source.volume = s.volume;
		s.source.pitch = s.pitch;
		s.source.loop = s.loop;

		s.source.Play();
		Destroy(s.source, s.source.clip.length);
	}

	public void PlayAmbiance(string name)
	{
		Sound s = Array.Find(AmbianceSounds, sound => sound.name == name);
		if (s == null)
		{
			Debug.LogError("Oopsie woopsie, the sound: " + name + " does not exist!");
			return;
		}
		s.source.Play();
	}

	public void PlayWeaponSound(string name)
	{
		Sound s = Array.Find(GunSounds, sound => sound.name == name);
		if (s == null)
		{
			Debug.LogError("Oopsie woopsie, the sound: " + name + " does not exist!");
			return;
		}

		if (s.randomPitch)
			s.source.pitch = UnityEngine.Random.Range(0.9f, 1.1f);

		s.source.Play();
	}
}
