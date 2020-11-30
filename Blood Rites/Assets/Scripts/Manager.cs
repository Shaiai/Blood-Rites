using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

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

    void SaveDecisions()
    {
        Manager.Instance.dealsTaken = dealsTaken;
    }
}
