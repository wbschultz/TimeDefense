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

    Vector3 movement;
    bool isBuildMode;

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
    
    void Shoot()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(movement * bulletForce, ForceMode2D.Impulse);
    }
}
