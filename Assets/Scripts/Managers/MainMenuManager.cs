using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public AudioData musicMainMenu;
    AudioManager _audio;

    void Start() {
        _audio = AudioManager.i;
        
        if (!_audio.CheckIfPlaying(musicMainMenu)) {
            _audio.Play(musicMainMenu);
        }
    }
}
