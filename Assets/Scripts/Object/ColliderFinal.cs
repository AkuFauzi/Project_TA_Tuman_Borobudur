using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderFinal : MonoBehaviour
{
    public GameObject colliderFinal;
    public QuestManager QuestManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {

        }
    }
}
