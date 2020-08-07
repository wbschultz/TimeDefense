using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed = 5f;

    public Rigidbody2D rb;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;

    Vector3 charScale;

    private void Start()
    {
        charScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movespeed * Time.deltaTime);

        // if moving left/right
        if (movement.x < 0)
        {
            // flip horizontal
            transform.localScale = new Vector3(-1 * charScale.x, transform.localScale.y, transform.localScale.z);
        } else if(movement.x > 0)
        {
            //flip back
            transform.localScale = new Vector3(charScale.x, transform.localScale.y, transform.localScale.z);
        }
        // if up/down don't change but fire up/down

    }
}
