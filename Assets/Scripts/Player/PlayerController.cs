using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    GameObject MyCamera;
    PlayerInput playerInput;
    CharacterController myController;
    Animator myAnimator;
    StarterAssetsInputs input;

    public float moveSpeed = 2.0f;
    public float sprintSpeed = 5.0f;
    public float accellaration = 10.0f;
    public float rotationSmooth = 1;

    float speed;
    float targetRotation;
    float rotationVelocity;
    float verticalVelocity;

    public bool move;

    // Start is called before the first frame update
    void Start()
    {
        myController = GetComponent<CharacterController>();
        MyCamera = GameObject.FindGameObjectWithTag("MainCamera");
        input = GetComponent<StarterAssetsInputs>();
        playerInput = GetComponent<PlayerInput>();

        LoadPlayerPosition();
        StartCoroutine(savePlayerPosition());
    }


    void LoadPlayerPosition()
    {
        myController.enabled = false;
        transform.position = SaveManager.Local.playerPosition;
        myController.enabled = true;
    }
    IEnumerator savePlayerPosition()
    {
        yield return new  WaitForSeconds(120);
        SaveManager.Local.playerPosition = transform.position;
        savePlayerPosition();
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void movement()
    {
        if (!myController.isGrounded)
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }


        float targetSpeed = input.sprint ? sprintSpeed : moveSpeed;
        if (input.move == Vector2.zero) { targetSpeed = 0; }

        Vector3 inputDir = new Vector3(input.move.x, 0, input.move.y);

        Vector3 targetDir = Quaternion.Euler(0, targetRotation, 0) * Vector3.forward;
        float currentHorizontalSpeed = new Vector3(myController.velocity.x, 0.0f, myController.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = input.analogMovement ? input.move.magnitude : 1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                Time.deltaTime * accellaration);

            speed = Mathf.Round(speed * 1000f) / 1000f;
        }
        else
        {
            speed = targetSpeed;
        }

        if (input.move != Vector2.zero)
        {
            targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + MyCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation,
                ref rotationVelocity, rotationSmooth);

            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }

        myController.Move(targetDir.normalized * (speed * Time.deltaTime) + new Vector3(0, verticalVelocity, 0) * Time.deltaTime);
    }
}
