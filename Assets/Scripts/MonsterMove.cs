using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class MonsterMove : MonoBehaviour
{
    private Rigidbody2D rb;
    
    public bool facingR= false;
    [SerializeField]
    private float speed = 4f;
    
    private float min = -2f, max = 2f;

    private GameObject player;



    private float start;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        start = transform.position.x;
    }

    

    // Update is called once per frame
    void Update()
    {
        Move();
       
    }

    

    private void Move()
    {
        if(facingR == true)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            rb.velocity = new Vector2 (speed, rb.velocity.y);
            if(transform.position.x > start + max)
            {
                facingR = false;
            }
        }
        else{
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rb.velocity = new Vector2 (-speed, rb.velocity.y);
            if(transform.position.x <= start + min)
            {
                facingR = true;
            }
        }
    }
}
