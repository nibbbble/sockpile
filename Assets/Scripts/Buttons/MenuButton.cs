using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MenuButton : MonoBehaviour
{
    public Button button;
    public string scene;
    public AudioData sfxClick;
    [HideInInspector]
    public AudioManager _audio;

    public virtual void Start() {
        _audio = AudioManager.i;
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public virtual void OnClick() {
        //_audio.Play("Click", AudioData.SoundType.SFX);
    }

    // public abstract void LoadScene();
}
