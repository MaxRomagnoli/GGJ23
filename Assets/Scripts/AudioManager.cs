using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;
	public AudioMixerGroup mixerGroup;
	public List<Sound> sounds;

	void Awake()
	{
		if (instance == null)
		{ instance = this; DontDestroyOnLoad(this.gameObject); }
		else
		{ Destroy(this.gameObject); }

        if (sounds == null)
            sounds = new List<Sound>();

        foreach (Sound s in sounds)
		{
            if(s.source == null)
            {
                s.source = this.gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.mute = s.mute;
                s.source.bypassEffects = s.bypassEff;
                s.source.bypassListenerEffects = s.bypassList;
                s.source.bypassReverbZones = s.bypassRev;
                s.source.playOnAwake = s.playOnAwake;
			    s.source.loop = s.loop;
                s.source.priority = s.priority;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.panStereo = s.stereoPan;
                s.source.spatialBlend = s.spatialBlend;
                s.source.reverbZoneMix = s.reverbZoneMix;
                //3D STUFF//
                s.source.dopplerLevel = s.dopplerLevel;
                s.source.spread = s.spread;
                s.source.minDistance = s.minDistance;
                s.source.maxDistance = s.maxDistance;
            }
            if(mixerGroup != null)
                s.source.outputAudioMixerGroup = mixerGroup;
            if (s.playOnAwake)
                Play(s.nameSound);
        }
	}

    public void Play(string sound)
    {
        Sound s = Array.Find(sounds.ToArray(), item => item.nameSound == sound);
        if (s == null)
        { Debug.LogWarning("Sound: " + name + " not found!"); return; }
        //s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        //s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
        s.source.Play();
    }

    public void Stop(string sound)
    {
        Sound s = Array.Find(sounds.ToArray(), item => item.nameSound == sound);
        if (s == null)
        { Debug.LogWarning("Sound: " + name + " not found!"); return; }
        s.source.Stop();
    }

    public void SetVolume(string sound, float volume)
    {
        Sound s = Array.Find(sounds.ToArray(), item => item.nameSound == sound);
        if (s == null)
        { Debug.LogWarning("Sound: " + name + " not found!"); return; }
        s.source.volume = volume;
    }

    public void StopAll()
    {
        foreach(Sound s in sounds)
        { s.source.Stop(); }
    }

#if UNITY_EDITOR
    public void AddSound() //by inspector
    {
        Sound newSound = new Sound();
        newSound.nameSound = "New Sound";
        sounds.Add(newSound);
    }

    public void AddSound(AudioSource soundToAdd) //by inspector
    {
        Sound newSound = new Sound();
        newSound.nameSound = soundToAdd.name;
        newSound.source = soundToAdd;
        sounds.Add(newSound);
    }

    public void RemoveSound(Sound soundToRemove) //by inspector
    {
        sounds.Remove(soundToRemove);
    }
#endif

}
