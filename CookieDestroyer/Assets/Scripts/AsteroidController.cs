using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AsteroidController : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // on collison
    private void OnCollisionEnter(Collision collision)
    {
        // if the alien collides with the player
        if (collision.gameObject.tag == "Enemy")
        {
            // instantiate the explosion
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);

            
            // destroy the alien
            Destroy(gameObject);
        }
    }
}
