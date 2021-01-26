using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    float x;
    Animator anim;
    public Animator bow;
    Animator arrow;
    // Start is called before the first frame update
    void Start()
    {
        arrow = bow.transform.GetChild(1).GetComponent<Animator>();
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //player body animations
        PlayerMovement a = transform.parent.GetComponent<PlayerMovement>();
        x = Input.GetAxis("Horizontal");
        if (a.CheckGrounded() && x != 0)
        {
            SetAllFalse(anim);
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
            SetAllFalse(anim);
            anim.SetBool("IsJumping", true);
        }
        else
        {
            SetAllFalse(anim);
            anim.SetBool("IsIdle", true);
        }
        BowAnimations(bow);
    }
    bool CheckFacing()
    {
        if (transform.localScale.x < 0)
            return false;
        else
            return true;
    }
    void SetAllFalse(Animator ani)
    {
        foreach (AnimatorControllerParameter parameter in ani.parameters)
            ani.SetBool(parameter.name, false);
    }

    void BowAnimations(Animator bow)
    {
        if (bow.transform.childCount==2)
        {
            if (Input.GetMouseButton(0) && Input.GetMouseButton(1) && bow.GetBool("IsCocking"))
            {
                SetAllFalse(bow);
                bow.SetBool("IsIdle", true);
            }
            if (Input.GetMouseButtonDown(0))
            {
                SetAllFalse(bow);
                bow.SetBool("IsCocking", true);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                SetAllFalse(bow);
                bow.SetBool("IsReleasing", true);
            }
            foreach (AnimatorControllerParameter parameter in bow.parameters)
            {
                bool set = bow.GetBool(parameter.name);
                arrow.SetBool(parameter.name, set);
            }
        }
        else
        {
            SetAllFalse(bow);
        }
       
            
    }
}
