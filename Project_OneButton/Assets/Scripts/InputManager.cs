using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-5)] // This script runs FIRST
public class InputManager : Singleton<InputManager>
{
    // Events
    public delegate void StartTelegraphInput(bool isPressing);
    public event StartTelegraphInput OnStartTelegraphInput;

    private InputControls controls;

    new void Awake()
    {
        base.Awake();

        controls = new InputControls();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        controls.Player.TelegraphInput.started += PressInput;
        controls.Player.TelegraphInput.canceled += ReleaseInput;
    }

    #region Successful Inputs

    private void PressInput(InputAction.CallbackContext context)
    {
        if (OnStartTelegraphInput == null) return;

        OnStartTelegraphInput(true);
    }

    private void ReleaseInput(InputAction.CallbackContext context)
    {
        if (OnStartTelegraphInput == null) return;

        OnStartTelegraphInput(false);
    }


    #endregion


}
