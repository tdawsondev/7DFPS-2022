using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls first person mouse looking. Should be attached to main camera.
/// </summary>
public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 10f;
    public float smoothing = 0.01f;
    public bool lookLocked;
    [SerializeField] private Transform playerBody;

    private Vector2 smoothedVelocity;
    private Vector2 currentLookingPos;
    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // LateUpdate is called at the end of every frame
    void LateUpdate()
    {
        if (!lookLocked)
        {
            Vector2 inputValues = InputManager.Instance.GetMouseDelta();
            inputValues = Vector2.Scale(inputValues, new Vector2(mouseSensitivity * smoothing, mouseSensitivity * smoothing));

            smoothedVelocity = Vector2.Lerp(smoothedVelocity, inputValues, (1f / smoothing) * Time.deltaTime);

            currentLookingPos += smoothedVelocity;
            currentLookingPos.y = Mathf.Clamp(currentLookingPos.y, -89f, 89f);

            transform.localRotation = Quaternion.AngleAxis(-currentLookingPos.y, Vector3.right);
            playerBody.localRotation = Quaternion.AngleAxis(currentLookingPos.x, playerBody.up);

        }
    }

    public void LockLook()
    {
        Cursor.lockState = CursorLockMode.None;
        lookLocked = true;
    }
    public void UnlockLook()
    {
        Cursor.lockState = CursorLockMode.Locked;
        lookLocked = false;
    }
}
