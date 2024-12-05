using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTile : MonoBehaviour
{
    public float unActiveTime = 1f;
    public float time = 0f;
    private bool isPlayer = false;
    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            isPlayer = true;
            time = time + Time.deltaTime;
            if(time >= unActiveTime)
            {
                gameObject.SetActive(false);
                Invoke("ReactivateTile", 2f);
            }
            
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            isPlayer = false;
            time = 0f;  // Reset thời gian đếm
        }
    }

    private void ReactivateTile()
    {
        if (!isPlayer)
        {
            // Tạo lại đối tượng ở vị trí cũ
            gameObject.SetActive(true);
        }

    }
}
