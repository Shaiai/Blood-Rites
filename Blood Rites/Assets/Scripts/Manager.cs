using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    int remainingEnemy;

    // Start is called before the first frame update


    void Update()
    {
        remainingEnemy = GameObject.FindGameObjectsWithTag("enemy").Length;
        Debug.Log("Current Enemies Remaining: " + remainingEnemy);
    }

}
