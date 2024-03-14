using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public WeaponAmmo weaponAmmo;
    public StarterAssetsInputs starterAssetsInputs;

    // Start is called before the first frame update
    void Start()
    {
        starterAssetsInputs = GameObject.FindObjectOfType<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == ("Player"))
        {
            if (starterAssetsInputs.pickup)
            {
                weaponAmmo.currentAmmo += 10;
                starterAssetsInputs.pickup = false;
                Destroy(gameObject);
            }
        }
    }
}
