using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {
    private KeyCode activateBuilderKey = KeyCode.B;
    public event Action ActivateBuilder, OnClicked;

    // EventSystem.current.IsPointerOverGameObject() will be true when the pointer is over a UI game object
    public bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();

    private void Update() {
        if (Input.GetKeyDown(activateBuilderKey)) {
            ActivateBuilder?.Invoke();
        }
        else if (Input.GetMouseButtonDown((int)MouseButton.Left)) {
            OnClicked?.Invoke();
        }
        // TODO: When defining the StopPlacement method, we don't want to hide the builder in every case
    }


}
