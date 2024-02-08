using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ThirdPersonController : MonoBehaviour
{
    GameObject MyCamera;
    public float speed = 5f;
    [Range(0.0f,0.3f)]
    public float rotSpeed = 0.12f;
    public float sprintSpeed = 100f;
    public float acdcSpeed = 10f;
    public float jumpSpeed;

    PlayerInput playerInput;
    StarterAssetsInputs input;
    CharacterController myController;
    float mDesireRotation = 0f;
    float velocityRotation;
    float verticalVelocity;
    public float mDesiredAnimationSpeed = 0f;
    float mSpeedY = 0f;

    public bool move;

    // Start is called before the first frame update
    void Start()
    {
        myController = GetComponent<CharacterController>();
        MyCamera = GameObject.FindGameObjectWithTag("MainCamera");
        playerInput = GetComponent<PlayerInput>();
        input = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void movement()
    {
        if (move)
        {
            //float x = Input.GetAxisRaw("Horizontal");
            //float z = Input.GetAxisRaw("Vertical");

            // gravitasi
            if (!myController.isGrounded)
            {
                mSpeedY += Physics.gravity.y * Time.deltaTime;
            }
            else
            {
                mSpeedY = 0;
            }
            Vector3 verticalMovement = Vector3.up * mSpeedY;
            ////////////////

            //mSprint = Input.GetKey(KeyCode.LeftShift);

            //Vector3 movement = new Vector3(x, 0, z).normalized;

            //InputSyntem
            float targetspeed = input.sprint ? sprintSpeed : speed;
            //if (input.move == Vector2.zero) targetspeed = 0f;
            Debug.Log(input.move);

            float currentHSpeed=new Vector3(myController.velocity.x,0,myController.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = input.analogMovement ? input.move.magnitude : 1f;

            if(currentHSpeed<targetspeed - speedOffset ||
                currentHSpeed > targetspeed + speedOffset)
            {
                speed = Mathf.Lerp(currentHSpeed, targetspeed * inputMagnitude, 
                    Time.deltaTime * acdcSpeed);

                speed = Mathf.Round(speed * 1000f) / 1000f;
            }
            else
            {
                speed = targetspeed;
            }

            Vector3 inputDir = new Vector3(input.move.x, 0, input.move.y).normalized;

            if(input.move != Vector2.zero)
            {
                mDesireRotation = Mathf.Atan2(inputDir.x, inputDir.z)*Mathf.Rad2Deg + 
                    MyCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                    mDesireRotation, ref velocityRotation, rotSpeed);

                transform.rotation = Quaternion.Euler(0f, rotation, 0);
            }
            Vector3 targetDir = Quaternion.Euler(0f, mDesireRotation, 0) * Vector3.forward;
            myController.Move(targetDir.normalized * (speed * Time.deltaTime) + 
                new Vector3(0, mSpeedY, 0) * Time.deltaTime);
        }

    }
}
