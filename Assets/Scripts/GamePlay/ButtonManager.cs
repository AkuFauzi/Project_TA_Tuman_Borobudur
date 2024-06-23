using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using StarterAssets;

public class ButtonManager : MonoBehaviour
{
    public GameObject SettingUI;
    public StarterAssetsInputs starterAssetsInputs;

    [Header("Button pertama")]
    public GameObject mainMenuIFirst;
    public GameObject winPanelFirst;
    public GameObject losePaneFirst;
    public GameObject settingFirst;
    public GameObject NextBookBT;

    bool setting;
    // Start is called before the first frame update
    void Start()
    {
        SettingUI.SetActive(false);
        setting = false;

        SaveManager.Initialize();
    }

    public void SettingMainMenu()
    {
        if(setting == false)
        {
            setting = true;
            SettingUI.SetActive(true);
            EventSystem.current.SetSelectedGameObject(settingFirst);
        }
        else
        {
            setting = false;
            SettingUI.SetActive(false);
            EventSystem.current.SetSelectedGameObject(mainMenuIFirst);
        }
    }

    public void play()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GamePlay");
    }
    public void backMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Retry()
    {
        Time.timeScale = 1;
        starterAssetsInputs.cursorLocked = true;
        starterAssetsInputs.cursorInputForLook = true;
        SceneManager.LoadScene("GamePlay");
    }


}
