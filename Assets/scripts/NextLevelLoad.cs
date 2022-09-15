using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelLoad : MonoBehaviour
{
    public void loadLevel(int index)
    {
        LoadingLevel.SwitchToScene(index);
    }
    
}
