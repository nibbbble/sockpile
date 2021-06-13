using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButton : MenuButton
{
    public GameObject creditsScreen;
    bool open;
    // new AudioManager _audio;

    public override void Start() {
        // _audio = AudioManager.i;
        base.Start();

        creditsScreen.SetActive(false);
        open = false;

        // button.onClick.AddListener(OnClick);
    }
    
    public override void OnClick() {
        _audio.Play(sfxClick);
        // base.OnClick();
        
        if (!open) {
            open = true;
        } else {
            open = false;
        }
        creditsScreen.SetActive(open);
    }
}
