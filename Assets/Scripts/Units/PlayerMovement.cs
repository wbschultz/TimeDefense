﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed = 5f;

    public Rigidbody2D rb;
    public Animator anim;

    Vector2 movement;
    Vector2 previousMovement = Vector2.zero;
    Vector2 mousePos;

    Vector3 charScale;

    // event to notify scripts (like shooting script) of changes to player direction
    public static event Action<Vector2> OnDirectionChange;

    private void Start()
    {
        charScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if(movement != previousMovement)
        {
            previousMovement = movement;
            if (movement != Vector2.zero)
            {
                // only want to update directions when non-zero
                OnDirectionChange.Invoke(movement);
                anim.SetBool("IsRunning", true);
            }
            else
            {
                anim.SetBool("IsRunning", false);
            }
        }
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
