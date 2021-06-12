using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButton : MenuButton
{
    public override void OnClick() {
        _audio.Play(sfxClick);
        TransitionManager._.FadeIn(scene);
    }
}
