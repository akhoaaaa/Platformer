using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private float dirX, dirY;
    public LayerMask groundLayer;

    private bool moveLeft, moveRight;
    // private UpdateScore updateScore;

    private MapManager mapManager;

    private Animator anim;
    private bool isJump = true;

    [SerializeField]
    private int maxJump = 2;

    [SerializeField]
    private int jumpCount = 0;
    public Transform groundCheck;

    [SerializeField]
    private float moveSpeed = 1f, jumM;
    bool facingR = true;
    private bool isGrounded;
    private bool isLadder = false;
    private bool isLadderM = false;
    public GameObject PlayerUIM;
    public GameObject HelpPlayer;
    public bool isLadderT = false;


    void Start()
    {
        PlayerPrefs.DeleteAll();
        PlayerUIM.SetActive(true);
        Time.timeScale = 1;
        mapManager = FindObjectOfType<MapManager>();        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        moveLeft = false;
        moveRight = false;

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            isLadder = true;
            other.collider.isTrigger = true;
            anim.SetBool("ladder", true);
            isLadderT = true;
        }
        if (other.gameObject.CompareTag("LadderTop"))
        {
            if (isLadderT == true)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 1f);
                isLadderT = false;
                other.collider.isTrigger = false;
            }
        }
    }
    // public void NextMap()
    // {
    //     // SceneManager.LoadScene("Map" + (mapManager.mapIndex + 1));
    // }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.CompareTag("Item"))
        // {
        //     ItemController itemController = other.GetComponent<ItemController>();
        //     if (itemController != null)
        //     {
        //         updateScore.AddScore(itemController.point);
        //     }
        //     other.gameObject.SetActive(false);
        // }
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            mapManager.SavePoint();
            Debug.Log(transform.position);
        }
        if (other.CompareTag("LadderTop"))
        {
            if (isLadderT && isLadder && !isGrounded)
            {
                isLadderT = false;
                other.isTrigger = false;
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 1f);
                isLadder = false;
                other.isTrigger = false;
                isLadderT = true;
            }
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isLadder = false;
            anim.SetBool("ladder", false);
            other.isTrigger = false;
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("SampleScene");
    }


    void Update()
    {
        movePlayer();
        movePlayMobile();
        animator();
        if(Time.time > 10)
        {
            HelpPlayer.SetActive(false);
        }

    }

    private void movePlayer()
    {
        dirX = Input.GetAxis("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        if (isGrounded)
        {
            isJump = true;
            jumpCount = 0;
        }

        if (Input.GetButtonDown("Jump") && isJump == true)
        {
            jumpCount++;
            Jump();
            if (jumpCount >= maxJump)
            {
                isJump = false; 
            }
        }

        if (isLadder)
        {
            dirY = Input.GetAxis("Vertical");
            if (isLadderM == true)
            {
                dirY = 1;
                rb.gravityScale = 0;
                rb.velocity = new Vector2(rb.velocity.x, dirY * moveSpeed);
                anim.SetBool("ladder", true);
            }
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, dirY * moveSpeed);
            anim.SetBool("ladder", true);

        }
        else
        {
            rb.gravityScale = 1;
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
            anim.SetBool("ladder", false);
        }

    }

    private void animator()
    {
        if (facingR == true && dirX > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            facingR = false;
        }
        if (facingR == false && dirX < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            facingR = true;
        }
        if (Math.Abs(dirX) > 0 && rb.velocity.y == 0)
        {
            anim.SetBool("Run", true);
            anim.SetBool("Fail", false);
            Debug.Log("Run");

        }

        else
        {
            anim.SetBool("Run", false);
        }

        if (rb.velocity.y > 0)
        {
            anim.SetBool("Jump", true);
            anim.SetBool("Fail", false);
        }

        else if (rb.velocity.y < 0)
        {
            anim.SetBool("Jump", false);
            anim.SetBool("Fail", true);
            if (rb.velocity.y > 0)
            {
                anim.SetBool("Fail", false);
                anim.SetBool("ladder", true);
            }
        }
        else
        {
            anim.SetBool("Jump", false);
            anim.SetBool("Fail", false);
        }
    }

    private void movePlayMobile()
    {

        if (moveLeft == true)
        {
            dirX = -1;
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

            Debug.Log(rb.velocity);
        }
        else if (moveRight == true)
        {
            dirX = 1;
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        }
        animator();
    }
    public void JumpMobile()
    {
        jumpCount++;
        if (jumpCount < maxJump && isJump)
        {
            rb.velocity = new Vector2 (rb.velocity.x,0);
            Vector2 jump = new Vector2 (rb.velocity.x,jumM);
            rb.AddForce(jump,ForceMode2D.Impulse);
        }
        

    }

    public void JumpDown()
    {
        isLadderM = true;
    }
    public void JumpUp()
    {
        isLadderM = false;
    }

    public void MoveLeftDown()
    {
        moveLeft = true;
    }
    public void MoveLeftUp()
    {
        moveLeft = false;
    }

    public void MoveRightDown()
    {
        moveRight = true;
    }
    public void MoveRightUp()
    {

        moveRight = false;
    }


    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        if (!isLadder) 
        {
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y); 
        }
    }


    void Jump()
    {
        if (jumpCount < maxJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            Vector2 jump = new Vector2(0, jumM);
            rb.AddForce(jump, ForceMode2D.Impulse);
        }
    }
}
