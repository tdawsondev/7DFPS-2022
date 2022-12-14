using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    float smoothSensitivity = 2f;
    [Header("Sprinting")]
    private bool sprinting = false;
    [SerializeField]
    private float sprintSpeed = 10f;
    [SerializeField]
    private float sprintSmooth = 1f;
    private float sprintRef;
    [SerializeField]
    private float fovChange = 5f;
    private float fovRef;
    private float defaultFOV;
    private Transform cameraTransform;

    [Header("Crouching")]
    private bool crouching = false;
    [SerializeField]
    private float crouchHeight;
    [SerializeField] 
    private float crouchSmooth;
    [SerializeField]
    private float crouchWalkSpeed;
    private float crouchOffset;
    private float defaultHeight;
    private float crouchRef;
    private float crouchOffsetRef;
    

    [Header("Jumping")]
    public int currentJumps = 2;
    public int maxJumps = 2;
    [SerializeField]
    private bool groundedPlayer;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    float _valx, _valz; // used in input smoothing

    public bool movementDisabled = false;

    private float movementSpeed;


    [Header("Audio")]
    public float loopTime;
    private float currentLoopTime;
    private int lastSound = 0;
    private bool audioPlaying;


    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        currentJumps = maxJumps;
        cameraTransform = Camera.main.transform;
        movementSpeed = playerSpeed;
        defaultFOV = Camera.main.fieldOfView;
        defaultHeight = controller.height;
        crouchOffset = (defaultHeight - crouchHeight) / 2f; // where does the new center of the controller have to be
        currentLoopTime = loopTime;
        audioPlaying = false;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer)
        {
            currentJumps = maxJumps;
        }

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -1f;
        }
        if (currentJumps == 2 && !groundedPlayer)
        {
            currentJumps--;
        }
        if (InputManager.Instance.StartSprinting())
        {
            sprinting = true;
        }
        if (InputManager.Instance.SprintReleased())
        {
            sprinting = false;
        }
        if (InputManager.Instance.PressedCrouch())
        {
            crouching = true;
            sprinting = false;
        }
        if (InputManager.Instance.ReleasedCrouch())
        {
            crouching = false;
        }
        

        Vector2 input = GetSmoothedInput();
        if(input.magnitude > 0f && !audioPlaying)
        {
            //start audio
            StartCoroutine(AuidoLoop());
        }
        
        if(input.magnitude <= 0f)
        {
            //stop audio
            audioPlaying = false;
            StopAllCoroutines();
        }
        GetMovementSpeed(input);

        Vector3 move = new Vector3(input.x, 0, input.y);
        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0f;
        camForward = camForward.normalized; // get cam forward, disregarding y
        Vector3 camRight = cameraTransform.right;
        camRight.y = 0f;
        camRight = camRight.normalized; // get cam right, disregarding y
        move = camForward * move.z + camRight * move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * movementSpeed);


       // jumps. built to allow for double joints if thats an option we want.
        if (InputManager.Instance.PlayerJumped() && currentJumps != 0)
        {
            currentJumps--;
            playerVelocity.y = 0f;
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    /// <summary>
    /// Sets movment speed. Smooths into sprint speed
    /// </summary>
    public void GetMovementSpeed(Vector2 input)
    {
        
        if (sprinting)
        {
            if (input.magnitude > 0)
            {
                Camera.main.fieldOfView = Mathf.SmoothDamp(Camera.main.fieldOfView, defaultFOV + fovChange, ref fovRef, sprintSmooth);
            }
            else
            {
                Camera.main.fieldOfView = Mathf.SmoothDamp(Camera.main.fieldOfView, defaultFOV, ref fovRef, sprintSmooth);
            }
            movementSpeed = Mathf.SmoothDamp(movementSpeed, sprintSpeed, ref sprintRef, sprintSmooth);
        }
        else if (crouching)
        {
            Camera.main.fieldOfView = Mathf.SmoothDamp(Camera.main.fieldOfView, defaultFOV, ref fovRef, sprintSmooth);
            movementSpeed = Mathf.SmoothDamp(movementSpeed, crouchWalkSpeed, ref sprintRef, sprintSmooth / 2f);
            controller.height = Mathf.SmoothDamp(controller.height, crouchHeight, ref crouchRef, crouchSmooth);
            controller.center = new Vector3(0, Mathf.SmoothDamp(controller.center.y, crouchOffset, ref crouchOffsetRef, crouchSmooth), 0);
        }
        else
        {
            Camera.main.fieldOfView = Mathf.SmoothDamp(Camera.main.fieldOfView, defaultFOV, ref fovRef, sprintSmooth);
            movementSpeed = Mathf.SmoothDamp(movementSpeed, playerSpeed, ref sprintRef, sprintSmooth/2f);
        }
        if (!crouching) // for readablity
        {
            controller.height = Mathf.SmoothDamp(controller.height, defaultHeight, ref crouchHeight, crouchSmooth);
            controller.center = new Vector3(0, Mathf.SmoothDamp(controller.center.y, 0, ref crouchOffsetRef, crouchSmooth), 0);
        }
    }


    /// <summary>
    /// Smooth The input.
    /// </summary>
    /// <returns> Current direction but super smooth.</returns>
    private Vector2 GetSmoothedInput()
    {
        if (movementDisabled) return Vector2.zero;
        float dead = 0.001f;
        Vector2 input = InputManager.Instance.GetPlayerMovement().normalized;
        float rX = 0, ry = 0;

        //Horizontal
        float targetx = input.x;
        _valx = Mathf.MoveTowards(_valx, targetx, smoothSensitivity * Time.unscaledDeltaTime);
        rX = (Mathf.Abs(_valx) < dead) ? 0f : _valx;

        //Vertical
        float targety = input.y;
        _valz = Mathf.MoveTowards(_valz, targety, smoothSensitivity * Time.unscaledDeltaTime);
        ry = (Mathf.Abs(_valz) < dead) ? 0f : _valz;

        return new Vector2(rX, ry);

    }

    public void PlayFootStepNoise()
    {
        lastSound = lastSound + 1;
        if(lastSound> 5)
        {
            lastSound = 1;
        }
        AudioManager.instance.Play("Footsteps" + lastSound);
    }

    public IEnumerator AuidoLoop()
    {
        audioPlaying = true;
        while (true)
        {
            if (!groundedPlayer)
            {
                currentLoopTime = 0;
            }
            else if (sprinting)
            {
                float speedIncrease = sprintSpeed / playerSpeed;
                currentLoopTime = loopTime * speedIncrease;
            }
            else if (crouching)
            {
                float speedIncrease = crouchWalkSpeed / playerSpeed;
                currentLoopTime = loopTime * speedIncrease;
            }
            else
            {
                currentLoopTime = loopTime;
            }

            if (currentLoopTime > 0)
            {
                PlayFootStepNoise();
                yield return new WaitForSeconds(1 / currentLoopTime);
            }
            else yield return null;
        }
    }

}
