// audio manager / audio controller
// under exclusive use by nibbbble incorporated 2021

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
class AudioObject {
    public string name;
    public AudioClip clip;
    public AudioData.SoundType soundType;
    public AudioMixerGroup group;
    [Range(0f, 1f)]
    public float volume;
    [Range(-2f, 2f)]
    public float pitch;
    public bool loop;
    [HideInInspector]
    public AudioSource source;

    public void SetFromData(AudioData data) {
        name = data.name;
        clip = data.clip;
        soundType = data.soundType;
        group = data.group;
        volume = data.volume;
        pitch = data.pitch;
        loop = data.loop;
    }

    public void SetSource(AudioSource _source) {
        source = _source;
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.loop = loop;
        source.outputAudioMixerGroup = group;
    }

    public void Play() {
        source.Play();
    }

    public void Stop() {
        source.Stop();
    }

    public void Pause() {
        source.Pause();
    }

    public void Resume() {
        source.UnPause();
    }

    public void FadeOut(float waitTime) {
        AudioManager.i.StartCoroutine(FadeOutCore(waitTime));
    }

    public bool IsPlaying() {
        if (source.isPlaying) return true; 
        else return false;
    }

    IEnumerator FadeOutCore(float waitTime) {
        float startVolume = source.volume;
        while (source.volume > 0) {
            source.volume -= startVolume * Time.deltaTime / waitTime;
            yield return null;
        }

        source.Stop();
        source.volume = startVolume;
    }
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager i;
    
    #pragma warning disable 0649
    List<AudioObject> audioObjects;
    #pragma warning restore 0649

    void Awake() {
        if (i != null) {
            if (i != this) {
                Destroy(this.gameObject);
            }
        } else {
            i = this;
            DontDestroyOnLoad(gameObject);
        }

        // i saw somewhere you were supposed to do this
        audioObjects = new List<AudioObject>();
        audioObjects.Add(new AudioObject());

        // trying to use resources.loadall instead of putting it in MANUALLY god help me
        // lollll what am i doing
        var music = Resources.LoadAll("Audio Data/Music", typeof(AudioData))
            .Cast<AudioData>().ToArray();
        var sfx = Resources.LoadAll("Audio Data/SFX", typeof(AudioData))
            .Cast<AudioData>().ToArray();

        for (int i = 0; i < music.Length; i++) {
            // creates gameobject that will hold the audiosource
            GameObject go = new GameObject("Music_" + i + "_" + music[i].name);
            go.transform.SetParent(this.transform);

            // creates an audioobject
            AudioObject ao = new AudioObject();
            ao.SetFromData(music[i]); // maps data
            ao.SetSource(go.AddComponent<AudioSource>()); // sets source
            audioObjects.Add(ao); // add to list
        }

        for (int i = 0; i < sfx.Length; i++) {
            GameObject go = new GameObject("SFX_" + i + "_" + sfx[i].name);
            go.transform.SetParent(this.transform);

            AudioObject ao = new AudioObject();
            ao.SetFromData(sfx[i]);
            ao.SetSource(go.AddComponent<AudioSource>());
            audioObjects.Add(ao);
        }
    }

    // -----------------------------------------------------------------
    // main methods

    public void Play(string name, AudioData.SoundType type) {
        // finds sound with name
        AudioObject sound = audioObjects.Find(x => x.name == name);

        // does the thing
        if (sound != null) {
            if (sound.loop) {
                if (!sound.IsPlaying()) sound.Play();
            } else sound.Play();
            return;
        }

        Debug.LogWarningFormat("AudioManager: No sound found with name {0}.", name);
    }

    public void Stop(string name, AudioData.SoundType type) {
        AudioObject sound = audioObjects.Find(x => x.name == name);
        if (sound != null) {
            sound.Stop();
            return;
        }
        
        Debug.LogWarningFormat("AudioManager: No sound found with name {0}.", name);
    }

    public void Pause(string name, AudioData.SoundType type) {
        AudioObject sound = audioObjects.Find(x => x.name == name);
        if (sound != null) {
            /*if (sound.source.isPlaying)*/ sound.Pause();
            return;
        }

        Debug.LogWarningFormat("AudioManager: No sound found with name {0}.", name);
    }

    public void Resume(string name, AudioData.SoundType type) {
        AudioObject sound = audioObjects.Find(x => x.name == name);
        if (sound != null) {
            /*if (!sound.source.isPlaying)*/ sound.Resume();
            return;
        }

        Debug.LogWarningFormat("AudioManager: No sound found with name {0}.", name);
    }

    public void FadeOut(string name, AudioData.SoundType type, float waitTime = 1f) {
        AudioObject sound = audioObjects.Find(x => x.name == name);
        if (sound != null) {
            if (sound.source.isPlaying) sound.FadeOut(waitTime);
            return;
        }

        Debug.LogWarningFormat("AudioManager: No sound found with name {0}.", name);
    }

    public bool CheckIfPlaying(string name, AudioData.SoundType type) {
        AudioObject sound = audioObjects.Find(x => x.name == name);
        if (sound != null) {
            if (sound.IsPlaying()) return true;
            else return false;
        }

        Debug.LogWarningFormat("AudioManager: No sound found with name {0}.", name);
        return false;
    }

    // -----------------------------------------------------------------
    // overloads for audiodata objects
    // so glad that i found out i could do this
    //

    public void Play(AudioData sound, bool isOneShot = false) {
        AudioObject _sound = audioObjects.Find(x => x.name == sound.name);
        
        // if (_sound.loop) {
        //     if (!_sound.IsPlaying())
        //         if (isOneShot) _sound.PlayOneShot();
        //         else _sound.Play();
        // } else _sound.Play();

        if (_sound.loop) {
            if (!_sound.IsPlaying()) _sound.Play();
        } else _sound.Play();
    }

    public void Stop(AudioData sound) {
        AudioObject _sound = audioObjects.Find(x => x.name == sound.name);
        _sound.Stop();
    }

    public void Pause(AudioData sound) {
        AudioObject _sound = audioObjects.Find(x => x.name == sound.name);
        _sound.Pause();
    }

    public void Resume(AudioData sound) {
        AudioObject _sound = audioObjects.Find(x => x.name == sound.name);
        _sound.Resume();
    }

    public void FadeOut(AudioData sound, float waitTime = 1f) {
        AudioObject _sound = audioObjects.Find(x => x.name == sound.name);
        if (_sound.source.isPlaying) _sound.FadeOut(waitTime);
    }

    public bool CheckIfPlaying(AudioData sound) {
        AudioObject _sound = audioObjects.Find(x => x.name == sound.name);
        if (_sound.IsPlaying()) return true;
        else return false;
    }
}
