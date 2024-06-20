using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody bulletRB;
    public GameObject hit;
    // Start is called before the first frame update
    void Start()
    {
        bulletRB = GetComponent<Rigidbody>();
        hit.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 25f;
        bulletRB.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        hit.SetActive(true);
        Destroy(gameObject, 0.1f);
    }
}
