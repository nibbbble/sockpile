using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsButton : MenuButton
{
    public override void OnClick() {
        _audio.Play(sfxClick);

        TransitionManager._.FadeIn(scene);
    }
}
