using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    int remainingEnemy;

    public int dealsTaken;
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }


    void Update()
    {
        remainingEnemy = GameObject.FindGameObjectsWithTag("enemy").Length;
        Debug.Log("Current Enemies Remaining: " + remainingEnemy);
    }

    void SaveDecisions()
    {
        Manager.Instance.dealsTaken = dealsTaken;
    }
}
