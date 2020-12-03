using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(Renderer))]
public class DepthSortByY : MonoBehaviour
{

    private const int yRange = 100;

   // public Transform Target;

   // public int TargetOffset = 0;




    // Update is called once per frame
    void Update()
    {
     //   if (Target == null)
      //  {
      //      Target = transform;
       // }
        Renderer renderer = GetComponent<Renderer>();
        // renderer.sortingOrder = -(int)(Target.position.y * yRange) + TargetOffset;
        renderer.sortingOrder = -(int)(transform.position.y * yRange);
    }
}
