using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //https://www.youtube.com/watch?v=qqIXam2iJIk
    PlayerMovement movement;
    float prevpos;
    bool firstime;
    float highestpoint;
    bool isfalling;
    // Start is called before the first frame update
    void Start()
    {
        firstime = true;
        isfalling = false;
        prevpos = transform.position.y;
        movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!movement.CheckGrounded())
        {
            if (transform.position.y<prevpos && firstime)
            {
                highestpoint = transform.position.y;
                firstime = false;
            }
            prevpos = transform.position.y;
        }
    if((movement.CheckGrounded() || movement.CheckSliding())&& !firstime)
        {
            float displacement = Mathf.Abs(highestpoint - prevpos);
          //  Debug.Log("Displacement: " +);
             firstime = true;
            
        }

    }
}
