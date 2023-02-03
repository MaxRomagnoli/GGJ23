using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound {

    [SerializeField] public string nameSound;
    [SerializeField] public AudioClip clip;
    [SerializeField] public AudioSource source;
    [SerializeField] public bool mute = false;
    [SerializeField] public bool bypassEff = false;
    [SerializeField] public bool bypassList = false;
    [SerializeField] public bool bypassRev = false;
    [SerializeField] public bool playOnAwake = false;
    [SerializeField] public bool loop = false;
    [SerializeField] public int priority = 1; //0 to 256
    [SerializeField] public float volume = 0.75f; //0 to 1f
    //public float volumeVariance = 0.1f; // 0 to 1f
    [SerializeField] public float pitch = 1f; // 0.1f to 3f
    //public float pitchVariance = 0.1f; // 0 to 1f
    [SerializeField] public float stereoPan = 0; //from -1 to 1
    [SerializeField] public float spatialBlend = 0; //from 0 to 1 (2D-3D)
    [SerializeField] public float reverbZoneMix = 1; //from 0 to 1.1f
    //3D STUFF//
    [SerializeField] public float dopplerLevel; //0 to 5
    [SerializeField] public float spread; //0 to 360
    [SerializeField] public float minDistance = 1;
    [SerializeField] public float maxDistance = 500f;
    [SerializeField] public AudioMixerGroup mixerGroup;

#if UNITY_EDITOR
    [SerializeField] public bool displaySound = true;
#endif

}
