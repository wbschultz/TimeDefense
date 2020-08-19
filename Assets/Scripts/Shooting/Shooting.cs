using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shooting : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bulletPrefab;
    public PlacementManager placementManager;

    public float bulletForce = 20f;

    Vector2 m_movement = new Vector2(1,0);
    bool isBuildMode;

    private void OnEnable()
    {
        PlayerMovement.OnDirectionChange += GetNewDirection;
    }

    private void OnDisable()
    {
        PlayerMovement.OnDirectionChange -= GetNewDirection;
    }

    // Update is called once per frame
    void Update()
    {
        bool isMouseOverUI = EventSystem.current.IsPointerOverGameObject();
        isBuildMode = placementManager.buildMode;
        
        if (!isMouseOverUI 
            && !isBuildMode
            && Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void GetNewDirection(Vector2 movement)
    {
        m_movement = movement;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(m_movement * bulletForce, ForceMode2D.Impulse);
    }
}
