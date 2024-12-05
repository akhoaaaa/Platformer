using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveTrap : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 2f;
    public bool top = true;
    public Transform startPosition;

    public Transform endPosition;
    public float threshold = 0.1f; // Ngưỡng nhỏ để xác định đối tượng đã đến đích hay chưa

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {

        if (top)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPosition.position, speed * Time.deltaTime);

            // Kiểm tra xem đối tượng có tới gần điểm cuối chưa
            if (Vector2.Distance(transform.position, endPosition.position) < threshold)
            {
                top = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, startPosition.position, speed * Time.deltaTime);

            // Kiểm tra xem đối tượng có tới gần điểm đầu chưa
            if (Vector2.Distance(transform.position, startPosition.position) < threshold)
            {
                top = true;
            }
        }
    }
}
