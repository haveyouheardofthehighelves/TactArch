using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    float x;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement a = transform.parent.GetComponent<PlayerMovement>();
        x = Input.GetAxis("Horizontal");
        if (a.CheckGrounded() && x != 0)
        {
            SetAllFalse();
            anim.SetBool("IsWalking", true);
            if (CheckFacing())
            {
                if (x > 0)
                    anim.SetFloat("Playback", 1);

                else if (x < 0)
                    anim.SetFloat("Playback", -1);
            }
            else
            {
                if (x < 0)
                    anim.SetFloat("Playback", 1);
                if (x > 0)
                    anim.SetFloat("Playback", -1);


            }

        }
        else if (!a.CheckGrounded() && !a.CheckSliding())
        {
            SetAllFalse();
            anim.SetBool("IsJumping", true);
        }
        else
        {
            SetAllFalse();
            anim.SetBool("IsIdle", true);
        }
        
    }
    bool CheckFacing()
    {
        if (transform.localScale.x < 0)
            return false;
        else
            return true;
    }
    void SetAllFalse()
    {
        foreach (AnimatorControllerParameter parameter in anim.parameters)
            anim.SetBool(parameter.name, false);
    }


}
