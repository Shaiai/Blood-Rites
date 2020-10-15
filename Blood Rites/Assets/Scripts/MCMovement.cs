using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCMovement : MonoBehaviour
{
    private Rigidbody2D mainCharacter;

    public float speed;
    private Vector3 change;

    private Animator animation;
  


    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animator>();
        mainCharacter = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Each frame will reset the characters to movement to standing still. 
        change = Vector3.zero;

        //Get the key press for horizontal movement.
        change.x = Input.GetAxis("Horizontal");
        //Get the key press for vertical movement.
        change.y = Input.GetAxis("Vertical");

        //Calls function to move and animate the character.
        UpdateAnimationAndMove();
    }

    void UpdateAnimationAndMove()
    {

        //Calls function to move the character.
        if(change != Vector3.zero)
        {
            MoveCharacter();
            animation.SetFloat("moveX", change.x);
            animation.SetFloat("moveY", change.y);
            animation.SetBool("moving", true);
        }
        else
        {
            animation.SetBool("moving", false);
        }
    }


    void MoveCharacter()
    {
        //This changes the position of the character  based on pre set perameters.
        mainCharacter.MovePosition(transform.position + change * speed * Time.deltaTime);
    }
}
