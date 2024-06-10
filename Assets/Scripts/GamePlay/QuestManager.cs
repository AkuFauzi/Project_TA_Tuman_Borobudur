using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public TextMeshProUGUI questText;
    public BukuManager bukuManager;
    public GameObject losePanel;
    public HealthBar healthBar;
    public GameObject[] obstacle;
    public GameObject[] timeLine;
    public GameObject[] spwaners;

    [TextArea(3, 10)]
    public string[] isiText;

    public int totalItem;
    public int itemCount;

    private void Start()
    {
        losePanel.SetActive(false);
    }

    private void Update()
    {
        QuestUpdate();
        LoseCondition();
    }

    void QuestUpdate()
    {
        totalItem = itemCount;
        questText.text = isiText[0] + itemCount+"/4";
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
        }
        
    }

    void LoseCondition()
    {
        if(healthBar.currentHealth <= 0)
        {
            losePanel.SetActive(true);
            Debug.Log("Lose");
        }
    }
}
