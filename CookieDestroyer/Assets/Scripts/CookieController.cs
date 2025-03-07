using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CookieController : MonoBehaviour
{
    private int speed = 5;

    private float speedMultiplier = 1f;
    public GameObject ExplosionPrefab;
    public GameObject pointsText;

    private Dictionary<string, int> pointValues = new Dictionary<string, int>
    {
        { "Enemy", 1000 },
        { "Shield", 500 },
        { "Astroid", 100 }
    };

    void Start()
    {
        // Rotate along x by 90 degrees
        transform.Rotate(Vector3.right * 90);

        // Apply force in the direction this cookie is facing
        GetComponent<Rigidbody>().AddForce(transform.up * 200 * speedMultiplier);
    }

    void Update()
    {
        // Rotate the cookie
        transform.Rotate(Vector3.up * Time.deltaTime * 200);
    }

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        
        if (pointValues.ContainsKey(tag))
        {
            AddPoints(pointValues[tag]);
        }
    }

    private void AddPoints(int amount)
    {
        var pointsManager = PointsManager.Instance;
        pointsManager.points[pointsManager.sceneIndex] += amount;

        // Instantiate explosion effect
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);

        // Instantiate points text
        GameObject points = Instantiate(pointsText, transform.position, Quaternion.identity);
        points.GetComponentInChildren<TextMeshProUGUI>().text = amount.ToString();

        // rotate the points text
        points.transform.Rotate(Vector3.right * 90);

        // Destroy the cookie
        Destroy(gameObject);
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }
}
