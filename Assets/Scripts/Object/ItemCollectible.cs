using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectible : MonoBehaviour
{
    public QuestManager questManager;
    public GameObject UIInteract;
    public StarterAssetsInputs starterAssetsInputs;

    // Start is called before the first frame update
    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            UIInteract.SetActive(true);
            if (starterAssetsInputs.pickup)
            {
                questManager.itemCount += 1;
                starterAssetsInputs.pickup = false;
                UIInteract.SetActive(false);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            UIInteract.SetActive(false);
        }
    }
}
