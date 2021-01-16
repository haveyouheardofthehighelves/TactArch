using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMotion : MonoBehaviour
{   
    Rigidbody2D rb;
    bool OnHit;
    // Start is called before the first frame update
    void Start()
    {
        OnHit = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.right*2000);
    }

    // Update is called once per frame
    void Update()
    {
        if (!OnHit)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag != "Player")
        {
            OnHit = true;
        }
    }
}
