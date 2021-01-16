using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   [HeaderAttribute("Groundstuff")]
    bool Grounded = false;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        GroundMovement(2500);
        if (!Grounded)
        {
            WallJump(.3f,100); 
        }
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
       
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        RaycastHit2D a = Physics2D.Raycast(FindChild(transform, "Body").position, Vector2.left,1.2f, ~LayerMask.GetMask("Player"));
        RaycastHit2D b = Physics2D.Raycast(FindChild(transform, "Body").position, -Vector2.left, 1.2f, ~LayerMask.GetMask("Player"));
        if (a)
        {
            newwall = a.collider.GetInstanceID();
            if(oldwall != newwall)
            {
                rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y, slidespeed, float.MaxValue), 0);
                if (Input.GetKeyDown("space"))
                {
                   oldwall = newwall;
                    rb.AddForce(Vector2.up * jumpforce,ForceMode2D.Impulse);
                }
            }

        }
        else if (b)
        {
            newwall = b.collider.GetInstanceID();
            if (oldwall != newwall)
            {
                rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y, slidespeed, float.MaxValue), 0);
                if (Input.GetKeyDown("space"))
                {
                    oldwall = newwall;
                    rb.AddForce(Vector2.up * jumpforce,ForceMode2D.Impulse);
                }
            }

        }

    }
    private void GroundMovement(float movespeed)
    {
      Rigidbody2D rb = GetComponent<Rigidbody2D>();
      float x =  Input.GetAxis("Horizontal")*Time.deltaTime*movespeed;
      if(Physics2D.CircleCast(FindChild(transform, "Body").position - FindChild(transform, "Body").up,.4f,Vector2.down,1f, ~LayerMask.GetMask("Player")))
        {
            Grounded = true;
            ResetWalls();
            Jump(75);
        }
        else
            Grounded = false;
        Vector2 move = new Vector2(x, rb.velocity.y);
        rb.velocity = move;
    }   
    void Jump(float jumpforce)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (Input.GetKeyDown("space"))
            rb.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
    }
    private void ResetWalls()
    {    oldwall = 0.1f;
         newwall = .2f;
    }
}
