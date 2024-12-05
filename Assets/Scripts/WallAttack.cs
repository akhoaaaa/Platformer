using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAttack : MonoBehaviour
{
    public Transform player;
    public GameObject bulletPrefab;
    public Transform shootPoint;
    private Rigidbody2D rb;
    public float shootF = 10f;
    public float shootDelay = 2f;
    private Animator anim;
    public bool canShoot = true;

    public bool isPlayer = false;
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame



    private IEnumerator shootBullet()
    {
        while (isPlayer && canShoot)
        {
            Vector2 direction = (player.position - shootPoint.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.Euler(0, 0, 0));
            rb = bullet.GetComponent<Rigidbody2D>();
            anim.SetTrigger("shot");
            rb.velocity = direction * shootF;
            // Phá hủy viên đạn sau 1 giây
            Destroy(bullet, 1f);
            Debug.Log("shott");
            canShoot = false;
            yield return new WaitForSeconds(shootDelay);
            canShoot = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayer = true;
            anim.SetTrigger("open");

            StartCoroutine(shootBullet());
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("idle");
            isPlayer = false;
            StopCoroutine(shootBullet());
        }
    }
}
