using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public CharacterController characterController;
    public Animator animator;
    public float speed = 3;
    public Image movementBar;
    public float movementBarAmount = 20;

    float motionSmoothTime = 0.1f;
    private Vector3 moveDirection = Vector3.zero;

    // camera and rotation
    public Transform cameraHolder;
    public float mouseSensitivity = 2f;
    public float upLimit = -50;
    public float downLimit = 50;

    // gravity
    private float gravity = 9.87f;

    void Awake()
    {
        GameManager.onGameStateChanged += GameManagerOnGameStateChanged;
    }

    void onDestroy()
    {
        GameManager.onGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.CoinToss:
                ResetMovementPoints();
                break;
        }
    }

    void Update()
    {
        if (GameObject.ReferenceEquals(gameObject, GameManager.Instance._character))
        {
            Rotate();
            Move();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                FinishMovement();
            }
        }
    }

    private void Rotate()
    {
        float horizontalRotation = Input.GetAxis("Mouse X");
        float verticalRotation = Input.GetAxis("Mouse Y");

        transform.Rotate(0, horizontalRotation * mouseSensitivity, 0);
        cameraHolder.Rotate(-verticalRotation * mouseSensitivity, 0, 0);

        Vector3 currentRotation = cameraHolder.localEulerAngles;
        if (currentRotation.x > 180)
        {
            currentRotation.x -= 360;
        }
        currentRotation.x = Mathf.Clamp(currentRotation.x, upLimit, downLimit);
        cameraHolder.localRotation = Quaternion.Euler(currentRotation);
    }

    private void Move()
    {
        if (movementBar.fillAmount > 0.0f)
        {
            if (characterController.isGrounded)
            {
                moveDirection = new Vector3(
                    Input.GetAxis("Horizontal"),
                    0,
                    Input.GetAxis("Vertical")
                );
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;
            }

            moveDirection.y -= gravity * Time.deltaTime;

            characterController.Move(moveDirection * Time.deltaTime);

            Vector3 horizontalVelocity = characterController.velocity;
            horizontalVelocity = new Vector3(
                characterController.velocity.x,
                0,
                characterController.velocity.z
            );

            float horizontalSpeed = horizontalVelocity.magnitude;

            movementBar.fillAmount -= (horizontalSpeed / movementBarAmount);

            animator.SetFloat("Blend", horizontalSpeed, motionSmoothTime, Time.deltaTime);
        }
        else
        {
            animator.SetFloat("Blend", 0);
        }
    }

    private void FinishMovement()
    {
        animator.SetFloat("Blend", 0);
        GameManager.Instance.FinishCharacterMovement();
    }

    public void ResetMovementPoints()
    {
        movementBar.fillAmount = 1;
    }
}
