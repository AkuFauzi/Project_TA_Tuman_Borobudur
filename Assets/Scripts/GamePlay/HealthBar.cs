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
    //bool cooldownHeal;
    
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        cooldownHeal = false;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = currentHealth;
        if(currentHealth < maxHealth)
        {
            if(cooldownHeal = false)
            {
                StartCoroutine(delay());
                IEnumerator delay()
                {
                    currentHealth += 10;
                    yield return new WaitForSeconds(1);
                    cooldown = false;

                }
            }
   
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (cooldown == false)
        {
            cooldown = true;
            StartCoroutine(delay());
            IEnumerator delay()
            {
                if (other.gameObject.tag == "enemy" && currentHealth != 0)
                {
                    currentHealth -= 10;
                }
                if (other.gameObject.tag == "AttackE" && currentHealth != 0)
                {
                    currentHealth -= 15;
                }
                yield return new WaitForSeconds(1);
                cooldown = false;

                
            }

        }


    }
}
