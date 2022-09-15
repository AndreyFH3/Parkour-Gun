using System;
using System.Collections.Generic;
using UnityEngine;

public class singleton : MonoBehaviour
{
    public static singleton Instance { get { if (_instance == null)
            {
                var go = new GameObject("Translate");
                _instance = go.AddComponent<singleton>();
                DontDestroyOnLoad(go);
            }
        return _instance; 
        } 
    } 
    private static singleton _instance;

    public Action translate;

    public void translateInvoke() => translate.Invoke();
    
}
