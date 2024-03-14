using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class TPSController : MonoBehaviour
{
    private StarterAssetsInputs StarterAssetsInputs;
    public CinemachineVirtualCamera aimVCam;
    private WeaponAmmo weaponAmmo;
    public LayerMask aimColliderLayer = new LayerMask();
    public Transform debugTranform;
    public Transform bulletPrefab;
    public Transform spawnBulletPosition;
    public ThirdPersonController thirdPersonController;
    public Transform mainCamera;
    public ButtonManager buttonManager;

    PlayerInput playerInput;

    public float firerate = 5f;

    public bool inpause;

    private void Awake()
    {
        StarterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        weaponAmmo = GetComponent<WeaponAmmo>();
        playerInput = GetComponent<PlayerInput>();
        buttonManager = FindObjectOfType<ButtonManager>();
        inpause = false;
    }

    private void Update()
    {
        aimShoot();
        pause();

    }

    public void pause()
    {
        if (StarterAssetsInputs.pause && inpause == false)
        {
            inpause = true;
            thirdPersonController.enabled = false;
            Time.timeScale = 0;
            buttonManager.SettingUI.SetActive(true);
            EventSystem.current.SetSelectedGameObject(buttonManager.settingFirst);
            StarterAssetsInputs.pause = false;
        }
        else if (StarterAssetsInputs.pause && inpause == true)
        {
            inpause = false;
            thirdPersonController.enabled = true;
            Time.timeScale = 1;
            buttonManager.SettingUI.SetActive(false);
            StarterAssetsInputs.pause = false;
        }
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
            if(shoot == true)
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

                        Instantiate(bulletPrefab, spawnBulletPosition.position, Quaternion.LookRotation(bulletDir, Vector3.up));
                        weaponAmmo.currentAmmo = weaponAmmo.currentAmmo - 1;
                        yield return new WaitForSeconds(1 / firerate);
                        cooldown = false;
                    }
                }
            }
            if (weaponAmmo.currentAmmo == 0)
            {
                shoot = false;
            }
            else
            {
                shoot = true;
            }
           
            //StarterAssetsInputs.shoot = false;
        }
    }
    bool cooldown;
    bool shoot;

}
