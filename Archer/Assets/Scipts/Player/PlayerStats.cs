using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    float Gold;
    float Health;
    string Arrowtype;
    public bool OutofAmmo = true; 
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {


        if (transform.GetChild(0).GetChild(0).GetChild(0).childCount == 2)
        {
            OutofAmmo = false;
        }
        else
        {
            OutofAmmo = true;
            PickupArrow();
        }
       


    }


    private void PickupArrow()
    {
        if(Input.GetKey("e")) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit)
            {
                Transform a = hit.transform;
                if (hit.collider.tag.Contains("Arrow"))
                {
                    hit = Physics2D.Raycast(transform.Find("PlayerSprite").position,a.transform.position- transform.Find("PlayerSprite").position,Mathf.Infinity,~LayerMask.GetMask("Player"));
                    if (hit)
                    {
                        if (Vector2.Distance(transform.Find("PlayerSprite").position, a.transform.position) <= 6.5)
                        {                      
                            GameObject g = Resources.Load<GameObject>("Arrows/" + hit.transform.tag);
                            Instantiate(g,transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).position,transform.GetChild(0).GetChild(0).GetChild(0).rotation,transform.GetChild(0).GetChild(0).GetChild(0));
                            Destroy(hit.transform.gameObject);
                        }
                    }
                }

            }
        }
    }
    public bool CheckAmmo()
    {
        return OutofAmmo;
    }

}
