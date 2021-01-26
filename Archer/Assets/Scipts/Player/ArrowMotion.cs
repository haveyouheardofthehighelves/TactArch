using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMotion : MonoBehaviour
{   
    Rigidbody2D rb;
    bool OnHit;
    float forcemodmod;
    public float forcemodifier = 10;
    // Start is called before the first frame update
    void Start()
    {
        OnHit = false;
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (this.GetComponent<Animator>().GetBool("IsCocking"))
            forcemodmod += Time.deltaTime;

        if (forcemodmod > .5f)
            forcemodmod = .5f;
        
                
        Debug.DrawRay(transform.position, transform.right*100,Color.green);
        if (this.GetComponent<Rigidbody2D>())
        {
            if (!OnHit)
            {
                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }

        }
        else
        {    if(transform.parent.name == "Bow")
            {
                if (this.GetComponent<Animator>().GetBool("IsReleasing"))
                {
                    transform.GetComponent<Collider2D>().isTrigger = false;
                    float scalex = transform.parent.parent.parent.localScale.x;
                    transform.SetParent(null);
                    rb = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
                    
                    print(scalex);
                    if (scalex < 0)
                    {
                        forcemodifier = -Mathf.Abs(forcemodifier);
                        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

                    }
                    else
                        forcemodifier = Mathf.Abs(forcemodifier);

                    forcemodmod = map(forcemodmod, 0, .5f, 0, 1);
                    print(forcemodmod);
                    rb.AddForce(forcemodmod*forcemodifier * transform.right, ForceMode2D.Impulse);
                    forcemodmod = 0;
                }
            }   
            //Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;

        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.layer != 8)
        {
            gameObject.layer = 0;
            OnHit = true;
        }
    }
    float map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return ((x - in_min) * (out_max - out_min) + (in_max - in_min) / 2) / ((in_max - in_min) + out_min)-.5f;
    }
}
