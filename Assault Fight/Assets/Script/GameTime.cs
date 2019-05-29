using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    [SerializeField] float time;
    

    // Update is called once per frame
    void Update()
    {
        time = Time.time;    
    }
}
