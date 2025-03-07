using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CannonController : MonoBehaviour
{
    private Vector3 startPos;         
    private Vector3 dragStartPos;     
    private bool isDragging = false;

    public float maxDragDistance = 2f;       
    public float launchForceMultiplier = 100f; 
    public GameObject bulletPrefab;    
    public Transform firePoint;        

    private Vector3 originalScale;
    private Quaternion originalRotation;
    public float resetSpeed = 5f; // Speed of resetting scale & rotation

    public Image powerBar; // Reference to the power bar image

    void Start()
    {
        startPos = transform.position; 
        originalScale = transform.localScale;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        #if UNITY_ANDROID || UNITY_IOS
            HandleTouchInput();
        #else
            HandleMouseInput();
        #endif

        float maxScaleZ = originalScale.z * 2f; // Max stretch is 2x original size
        float dragAmount = (transform.localScale.z - originalScale.z) / (maxScaleZ - originalScale.z);
        powerBar.fillAmount = Mathf.Clamp(dragAmount, 0f, 1f);

        // If not dragging, smoothly reset cannon
        if (!isDragging)
        {
            ResetCannon();
        }
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) 
            {
                if (hit.collider.gameObject == gameObject)
                {
                    isDragging = true;
                    dragStartPos = transform.position;
                }
            }
        }

        if (Input.GetMouseButton(0) && isDragging) 
        {
            Vector3 mouseWorldPos = GetWorldPosition(Input.mousePosition);
            Vector3 dragVector = mouseWorldPos - startPos;
            dragVector = Vector3.ClampMagnitude(dragVector, maxDragDistance);

            RotateCannon(dragVector); 
        }

        if (Input.GetMouseButtonUp(0) && isDragging) 
        {
            FireBullet();
            isDragging = false;
        }
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject == gameObject)
                        {
                            isDragging = true;
                            dragStartPos = transform.position;
                        }
                    }
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        Vector3 touchWorldPos = GetWorldPosition(touch.position);
                        Vector3 dragVector = touchWorldPos - startPos;
                        dragVector = Vector3.ClampMagnitude(dragVector, maxDragDistance);

                        RotateCannon(dragVector); 
                    }
                    break;

                case TouchPhase.Ended:
                    if (isDragging)
                    {
                        FireBullet();
                        isDragging = false;
                    }
                    break;
            }
        }
    }

    void FireBullet()
    {
        Vector3 launchDirection = (startPos - transform.position).normalized;

        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);

            float dragAmount = (transform.localScale.z - originalScale.z) / (originalScale.z * (maxDragDistance / originalScale.z));
            float speedMultiplier = Mathf.Clamp(dragAmount * 2f, 0.5f, 2f); // Ensures values between 0.5 and 2

            bullet.GetComponent<CookieController>().SetSpeedMultiplier(speedMultiplier);
            Debug.Log("Launch Strength: " + speedMultiplier);
            
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

            if (bulletRb != null)
            {
                bulletRb.AddForce(launchDirection * launchForceMultiplier * speedMultiplier);
            }
        }
    }


    void RotateCannon(Vector3 dragVector)
    {
        if (dragVector.sqrMagnitude > 0.01f) 
        {
            Quaternion targetRotation = Quaternion.LookRotation(-dragVector, Vector3.up); 
            transform.rotation = targetRotation;

            // 🔥 Stretch the cannon in the Z direction instead of Y
            float stretchFactor = 1f + (dragVector.magnitude / maxDragDistance);
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z * stretchFactor);
        }
    }

    void ResetCannon()
    {
        // Smoothly reset rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, Time.deltaTime * resetSpeed);
        
        // Smoothly reset scale (only affects Z)
        transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * resetSpeed);
    }

    Vector3 GetWorldPosition(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        Plane plane = new Plane(Vector3.up, startPos); 
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            return ray.GetPoint(distance);
        }
        return startPos;
    }
}
