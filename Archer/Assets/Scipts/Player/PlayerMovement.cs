﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HeaderAttribute("Groundstuff")]
    bool Grounded = false;
    [HeaderAttribute("Wallstuff")]
    bool Sliding = false;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
    
        GroundMovement(15);

        if (!Grounded)
        {
            WallJump(-1f, 30);
        }

        print(CheckGrounded());
    
    }

   
    private Transform FindChild(Transform a,string find)
    {
        foreach(Transform child in a)
        {
            if(child.name == find){
                return child;
            }
        }
        Debug.Log(find + " does not exist under " + a.name);
        return null;
    }
    float oldwall = 0.1f;
    float newwall = .2f;
  
    private void WallJump(float slidespeed, float jumpforce)
    {
        LayerMask c = 1 << 8 | 1 << 10;
        c = ~c;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        RaycastHit2D a = Physics2D.Raycast(FindChild(transform.Find("PlayerSprite"), "Body").position, Vector2.left, 2f, c);
        RaycastHit2D b = Physics2D.Raycast(FindChild(transform.Find("PlayerSprite"), "Body").position, -Vector2.left, 2f, c);
        Vector3 scale = transform.Find("PlayerSprite").transform.localScale;
        if (a && a.collider.tag != "cantwalljump" && scale.x>0 && !a.collider.tag.Contains("Arrow"))
        {
            newwall = a.collider.GetInstanceID();
            if(oldwall != newwall)
            {
                //transform.Find("PlayerSprite").transform.localScale = new Vector3(Mathf.Abs(transform.Find("PlayerSprite").transform.localScale.x), transform.Find("PlayerSprite").transform.localScale.y, transform.Find("PlayerSprite").transform.localScale.z);
                rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y, slidespeed, float.MaxValue), 0);
                Sliding = true;
                if (Input.GetKeyDown("space"))
                {
                    oldwall = newwall;
                    rb.velocity = new Vector2(rb.velocity.x,jumpforce);
                }
            }
            else
            {
                Sliding = false;
            }
        }
        else if (b && b.collider.tag != "cantwalljump" && scale.x<0 && !b.collider.tag.Contains("Arrow"))
        {
            //transform.Find("PlayerSprite").transform.localScale = new Vector3(-Mathf.Abs(transform.Find("PlayerSprite").transform.localScale.x), transform.Find("PlayerSprite").transform.localScale.y, transform.Find("PlayerSprite").transform.localScale.z);
            newwall = b.collider.GetInstanceID();
            if (oldwall != newwall)
            {
                rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y, slidespeed, float.MaxValue), 0);
                Sliding = true;
                if (Input.GetKeyDown("space"))
                {
                    oldwall = newwall;
                    rb.velocity = new Vector2(rb.velocity.x,jumpforce);
                }
            }
            else
                Sliding = false;
            
        }
        else
            Sliding = false;
        

    }
    private void GroundMovement(float movespeed)
    {
      Rigidbody2D rb = GetComponent<Rigidbody2D>();
      float x;
      LayerMask layers =  ~(1 << 8 | 1 << 10);

      if(Physics2D.OverlapCircle(FindChild(transform.Find("PlayerSprite"), "Feet").position,.4f,layers))
        {
  
            Grounded = true;
            ResetWalls();
            Jump(20);
            x= Input.GetAxis("Horizontal") * movespeed;
        }
        else
        {
            x = Input.GetAxis("Horizontal") * movespeed;
            Grounded = false;
        }
        Vector2 move = new Vector2(x, rb.velocity.y);
        rb.velocity = move;
    }   
    void Jump(float jumpforce)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (Input.GetKeyDown("space"))
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpforce);
        }
    }
    private void ResetWalls()
    {    oldwall = 0.1f;
         newwall = .2f;
    }
    public bool CheckGrounded()
    {
        return Grounded;
    }
    public bool CheckSliding()
    {
        return Sliding;
    }

}
