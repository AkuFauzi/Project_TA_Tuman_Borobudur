using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BukuManager : MonoBehaviour
{
    public GameObject[] paper;
    public GameObject[] itemCollectible;
    public GameObject[] page;

    public Button BackBT, NextBT;

    public int currentIndex = 0;
    public int maxIndex;

    public int maxPaperIndex;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < paper.Length; i++)
        {
            if (SaveManager.Local.buku[i])
            {
                page[i].SetActive(true);
            }
            else
            {
                page[i].SetActive(false);
            }
        }
        
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
        maxPaperIndex = paper.Length;
        for(int q = 0; q < itemCollectible.Length; q++)
        {
            if (itemCollectible[q] == null)
            {
                SaveManager.Local.buku[q] = true;
                paper[q].SetActive(true);
            }
            else
            {
                paper[q].SetActive(false);
            }
        }
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
            EventSystem.current.SetSelectedGameObject(NextBT.gameObject);
        }
    }
    public void Next()
    {
        currentIndex += 1 ;
        if(currentIndex == maxIndex - 1)
        {
            NextBT.gameObject.SetActive(false);
            EventSystem.current.SetSelectedGameObject(BackBT.gameObject);
        }
        if(currentIndex >= 1)
        {
            BackBT.gameObject.SetActive(true);
        }
    }
}
