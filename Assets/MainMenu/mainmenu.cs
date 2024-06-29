using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainmenu : MonoBehaviour
{
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(this.gameObject);
    }

    void Update()
    {
        
    }


    public void Quit()
    {
        Application.Quit();
    }
    
    public void loadGameplay() 
    {
        Application.LoadLevel("SampleScene");
    }

}
