using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public WeaponAmmo weaponAmmo;
    public StarterAssetsInputs starterAssetsInputs;
    public GameObject UIInteract;

    // Start is called before the first frame update
    void Start()
    {
        starterAssetsInputs = GameObject.FindObjectOfType<StarterAssetsInputs>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == ("Player"))
        {
            UIInteract.SetActive(true);
            if (starterAssetsInputs.pickup)
            {
                weaponAmmo.currentAmmo += 10;
                starterAssetsInputs.pickup = false;
                UIInteract.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == ("Player"))
        {
            UIInteract.SetActive(false);
        }
    }
}
