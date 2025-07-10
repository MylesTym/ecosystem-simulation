using System;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private PlayerAnimationController animationController;
    private PlayerState currentState;

    [Header("Camera")]
    public Transform cameraHolder;

    [Header("Movement")]
    public float walkSpeed = 4f;
    public float gravity = -9.81f;

    public Vector2 InputMove { get; private set; }
    public Vector2 InputLook { get; private set; }

    private float verticalSpeed;
    private float xRotation;

    public PlayerAnimationController Animation => animationController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animationController = GetComponent<PlayerAnimationController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SwitchState(new IdleState(this));
    }

    private void Update()
    {
        HandleInput();
        currentState.HandleInput();
        currentState.Update();
    }

    private void HandleInput()
    {
        InputMove = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        InputLook = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        float mouseX = InputLook.x;
        float mouseY = InputLook.y;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    public void Move(Vector3 motion)
    {
        verticalSpeed += gravity * Time.deltaTime;
        motion.y = verticalSpeed;

        characterController.Move(motion * Time.deltaTime);

        if (characterController.isGrounded)
            verticalSpeed = 0f;
    }

    public void SwitchState(PlayerState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
}