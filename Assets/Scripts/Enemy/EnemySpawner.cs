using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemy;
    public GameObject locSpawn;

    public float cooldownSpawn;
    public int totalSpawn;
    public int maxSpawn;

    public bool spawn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();  
    }

    void SpawnEnemy()
    {
        int rnd = Random.Range(0, enemy.Length);
        totalSpawn = Mathf.Clamp(totalSpawn, 0, maxSpawn);
        if (spawn == false)
        {
            spawn = true;
            StartCoroutine(delay());
            IEnumerator delay()
            {
                Instantiate(enemy[rnd], this.gameObject.transform.transform.position, Quaternion.identity);
                totalSpawn += 1;
                yield return new WaitForSeconds(1 * cooldownSpawn);
                spawn = false;
            }
        }
    }
}
