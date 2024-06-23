using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestManager : MonoBehaviour
{
    public TextMeshProUGUI questText;
    public ButtonManager buttonManager;
    public BukuManager bukuManager;
    public GameObject losePanel;
    public GameObject winPanel;
    public GameObject lighting;
    public HealthBar healthBar;
    public Kuntilanak bossKunti;
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
        winPanel.SetActive(false);
        lighting.SetActive(false);
    }

    private void Update()
    {
        QuestUpdate();
        WinCondition();
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
            lighting.SetActive(true);

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
            EventSystem.current.SetSelectedGameObject(buttonManager.losePaneFirst);
        }
    }
    void WinCondition()
    {
        if(bossKunti.healthPoint <= 0)
        {
            winPanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(buttonManager.winPanelFirst);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
