using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PointsManager : MonoBehaviour
{
    public static PointsManager Instance { get; private set; }

    public int[] points = new int[5];
    public int sceneIndex;
    public TextMeshProUGUI pointsText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (pointsText != null)
        {
            pointsText.text = "Points: " + points[sceneIndex].ToString();
        }
    }
}