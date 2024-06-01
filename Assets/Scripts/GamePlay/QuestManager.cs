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

    public int totalItem;
    public int itemCount;

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
        
    }
}
