using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Enemy : MonoBehaviour 
{

    private HealthManager healthMan;
    private float waitToHurt = 2f;
    private bool isTouching;
    [SerializeField]
    private int attackDamage = 10;

    void Start()
    {
        healthMan = FindObjectOfType<HealthManager>();
    }


    void Update()
    {
       /* if(reloading)
        {
          waitToLoad -= Time.deltaTime;  
          if(waitToLoad >= 0)
          {
            // SceneManager.LoadScene("DungeonCrawl");
          }
        }*/

        if(isTouching)
        {
            waitToHurt -= Time.deltaTime;
            if(waitToHurt <= 0)
            {
                healthMan.TakeDamage(attackDamage);
                waitToHurt = 2f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag == "Player")
        {
            //other.gameObject.SetActive(false);
             //reloading = true;

            other.gameObject.GetComponent<HealthManager>().TakeDamage(attackDamage);
           
       
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if(other.collider.tag == "Player")
        {
            isTouching = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.collider.tag == "Player")
        { 
            isTouching = false;
            waitToHurt = 2f;
        }
    }
}