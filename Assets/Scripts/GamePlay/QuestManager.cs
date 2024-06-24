using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestManager : MonoBehaviour
{
    public TextMeshProUGUI questText;
    public StarterAssetsInputs StarterAssetsInputs;
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

    public bool death, win;

    private void Start()
    {
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        lighting.SetActive(false);
        death = false;
        win = false;
        itemCount = SaveManager.Local.itemCount;
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
            questText.text = isiText[1] + itemCount+"/4";
        }
        else if (totalItem >= 8)
        {
            obstacle[1].SetActive(false);
            questText.text = isiText[2];
        }

        for (int i = 0; i < bukuManager.itemCollectible.Length; i++)
        {
            if (bukuManager.itemCollectible[i] == null)
            {
                spwaners[i].SetActive(true);
            }
        }
        
    }

    public void LoseCondition()
    {
        if(healthBar.currentHealth <= 0 && death == false)
        {
            losePanel.SetActive(true);
            Time.timeScale = 0;
            StarterAssetsInputs.cursorLocked = false;
            StarterAssetsInputs.cursorInputForLook = false;
            EventSystem.current.SetSelectedGameObject(buttonManager.losePaneFirst);
            death = true;
        }
    }
    public void WinCondition()
    {
        if(bossKunti.healthPoint <= 0 && win == false)
        {
            winPanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(buttonManager.winPanelFirst);
        }
    }
}
