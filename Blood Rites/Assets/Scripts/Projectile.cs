using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed;
    private Vector2 target;
    public Transform player;
    public float projectileLife;
    public float maxTime;


    private HealthManager healthMan;
    [SerializeField]
    private int attackDamage = 10;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);

       healthMan = FindObjectOfType<HealthManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime); 

        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }

        if(projectileLife <= 0)
        {
            DestroyProjectile();
            projectileLife = maxTime;
        }
        else
        {
            projectileLife -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
         if(other.CompareTag("Player")){
            DestroyProjectile();
            other.gameObject.GetComponent<HealthManager>().TakeDamage(attackDamage);
         }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
