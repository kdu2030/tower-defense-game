using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    //public event Action OnClicked, OnExit;
    private KeyCode activateBuilderKey = KeyCode.B;
    public event Action ActivateBuilder;

    private void Update() {
        if (Input.GetKeyDown(activateBuilderKey)) {
            ActivateBuilder.Invoke();
        }
    }


}
