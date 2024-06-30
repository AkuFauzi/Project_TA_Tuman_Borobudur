using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using StarterAssets;

public class ButtonManager : MonoBehaviour
{
    public GameObject SettingUI;
    public GameObject creditUI;
    public GameObject mainmenuUI;
    public StarterAssetsInputs starterAssetsInputs;

    [Header("Button pertama")]
    public GameObject mainMenuIFirst;
    public GameObject winPanelFirst;
    public GameObject losePaneFirst;
    public GameObject settingFirst;
    public GameObject creditFirst;
    public GameObject NextBookBT;

    bool setting;
    bool credit;

    private void Awake()
    {
        SaveManager.Initialize();
    }
    // Start is called before the first frame update
    void Start()
    {
        SettingUI.SetActive(false);
        setting = false;

    }

    public void SettingMainMenu()
    {
        if(setting == false)
        {
            setting = true;
            SettingUI.SetActive(true);
            mainmenuUI.SetActive(false);
            Cursor.visible = true;
            EventSystem.current.SetSelectedGameObject(settingFirst);
        }
        else
        {
            setting = false;
            SettingUI.SetActive(false);
            mainmenuUI.SetActive(true);
            Cursor.visible = false;
            EventSystem.current.SetSelectedGameObject(mainMenuIFirst);
        }
    }

    public void CreditMenu()
    {
        if(credit == false)
        {
            credit = true;
            creditUI.SetActive(true);
            mainmenuUI.SetActive(false);
            EventSystem.current.SetSelectedGameObject(creditFirst);
        }
        else
        {
            credit = false;
            creditUI.SetActive(false);
            mainmenuUI.SetActive(true);
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
        SaveManager.Local = new SaveManager.LocalColletion();
    }

    public void Retry()
    {
        Time.timeScale = 1;
        starterAssetsInputs.cursorInputForLook = true;
        SceneManager.LoadScene("GamePlay");
    }
    public void quit()
    {
        Application.Quit();
    }


}
