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
    public GameObject ingameUI;
    public GameObject lighting, lightingBoss;
    public HealthBar healthBar;
    public Kuntilanak bossKunti;
    public TPSController tpsController;
    public GameObject[] obstacle;
    public GameObject[] timeLine;
    public GameObject[] spawners;
    


    [TextArea(3, 10)]
    public string[] isiText;

    public int totalItem;
    public int itemCount;

    public bool death, win, level2;

    private void Start()
    {
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        lighting.SetActive(false);
        lighting.SetActive(false);
        death = false;
        win = false;

        itemCount = SaveManager.Local.itemCount;

        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].SetActive(false);
        }

    }

    private void Update()
    {
        SaveManager.Local.itemCount = itemCount;
        itemLoad();
        QuestUpdate();
        WinCondition();
        LoseCondition();
    }

    void QuestUpdate()
    {
        questText.text = isiText[0] + " " + totalItem + "/4";
        if (itemCount >= 4 && level2 == false)
        {
            timeLine[0].SetActive(true);
            obstacle[0].SetActive(false);
            lighting.SetActive(true);
            level2 = true;
            totalItem = 0;
            questText.text = isiText[1];
        }
        else if (itemCount >= 8 && level2 == true)
        {
            obstacle[1].SetActive(false);
            timeLine[2].SetActive(true);
            lightingBoss.SetActive(true);
            questText.text = isiText[3] + " " + totalItem + "/4";
        }

        for (int i = 0; i < bukuManager.itemCollectible.Length - 5; i++)
        {
            if (bukuManager.itemCollectible[i] == null)
            {
                spawners[i].SetActive(true);
            }
            else
            {
                spawners[i].SetActive(false);
            }
        }
        
    }

    public void LoseCondition()
    {
        if(healthBar.currentHealth <= 0 && death == false)
        {
            losePanel.SetActive(true);
            ingameUI.SetActive(false);
            tpsController.onOverlay = true;
            Cursor.lockState = CursorLockMode.None;
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
            Cursor.lockState = CursorLockMode.None;
            StarterAssetsInputs.cursorInputForLook = false;
            EventSystem.current.SetSelectedGameObject(buttonManager.winPanelFirst);
        }
    }

    public void itemLoad()
    {
        for (int i = 0; i < bukuManager.paper.Length; i++)
        {
            if (SaveManager.Local.buku[i])
            {
                bukuManager.paper[i].SetActive(true);
                Destroy(bukuManager.itemCollectible[i]);
            }
            else
            {
                bukuManager.paper[i].SetActive(false);
            }
        }
    }
}
