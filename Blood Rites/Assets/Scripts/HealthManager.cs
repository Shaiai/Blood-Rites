using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    //Reference to our health bear and precursors for default health settings.
    public HealthBar healthBar;
    public int maxHealth = 100;
    public int currentHealth;

    //Variables to handle the character flash for when we take damage.
    private bool flashActive;
    [SerializeField]
    private float flashLen = 0f;
    private float flashCounter = 0f;

    private SpriteRenderer playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        //Set health to 100 at the beginning of scene.
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(flashActive)
        {
            if(flashCounter > flashLen *.99f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }
            else if(flashCounter > flashLen *.82f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            }
            else if(flashCounter > flashLen *.66f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }
            else if(flashCounter > flashLen *.49f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            }
            else if(flashCounter > flashLen *.33f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }
            else if(flashCounter > flashLen *.16f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            }
            else if(flashCounter > 0)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }
            else
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
                flashActive = false;
            }
            flashCounter -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        //When character takes damage, we want to subtract health by damage.
        currentHealth -= damage;

        flashActive = true;
        flashCounter = flashLen;

        //Set healthabr status to current health after taking daamge.
        healthBar.SetHealth(currentHealth);


        //When health is 0, character dies.
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
