using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainmenu : MonoBehaviour
{
    void Start()
    {
        
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
