using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockstateCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Cursor.lockState);
        Screen.fullScreen = true;
    }
}
