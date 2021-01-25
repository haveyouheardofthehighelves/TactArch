﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowMechanics : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement a = transform.parent.parent.GetComponent<PlayerMovement>();
       
        BowClamp(a);
        if (a.CheckGrounded() || (!a.CheckGrounded() && !a.CheckSliding()))
            FlipControl();
        LookTowards(a);



    }
    void FlipControl()
    {   
      
        if(transform.eulerAngles.z>90 && transform.eulerAngles.z < 270)
            transform.parent.localScale = new Vector3(-transform.parent.localScale.x, transform.parent.localScale.y, transform.parent.localScale.z);
        
    }
    void BowClamp(PlayerMovement check)
    {
       
    }

    void LookTowards(PlayerMovement check)
    {

        if (!check.CheckSliding())
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.nearClipPlane;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y);
            if (transform.parent.localScale.x > 0)
                transform.right = direction;
            else
                transform.right = -direction;
        }
        else
        {
            GameObject g = Instantiate(gameObject, transform.position, transform.rotation,transform.parent);
             g.GetComponent<SpriteRenderer>().enabled = false;
             Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = Camera.main.nearClipPlane;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                Vector2 direction = new Vector2(
                mousePosition.x - transform.position.x,
                mousePosition.y - transform.position.y);
                if (transform.parent.localScale.x > 0)
                    g.transform.right = direction;
                else
                    g.transform.right = -direction;
            float Angle = g.transform.eulerAngles.z;
            if(Angle >= 0 && Angle <= 90 || Angle >=270 && Angle<=360)
            {
                transform.right = g.transform.right;
            }
            Destroy(g);
            /* if(Angle>90 && Angle <= 180)
             {   
                 Angle = 85;
                 transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Angle);
             }
             else if(Angle>180 && Angle < 270)
             {
                 Angle = 275;
                 transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Angle);
             }
             else
             {
                 Vector3 mousePosition = Input.mousePosition;
                 mousePosition.z = Camera.main.nearClipPlane;
                 mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                 Vector2 direction = new Vector2(
                 mousePosition.x - transform.position.x,
                 mousePosition.y - transform.position.y);
                 if (transform.parent.localScale.x > 0)
                     transform.right = direction;
                 else
                     transform.right = -direction;

             }*/
        }

        
    }
}
