using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public int BulletsLeft = 3;
    public GameObject BulletImage;
    private int bulletCount;
    // Start is called before the first frame update
    void Start()
    {
        bulletCount = BulletsLeft;
    }

    // Update is called once per frame
    void Update()
    {
        // draw the image times the number of bullets left
        for (int i = 0; i < BulletsLeft; i++)
        {
            GameObject bullet = Instantiate(BulletImage, new Vector3(0, 0, 0), Quaternion.identity);
            bullet.transform.SetParent(GameObject.Find("Canvas").transform, false);
            bullet.transform.position = new Vector3(50 + i * 50, 50, 0);
        }

        
    }
}
