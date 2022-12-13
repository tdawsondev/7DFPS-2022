using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private PlayerInputActions playerControls;

    public bool Charging1 = false;
    public bool Charging2 = false;


    private void Awake()
    {
        playerControls = new PlayerInputActions();
        if (Instance != null)
        {
            Debug.LogWarning("More Than One Input Manager");
        }
        Instance = this;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnEnable()
    {
        playerControls.Enable();

    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // MOVEMENT-----------------------
    public Vector2 GetPlayerMovement()
    {
        return playerControls.Player.Movement.ReadValue<Vector2>();
    }
    public Vector2 GetMouseDelta()
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }
    public bool PlayerJumped()
    {
        return playerControls.Player.Jump.triggered;
    }
    public bool StartSprinting()
    {
        return playerControls.Player.Sprint.triggered;
    }
    public bool SprintReleased()
    {
        return playerControls.Player.Sprint.WasReleasedThisFrame();
    }

    // COMBAT-----------------------
    public bool IsCharging1()
    {
        return playerControls.Player.Shoot1.ReadValue<float>() > 0.1;
    }
    public bool IsUnReleasing1()
    {
        return playerControls.Player.Shoot1.WasReleasedThisFrame();
    }
    public bool IsCharging2()
    {
        return playerControls.Player.Shoot2.ReadValue<float>() > 0.1;
    }
    public bool IsUnReleasing2()
    {
        return playerControls.Player.Shoot2.WasReleasedThisFrame();
    }

    public bool PausePressed()
    {
        return playerControls.Player.Pause.triggered;
    }

    public bool PressedCrouch()
    {
        return playerControls.Player.Crouch.triggered;
    }
    public bool ReleasedCrouch()
    {
        return playerControls.Player.Crouch.WasReleasedThisFrame();
    }



}
