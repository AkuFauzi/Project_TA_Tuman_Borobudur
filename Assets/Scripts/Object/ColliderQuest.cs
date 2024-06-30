using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderQuest : MonoBehaviour
{
    public QuestManager questManager;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            questManager.timeLine[1].SetActive(true);
            questManager.questText.text = questManager.isiText[2] + questManager.itemCount + "/4";
            Destroy(gameObject);
        }
    }
}
