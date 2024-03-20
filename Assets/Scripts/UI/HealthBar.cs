using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public int maxHealth;
    public int regenHealth;
    public int currentHealth;

    bool cooldown;
    bool cooldownHeal;
    
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = currentHealth;
        if(currentHealth < maxHealth && cooldownHeal == false)
        {
            cooldownHeal = true;
            StartCoroutine(heal());
            IEnumerator heal()
            {
                currentHealth += regenHealth;
                yield return new WaitForSeconds(2);
                cooldownHeal = false;
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if(cooldown == false)
        {
            cooldown = true;
            StartCoroutine(delay());
            IEnumerator delay()
            {
                if (other.gameObject.tag == "Enemy" && currentHealth != 0)
                {
                    currentHealth -= 10;
                }
                yield return new WaitForSeconds(1);
                cooldown = false;
            }
            
        }

    }
}
