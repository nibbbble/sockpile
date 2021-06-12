using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MenuButton : MonoBehaviour
{
    public Button button;
    public string scene;

    void Start() {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public abstract void OnClick();

    // public abstract void LoadScene();
}
