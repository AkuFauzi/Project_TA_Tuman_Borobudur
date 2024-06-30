using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public int maxAmmo;
    public int currentAmmo;

    private void Start()
    {
        currentAmmo = SaveManager.Local.currentAmmo;
    }

    private void Update()
    {
        maxAmmo = 50;
        currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo);
        SaveManager.Local.currentAmmo = currentAmmo;
        ammoText.text = currentAmmo + "/" + maxAmmo;
    }

}
