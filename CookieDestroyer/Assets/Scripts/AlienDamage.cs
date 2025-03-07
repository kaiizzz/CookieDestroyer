using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AlienDamage : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    public GameObject pointsText;
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
        if (collision.gameObject.tag == "Cookie")
        {
            // add points to the player
            PointsManager.Instance.points[PointsManager.Instance.sceneIndex] += 0;

            // instantiate the explosion
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);

            
            // destroy the alien
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Astroid")
        {
            PointsManager.Instance.points[PointsManager.Instance.sceneIndex] += 2000;
            
            // instantiate the explosion
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);

            // instantiate the points text
            GameObject points = Instantiate(pointsText, transform.position, Quaternion.identity);
            points.GetComponentInChildren<TextMeshProUGUI>().text = "2000";

            // rotate the points text
            points.transform.Rotate(Vector3.right * 90);
            
            // destroy the alien
            Destroy(gameObject);
        }
    }
}
