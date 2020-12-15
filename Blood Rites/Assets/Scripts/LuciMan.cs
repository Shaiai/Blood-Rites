using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuciMan : MonoBehaviour
{

    private Animator anim;
    private Transform target;
    [SerializeField]
    private float speed = 0;
    [SerializeField]
    private float maxRange = 0;
    [SerializeField]
    private float minRange = 0;

    public GameObject projectile;
    public float shotTimer;
    public float maxTime;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        target = FindObjectOfType<PlayerMovement>().transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange)
        {
        anim.SetBool("chasing", true);
        FollowPlayer();
        }
        else if(Vector3.Distance(target.position, transform.position) >= minRange && Vector3.Distance(target.position,transform.position) <= maxRange)
        {
            transform.position = this.transform.position;
        }
        else if(Vector3.Distance(transform.position, target.position) < minRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, -speed * Time.deltaTime);
        }

        if(shotTimer <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            shotTimer = maxTime;
        }
        else 
        {
            shotTimer -= Time.deltaTime;
        }

    }

    public void FollowPlayer()
    {
        anim.SetFloat("moveX", (target.position.x - transform.position.x));
        anim.SetFloat("moveY", (target.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);  
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "WeaponHitBox")
        {
            Vector2 difference = transform.position - other.transform.position;
            transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);
        }
    }

  
}
