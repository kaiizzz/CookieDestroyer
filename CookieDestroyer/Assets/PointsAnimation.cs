using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsAnimation : MonoBehaviour
{
    public TextMeshProUGUI text;
    private float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move the text up
        transform.position += Vector3.up * speed * Time.deltaTime;

        // Fade out the text
        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - 0.8f * Time.deltaTime);

        // Destroy the text
        if (text.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
