using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class flagscript : MonoBehaviour
{
    public string SceneName;
    public GameObject particles;
    bool settimer = false;
    float cooldown = 5;
    float cooldowntime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (settimer)
        {
            if (cooldowntime <= 0)
                SceneManager.LoadScene(SceneName);
            else
                cooldowntime -= Time.deltaTime;


        }
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.layer == 8)
        {
            print("Move on");
            particles.SetActive(true);
            cooldowntime = cooldown;
            settimer = true;
        }

    }
}
