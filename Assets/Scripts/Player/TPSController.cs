using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class TPSController : MonoBehaviour
{
    public CinemachineVirtualCamera aimVCam;
    private StarterAssetsInputs StarterAssetsInputs;
    public LayerMask aimColliderLayer = new LayerMask();
    public Transform debugTranform;
    public Transform bulletPrefab;
    public Transform spawnBulletPosition;
    public ThirdPersonController thirdPersonController;
    public Transform mainCamera;

    public float firerate = 5f;

    private void Awake()
    {
        StarterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
        aimShoot();

    }

    void aimShoot()
    {
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, aimColliderLayer))
        {
            debugTranform.position = hit.point;
            mouseWorldPosition = hit.point;
        }


        if (StarterAssetsInputs.aim)
        {
            aimVCam.gameObject.SetActive(true);
            thirdPersonController.rotate = false;

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDir = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * 20);
        }
        else
        {
            aimVCam.gameObject.SetActive(false);
            thirdPersonController.rotate = true;
        }

        if (StarterAssetsInputs.shoot)
        {
            Vector3 bulletDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            Vector3 noAimShootDIr = mouseWorldPosition;
            noAimShootDIr.y = transform.position.y;
            Vector3 shootDir = (noAimShootDIr - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, shootDir, Time.deltaTime * 10000);

            if (cooldown == false 
                && mainCamera.transform.eulerAngles.y <= transform.eulerAngles.y + 10
                && mainCamera.transform.eulerAngles.y >= transform.eulerAngles.y - 10)
            {
                cooldown = true;
                StartCoroutine(delay());
                IEnumerator delay()
                {
                    //
                    
                    Instantiate(bulletPrefab, spawnBulletPosition.position, Quaternion.LookRotation(bulletDir, Vector3.up));
                   
                    yield return new WaitForSeconds(1 / firerate);
                    cooldown = false;
                }
            }


          
            //StarterAssetsInputs.shoot = false;
        }
    }
    bool cooldown;
}
