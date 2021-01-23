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
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(
        mousePosition.x - transform.position.x,
        mousePosition.y - transform.position.y
        );

        if (transform.parent.localScale.x > 0)
        {
            transform.right = direction;
            print("not flipped");
        }

        else
            transform.right = -direction;
        

    }

}
