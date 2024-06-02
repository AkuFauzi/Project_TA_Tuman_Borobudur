using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public TextMeshProUGUI questText;
    public BukuManager bukuManager;
    public GameObject[] obstacle;
    public GameObject[] timeLine;
    public GameObject[] spwaners;

    public int totalItem;
    public int itemCount;

    private void Start()
    {
        bukuManager = GetComponent<BukuManager>();
    }

    private void Update()
    {
        QuestUpdate();
    }

    void QuestUpdate()
    {
        totalItem = itemCount;
        questText.text = "Jumlah Item Didapat " + itemCount+"/4";
        if (totalItem >= 4)
        {
            timeLine[0].SetActive(true);
            obstacle[0].SetActive(false);
            questText.text = "jjj";
        }

        for (int i = 0; i < bukuManager.itemCollectible.Length; i++)
        {
            if (bukuManager.itemCollectible[i] = null)
            {
                spwaners[i].SetActive(true);
            }
            else
            {
                spwaners[i].SetActive(false);
            }
        }
        
    }
}
