using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BukuManager : MonoBehaviour
{
    public GameObject[] paper;
    public GameObject[] page;
    public Button BackBT, NextBT;

    int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int CurrentIndex
    {
        get
        {
            return currentIndex;
        }
        set
        {
            if (page[currentIndex] != null)
            {
                GameObject activeObj = page[currentIndex];
                activeObj.SetActive(false);
            }
            if(value < 0)
            {
                currentIndex = page.Length - 1;
            }
            else if (value > page.Length -1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex = value;
            }
            if (page[currentIndex] != null)
            {
                GameObject activeOBJ = page[currentIndex];
                activeOBJ.SetActive(true);
            }
        }
    }

    public void Previous(int direction)
    {
        if(direction == 0)
        {
            currentIndex--;
        }
        if(currentIndex <= 3)
        {
            NextBT.gameObject.SetActive(true);
        }
        if(currentIndex <= 0)
        {
            BackBT.gameObject.SetActive(false);
        }
    }
    public void Next(int direction)
    {
        if(direction >= 1)
        {
            currentIndex++;
        }
        if(currentIndex >= 4)
        {
            NextBT.gameObject.SetActive(false);
        }
        if(currentIndex >= 1)
        {
            BackBT.gameObject.SetActive(true);
        }
    }
}
