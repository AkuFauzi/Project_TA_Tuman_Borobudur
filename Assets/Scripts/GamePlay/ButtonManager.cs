using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour
{
    public GameObject SettingUI;

    [Header("Button pertama")]
    public GameObject mainMenuIFirst;
    public GameObject settingFirst;
    public GameObject NextBookBT;

    bool setting;
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
        SceneManager.LoadScene("GamePlay");
    }

}
