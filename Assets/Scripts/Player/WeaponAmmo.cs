using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public int maxAmmo;
    public int currentAmmo;

    private void Update()
    {
        maxAmmo = 50;
        currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo);
        ammoText.text = currentAmmo + "/" + maxAmmo;
    }

}
