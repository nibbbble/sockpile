using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu]
public class AudioData : ScriptableObject
{
    [TextArea(3,10)]
    public string description;
    public AudioClip clip;
    [HideInInspector]
    public enum SoundType {
        Music,
        SFX
    }
    public SoundType soundType;
    public AudioMixerGroup group;
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(-2f, 2f)]
    public float pitch = 1f;
    public bool loop = false;
    [HideInInspector]
    public AudioSource source;

    private void OnEnable() {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }
}
