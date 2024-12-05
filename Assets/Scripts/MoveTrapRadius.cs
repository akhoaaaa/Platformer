using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using Unity.Mathematics;
using UnityEngine;

public class MoveTrapRadius : MonoBehaviour
{
    [SerializeField]
    public float speed = 500f;
    void Update()
    {
        MoveRadius();
        
    }
    void MoveRadius(){
        float angle = Mathf.PingPong(Time.time * speed, 200f) -100f;
        transform.rotation = Quaternion.Euler(0, 0, angle);    
    }
}
