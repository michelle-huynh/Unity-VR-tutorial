using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleExample : MonoBehaviour
{
    private int number = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("This is a log with the value of an int variable: " + number);
        Debug.LogWarning("This is a warning with the name of this gameobject: " + gameObject.name);
        Debug.LogError("This is an error with the time: " + System.DateTime.Now);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
