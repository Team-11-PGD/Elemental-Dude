using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	public Sound[] SoundEffects;
	public Sound[] AmbianceSounds;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(this.gameObject);

		InitAmbianceAudioSources();
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

	public void PlayAmbiance(string name)
	{
		Sound s = Array.Find(SoundEffects, sound => sound.name == name);
		if (s == null)
		{
			Debug.LogError("Oopsie woopsie, the sound: " + name + " does not exist!");
			return;
		}
		s.source.Play();
	}
}
