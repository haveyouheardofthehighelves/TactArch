using System.Collections;
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
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        if (a.CheckGrounded() || (!a.CheckGrounded() && !a.CheckSliding()))
            FlipControl();
        Vector2 direction = new Vector2(
        mousePosition.x - transform.position.x,
        mousePosition.y - transform.position.y
        );
        if (transform.parent.localScale.x > 0)
            transform.right = direction;
        else
            transform.right = -direction;
    }
    void FlipControl()
    {   
      
        if(transform.eulerAngles.z>90 && transform.eulerAngles.z < 270)
            transform.parent.localScale = new Vector3(-transform.parent.localScale.x, transform.parent.localScale.y, transform.parent.localScale.z);
        
    }
}
