using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    private Rigidbody2D myPlayer;
    private Animator anim;



    [SerializeField]
    private float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
       myPlayer = GetComponent<Rigidbody2D>(); 
       anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       myPlayer.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * (speed * Time.deltaTime);

        if(myPlayer.velocity != Vector2.zero)
        {
        anim.SetFloat("moveX", myPlayer.velocity.x);
        anim.SetFloat("moveY", myPlayer.velocity.y);
        anim.SetBool("moving", true);
        }
        else
        {
        anim.SetBool("moving", false);
        }

    }
}
