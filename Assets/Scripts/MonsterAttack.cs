using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore;

public class MonsterAttack : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    private Animator anim;
    public bool facingR = true;
    public Transform detection;
    public bool isPlayer = false;
    private Vector2 startPos;

    // Start is called before the first frame update

    void Start()
    {
        anim = GetComponent<Animator>();
        startPos = transform.position;


    }
    private void Update()
    {
        MoveMonster();
        DetectPlayer();
    }

    private void MoveMonster()
    {
        if (player == null)
        {
            return;
        }
        if (isPlayer)
        {
            anim.SetBool("Walk", true);
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            if (facingR)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                if (player.position.x < transform.position.x && facingR)
                {
                    facingR = false;
                }
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                if (player.position.x > transform.position.x && !facingR)
                {
                    facingR = true;
                }
            }
        }
        else
        {
            anim.SetBool("Walk", false);
            transform.position = Vector2.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
        }
    }
    private void DetectPlayer()
    {
        // Kiểm tra xem người chơi có trong vùng phát hiện hay không
        if (detection != null && detection.GetComponent<Collider2D>().IsTouching(player.GetComponent<Collider2D>()))
        {
            isPlayer = true; 
        }
        else
        {
            isPlayer = false;  
        }
    }
}
