using System;
using UnityEngine;

//Mees
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
			instance.transform.parent = null;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(this.gameObject);

		InitWorldAudioSources(AmbianceSounds);
		InitWorldAudioSources(GunSounds);
		InitWorldAudioSources(UISounds);
	}

	private void InitWorldAudioSources(Sound[] soundArray)
	{
		foreach (Sound s in soundArray)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}

	/// <summary>
	/// Play sound on the position of a Gameobject.
	/// </summary>
	/// <param name="SoundArray">Array the sound you want to play is stored in</param>
	/// <param name="gameobj">Gameobject the sound is played from</param>
	/// <param name="name">Name of the sound you want to play</param>
	public void PlaySoundFromObject(Sound[] SoundArray, GameObject gameobj, string name)
	{
		Sound s = Array.Find(SoundArray, sound => sound.name == name);
		if (s == null)
		{
			Debug.LogError("Oopsie! The sound: " + name + " does not exist!");
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

	/// <summary>
	/// Play sound from the Audio Manager object.
	/// </summary>
	/// <param name="SoundArray">Array the sound you want to play is stored in</param>
	/// <param name="name">Name of the sound you want to play</param>
	public void PlaySoundFromWorld(Sound[] SoundArray, string name)
	{
		Sound s = Array.Find(SoundArray, sound => sound.name == name);
		if (s == null)
		{
			Debug.LogError("Oopsie! The sound: " + name + " does not exist!");
			return;
		}
		s.source.clip = s.clip;

		s.source.volume = s.volume;
		s.source.pitch = s.pitch;
		s.source.loop = s.loop;

		if (s.randomPitch)
			s.source.pitch = UnityEngine.Random.Range(0.9f, 1.1f);

		s.source.Play();
	}

	/// <summary>
	/// Stop the sound from the Audio Manager object.
	/// </summary>
	/// <param name="SoundArray">Array the sound you want to stop is stored in</param>
	/// <param name="name">Name of the sound you want to stop</param>
	public void StopSoundFromWorld(Sound[] SoundArray, string name)
	{
		Sound s = Array.Find(SoundArray, sound => sound.name == name);
		if (s == null)
		{
			Debug.LogError("Oopsie! The sound: " + name + " does not exist!");
			return;
		}
		s.source.Stop();
	}
}
