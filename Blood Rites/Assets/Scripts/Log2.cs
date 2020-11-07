using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log2 : MonoBehaviour
{

    private Animator anim;
    private Transform target;
    public Transform homePos;
    [SerializeField]
    private float speed = 0;
    [SerializeField]
    private float maxRange = 0;
    [SerializeField]
    private float minRange = 0;

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
        anim.SetBool("wakeUp", true);
        FollowPlayer();
        }
        else if(Vector3.Distance(target.position, transform.position) >= maxRange)
        {
            ReturnHome();
        }
    }

    public void FollowPlayer()
    {
        anim.SetFloat("moveX", (target.position.x - transform.position.x));
        anim.SetFloat("moveY", (target.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);  
    }

    public void ReturnHome()
    {
        anim.SetFloat("moveX", (homePos.position.x - transform.position.x));
        anim.SetFloat("moveY", (homePos.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, homePos.position, speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, homePos.position) == 0)
        {
            anim.SetBool("wakeUp",false);
            anim.SetBool("SleepNow",true);
        }
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
