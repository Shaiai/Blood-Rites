using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //This will get the position of the objet the camera will follow.
    public Transform target;
    //The rate at which the camera will chase the object.
    public float smoothing;

    public Vector2 maxPosition;
    public Vector2 minPosition;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(transform.position != target.position)
        {
                //This vector will be used to make sure the camera never goes too far from or behind the scene.
                Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

                //Fixates the camera to your scene using the top right as max values and min values from bottom left. / Bounds Camera.
                targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
                targetPosition.y = Mathf.Clamp(targetPosition.y,minPosition.y,maxPosition.y);

                //This will change the position of the camera to that of the target.
                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);

                
        }
    }
}
