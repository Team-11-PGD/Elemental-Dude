using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	public Sound[] SoundEffects;
	public Sound[] PlayerSounds;
	public Sound[] AmbianceSounds;
	public Sound[] MonsterSounds;
	public Sound[] GunSounds;
	public Sound[] UISounds;

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

		InitWorldAudioSources();
	}

	private void InitWorldAudioSources()
	{
		foreach (Sound s in AmbianceSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}

		foreach (Sound s in GunSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}

		foreach (Sound s in UISounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}

	public void PlaySoundFromObject(Sound[] SoundArray, GameObject gameobj, string name)
	{
		Sound s = Array.Find(SoundArray, sound => sound.name == name);
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

		if (s.randomPitch)
			s.source.pitch = UnityEngine.Random.Range(0.9f, 1.1f);

		s.source.Play();
		Destroy(s.source, s.source.clip.length);
	}

	public void PlaySoundFromWorld(Sound[] SoundArray, string name)
	{
		Sound s = Array.Find(SoundArray, sound => sound.name == name);
		if (s == null)
		{
			Debug.LogError("Oopsie woopsie, the sound: " + name + " does not exist!");
			return;
		}

		if (s.randomPitch)
			s.source.pitch = UnityEngine.Random.Range(0.9f, 1.1f);

		s.source.Play();
	}

	public void StopSoundFromWorld(Sound[] SoundArray, string name)
	{
		Sound s = Array.Find(SoundArray, sound => sound.name == name);
		if (s == null)
		{
			Debug.LogError("Oopsie woopsie, the sound: " + name + " does not exist!");
			return;
		}
		s.source.Stop();
	}
}
