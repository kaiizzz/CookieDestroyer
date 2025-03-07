using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShieldDamage : MonoBehaviour
{
    public int maxHp = 2; // Shield health
    private int hp; // Current shield health
    private Material material; // Shield material
    private Color originalColor; // Store the original shield color

    public GameObject ExplosionPrefab; // Explosion prefab
    public GameObject pointsText; // Points text prefab

    void Start()
    {
        material = GetComponent<Renderer>().material; // Get the material of the shield
        originalColor = material.color; // Store the original color
        hp = maxHp; // Set shield health to max
        UpdateShieldTransparency();
    }

    void Update()
    {
        UpdateShieldTransparency();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hp > 0 && (collision.gameObject.CompareTag("Cookie") || collision.gameObject.CompareTag("Astroid"))) // Check if the shield is hit by a cookie or bullet
        {
            hp--; // Reduce health
            UpdateShieldTransparency();

            if (collision.gameObject.CompareTag("Cookie"))
            {
                // continue
            }
            else if (collision.gameObject.CompareTag("Astroid"))
            {
                PointsManager.Instance.points[PointsManager.Instance.sceneIndex] += 1000; // Add points
                GameObject points = Instantiate(pointsText, transform.position, Quaternion.identity); // Create points text
                points.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "1000"; // Set points text

                // rotate the points text
                points.transform.Rotate(Vector3.right * 90);


            }
        }

        if (hp <= 0)
        {
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity); // Create explosion
            Destroy(gameObject); // Destroy shield when health is 0
        }
    }

    private void UpdateShieldTransparency()
    {
        Color newColor = originalColor;
        
        if (hp == 2)
        {
            newColor.a = 0.4f; // 70% opacity
        }
        else if (hp == 1)
        {
            newColor.a = 0.01f; // 40% opacity
        }
        
        material.color = newColor;
    }
}
