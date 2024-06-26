using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using UnityEngine.WSA;

public class TPSController : MonoBehaviour
{
    private StarterAssetsInputs StarterAssetsInputs;
    public CinemachineVirtualCamera aimVCam;
    private WeaponAmmo weaponAmmo;
    private HealthBar healthBar;
    public LayerMask aimColliderLayer = new LayerMask();
    public Transform debugTranform;
    public Transform bulletPrefab;
    public Transform spawnBulletPosition;
    public ThirdPersonController thirdPersonController;
    public Transform mainCamera;
    public ButtonManager buttonManager;
    public QuestManager questManager;

    public GameObject book;
    public AudioManager AudioManager;

    PlayerInput playerInput;

    public float firerate = 5f;

    public bool inpause;
    public bool onbook;
    public bool onOverlay;

    private void Awake()
    {
        StarterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        weaponAmmo = GetComponent<WeaponAmmo>();
        healthBar = GetComponent<HealthBar>();
        playerInput = GetComponent<PlayerInput>();
        buttonManager = FindObjectOfType<ButtonManager>();
        inpause = false;
        onbook = false;
        onOverlay = false;
        book.SetActive(false);
    }

    private void Update()
    {

        aimShoot();
        pause();
        OpenBook();
        cheat();
    }

    public void pause()
    {
        if (StarterAssetsInputs.pause && inpause == false && onOverlay == false)
        {
            inpause = true;
            onOverlay = true;
            thirdPersonController.enabled = false;
            Time.timeScale = 0;
            buttonManager.SettingUI.SetActive(true);
            EventSystem.current.SetSelectedGameObject(buttonManager.settingFirst);
            playerInput.SwitchCurrentActionMap("UI");
            StarterAssetsInputs.pause = false;
        }
        else if (StarterAssetsInputs.pause && inpause == true && onOverlay == true)
        {
            inpause = false;
            onOverlay = false;
            thirdPersonController.enabled = true;
            Time.timeScale = 1;
            buttonManager.SettingUI.SetActive(false);
            playerInput.SwitchCurrentActionMap("Player");
            StarterAssetsInputs.pause = false;
        }
    }

    public void OpenBook()
    {
        if(StarterAssetsInputs.openBook && onbook == false && onOverlay == false)
        {
            onbook = true;
            onOverlay = true;
            Time.timeScale = 0;
            book.SetActive(true);
            EventSystem.current.SetSelectedGameObject(buttonManager.NextBookBT);
            StarterAssetsInputs.openBook = false;
            StarterAssetsInputs.cursorLocked = false;
            StarterAssetsInputs.cursorInputForLook = false;
        }
        else if(StarterAssetsInputs.openBook && onbook == true && onOverlay == true)
        {
            onbook = false;
            onOverlay = false;
            Time.timeScale = 1;
            book.SetActive(false);
            StarterAssetsInputs.openBook = false;
            StarterAssetsInputs.cursorLocked = true;
            StarterAssetsInputs.cursorInputForLook = true;
        }
    }

    void aimShoot()
    {
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, aimColliderLayer))
        {
           // debugTranform.position = hit.point;
            mouseWorldPosition = hit.point;
        }


        if (StarterAssetsInputs.aim)
        {
            aimVCam.gameObject.SetActive(true);
            thirdPersonController.rotate = false;

            thirdPersonController._animator.SetBool("Aim", true);

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDir = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * 20);
        }
        else
        {
            aimVCam.gameObject.SetActive(false);
            thirdPersonController._animator.SetBool("Aim", false);
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

                transform.forward = Vector3.Lerp(transform.forward, shootDir, Time.deltaTime * 1000);

                if (cooldown == false
                    && mainCamera.transform.eulerAngles.y <= transform.eulerAngles.y + 10
                    && mainCamera.transform.eulerAngles.y >= transform.eulerAngles.y - 10)
                {
                    cooldown = true;
                    StartCoroutine(delay());
                    IEnumerator delay()
                    {
                        thirdPersonController._animator.SetBool("Shoot", true);
                        Instantiate(bulletPrefab, spawnBulletPosition.position, Quaternion.LookRotation(bulletDir, Vector3.up));
                        weaponAmmo.currentAmmo = weaponAmmo.currentAmmo - 1;

                        SaveManager.Local.currentAmmo = weaponAmmo.currentAmmo;

                        yield return new WaitForSeconds(1 / firerate);
                        thirdPersonController._animator.SetBool("Shoot", false);
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
    bool oncheat;
    void cheat()
    {
        if (StarterAssetsInputs.cheat1)
        {
            AudioManager.audioSourceBGM.PlayOneShot(AudioManager.bgmBoss);
            StarterAssetsInputs.deletesave = false;
        }
        if (StarterAssetsInputs.cheat2 && oncheat == false)
        {
            Debug.Log("wdja");
            oncheat = true;
            healthBar.cheat = true;

            StarterAssetsInputs.cheat2 = false;
        }
        else if (StarterAssetsInputs.cheat2 && oncheat == true)
        {
            oncheat = false;
            healthBar.cheat = false;
            StarterAssetsInputs.cheat2 = false;
        }
        if (StarterAssetsInputs.cheat3)
        {
            weaponAmmo.currentAmmo = weaponAmmo.maxAmmo;
            StarterAssetsInputs.deletesave = false;
        }
        if (StarterAssetsInputs.cheat4)
        {
            questManager.itemCount = 4;
            StarterAssetsInputs.cheat4 = false;
        }

        if (StarterAssetsInputs.deletesave)
        {
            SaveManager.Local = new SaveManager.LocalColletion();
            StarterAssetsInputs.deletesave = false;
        }
    }

}
