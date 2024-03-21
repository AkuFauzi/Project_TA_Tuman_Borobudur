using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BukuManager : MonoBehaviour
{
    public GameObject[] paper;
    public GameObject[] page;
    public Button BackBT, NextBT;

    public int currentIndex = 0;
    public int maxIndex;

    // Start is called before the first frame update
    void Start()
    {
        BackBT.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PageUpdate();
        PaperUpdate();
    }

    void PageUpdate()
    {
        maxIndex = page.Length;
        for(int i = 0; i < page.Length; i++)
        {
            if (i == currentIndex)
            {
                page[i].SetActive(true);
            }
            else
            {
                page[i].SetActive(false);
            }
        }
    }

    void PaperUpdate()
    {

    }


    public void Previous()
    {
        currentIndex -= 1;
        if(currentIndex <= maxIndex)
        {
            NextBT.gameObject.SetActive(true);
        }
        if(currentIndex == 0)
        {
            BackBT.gameObject.SetActive(false);
        }
    }
    public void Next()
    {
        currentIndex += 1 ;
        if(currentIndex == maxIndex - 1)
        {
            NextBT.gameObject.SetActive(false);
        }
        if(currentIndex >= 1)
        {
            BackBT.gameObject.SetActive(true);
        }
    }
}
