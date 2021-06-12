using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MenuButton
{
    public override void OnClick() {
        _audio.Play(sfxClick);
        _audio.FadeOut("Main Menu", AudioData.SoundType.Music);

        TransitionManager._.FadeIn(scene);
    }
}
