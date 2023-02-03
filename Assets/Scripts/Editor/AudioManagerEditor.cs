using UnityEngine;
using UnityEditor;
using UnityEngine.Audio;

[CustomEditor(typeof(AudioManager))]
public class AudioManagerEditor : Editor {

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        AudioManager aManager = (AudioManager)target;

        //EditorGUI.BeginChangeCheck();

        //MIXER GROUP//
        aManager.mixerGroup = (AudioMixerGroup)EditorGUILayout.ObjectField("Mixer Group", aManager.mixerGroup, typeof(AudioMixerGroup), true);

        if (aManager.sounds == null)
            aManager.sounds = new System.Collections.Generic.List<Sound>();

        Sound soundToRemove = null;

        //DISPLAY ALL THE SOUNDS//
        foreach(Sound s in aManager.sounds)
        {
            GUILayout.BeginVertical("box");

            s.nameSound = EditorGUILayout.TextField("Name", s.nameSound);
            if (!s.displaySound)
            {
                //GUILayout.Label(s.nameSound);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Remove sound"))
                    soundToRemove = s;
                s.displaySound = GUILayout.Button("Show sound");
                GUILayout.EndHorizontal();
            }
            else
            {
                if (s.source != null)
                {
                    EditorGUILayout.ObjectField("Audio Source", s.source, typeof(AudioSource), false); //not possible to remove that to prevent errors
                    s.source.mute = EditorGUILayout.Toggle("Mute", s.source.mute);
                    s.source.bypassEffects = EditorGUILayout.Toggle("Bypass Effects", s.source.bypassEffects);
                    s.source.bypassListenerEffects = EditorGUILayout.Toggle("Bypass Listener Effects", s.source.bypassListenerEffects);
                    s.source.bypassReverbZones = EditorGUILayout.Toggle("Bypass Reverb Zones", s.source.bypassReverbZones);
                    s.source.playOnAwake = EditorGUILayout.Toggle("Play on Awake", s.source.playOnAwake);
                    s.source.loop = EditorGUILayout.Toggle("Loop", s.source.loop);
                    s.source.priority = (int)EditorGUILayout.Slider("Priority", s.source.priority, 0, 256);
                    s.source.volume = EditorGUILayout.Slider("Volume", s.source.volume, 0, 1f);
                    //s.volumeVariance = EditorGUILayout.Slider("volume variance", s.volumeVariance, 0, 1f);
                    s.source.pitch = EditorGUILayout.Slider("Pitch", s.source.pitch, 0.1f, 3f);
                    //s.pitchVariance = EditorGUILayout.Slider("pitch variance", s.pitchVariance, 0, 1f);
                    s.source.panStereo = EditorGUILayout.Slider("Stereo Pan", s.source.panStereo, -1f, 1f);
                    s.source.spatialBlend = EditorGUILayout.Slider("Spatial Blend (2D to 3D)", s.source.spatialBlend, 0, 1f);
                    s.source.reverbZoneMix = EditorGUILayout.Slider("Reverb Zone Mix", s.source.reverbZoneMix, 0, 1.1f);
                    if(s.source.spatialBlend > 0)
                    {
                        GUILayout.Label("3D Stuff", EditorStyles.boldLabel);
                        s.source.dopplerLevel = EditorGUILayout.Slider("Doppler Level", s.source.dopplerLevel, 0, 5f);
                        s.source.spread = EditorGUILayout.Slider("Doppler Level", s.source.spread, 0, 360f);
                        GUILayout.BeginHorizontal();
                        s.source.minDistance = EditorGUILayout.FloatField("Min Distance", s.source.minDistance);
                        s.source.maxDistance = EditorGUILayout.FloatField("Max Distance", s.source.maxDistance);
                        GUILayout.EndHorizontal();
                    }
                }
                else
                {
                    s.clip = (AudioClip)EditorGUILayout.ObjectField("Audio Clip", s.clip, typeof(AudioClip), true);
                    s.mute = EditorGUILayout.Toggle("Mute", s.mute);
                    s.bypassEff = EditorGUILayout.Toggle("Bypass Effects", s.bypassEff);
                    s.bypassList = EditorGUILayout.Toggle("Bypass Listener Effects", s.bypassList);
                    s.bypassRev = EditorGUILayout.Toggle("Bypass Reverb Zones", s.bypassRev);
                    s.playOnAwake = EditorGUILayout.Toggle("Play on Awake", s.playOnAwake);
                    s.loop = EditorGUILayout.Toggle("Loop", s.loop);
                    s.priority = (int)EditorGUILayout.Slider("Priority", s.priority, 0, 256);
                    s.volume = EditorGUILayout.Slider("Volume", s.volume, 0, 1f);
                    //s.volumeVariance = EditorGUILayout.Slider("Volume variance", s.volumeVariance, 0, 1f);
                    s.pitch = EditorGUILayout.Slider("Pitch", s.pitch, 0.1f, 3f);
                    //s.pitchVariance = EditorGUILayout.Slider("Pitch variance", s.pitchVariance, 0, 1f);
                    s.stereoPan = EditorGUILayout.Slider("Stereo Pan", s.stereoPan, -1f, 1f);
                    s.spatialBlend = EditorGUILayout.Slider("Spatial Blend (2D to 3D)", s.spatialBlend, 0, 1f);
                    s.reverbZoneMix = EditorGUILayout.Slider("Reverb Zone Mix", s.reverbZoneMix, 0, 1.1f);
                    if (s.spatialBlend > 0)
                    {
                        GUILayout.Label("3D Stuff", EditorStyles.boldLabel);
                        s.dopplerLevel = EditorGUILayout.Slider("Doppler Level", s.dopplerLevel, 0, 5f);
                        s.spread = EditorGUILayout.Slider("Doppler Level", s.spread, 0, 360f);
                        GUILayout.BeginHorizontal();
                        s.minDistance = EditorGUILayout.FloatField("Min Distance", s.minDistance);
                        s.maxDistance = EditorGUILayout.FloatField("Max Distance", s.maxDistance);
                        GUILayout.EndHorizontal();
                    }
                }
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Remove sound"))
                    soundToRemove = s;
                if (GUILayout.Button("Hide sound"))
                    s.displaySound = false;
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();

        }

        //REMOVE SOUND//
        if(soundToRemove != null)
        {
            //Undo.RegisterCompleteObjectUndo(aManager, "Remove Audio"); //work? dunno
            //Undo.RecordObject(aManager, "Remove Audio"); //dunno 2
            aManager.RemoveSound(soundToRemove);
            //Undo.FlushUndoRecordObjects();
        }

        //NEW AUDIO SOURCE//
        AudioSource newAudioSource = null;
        GUILayout.BeginVertical("box");
        GUILayout.Label("Drag new 3D audio source or create one", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        newAudioSource = (AudioSource)EditorGUILayout.ObjectField(newAudioSource, typeof(AudioSource), true);
        if (newAudioSource != null)
            aManager.AddSound(newAudioSource);
        if(GUILayout.Button("Add a new sound"))
            aManager.AddSound();
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();

        //UNDO STUFF//
        /*if(EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Change");
        }*/
        /*if(GUI.changed)
        {
            Undo.CreateSnapshot();
            Undo.RegisterSnapshot();
        }*/

    }
}
