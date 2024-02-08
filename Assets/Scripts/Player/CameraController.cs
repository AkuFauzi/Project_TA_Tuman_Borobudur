using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera aimVCam;
    private StarterAssetsInputs StarterAssetsInputs;

    private void Awake()
    {
        StarterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        if (StarterAssetsInputs.aim)
        {
            aimVCam.gameObject.SetActive(true);
        }
        else
        {
            aimVCam.gameObject.SetActive(false);
        }
    }
}
