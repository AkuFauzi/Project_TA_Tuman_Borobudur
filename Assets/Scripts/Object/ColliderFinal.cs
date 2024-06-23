using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderFinal : MonoBehaviour
{
    public GameObject collider;
    public QuestManager QuestManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {

        }
    }
}
