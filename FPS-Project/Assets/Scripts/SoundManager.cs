using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager soundManager;
    void Awake () {
        if (soundManager == null) {
            soundManager = this;
        } else if (soundManager != this) {
            Destroy (gameObject);
        }

        DontDestroyOnLoad (gameObject);
    }

    // Update is called once per frame
    void Update () {

    }
}