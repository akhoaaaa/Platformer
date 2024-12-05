using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject bulletPrefab;
    public float shootF = 5f;
    [SerializeField]
    private Transform shootPoint;
    private Transform player;
    public bool canShoot = true;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (shootPoint == null)
        {
            shootPoint = transform;
        }

    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canShoot)
        {
            StartCoroutine(ShootPlayer());
        }
    }

    private IEnumerator ShootPlayer()
    {
        canShoot = false;
        // Tính toán hướng bắn từ quái vật đến người chơi
        Vector2 direction = (player.position - transform.position).normalized;


        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * shootF;

        Destroy(bullet, 1f);
        yield return new WaitForSeconds(1f);
        canShoot = true;
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Khi người chơi ra khỏi vùng, dừng bắn
            StopCoroutine(ShootPlayer());
        }
    }
}
