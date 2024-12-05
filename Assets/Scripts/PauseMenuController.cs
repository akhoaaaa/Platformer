using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject pauseMenuUI;
    public int score = 0; // Biến lưu trữ điểm số
    private Vector3 initPosPlayer;
    private MapManager mapManager;

    private HeartManager heartManager;

    public GameObject player;

    public GameObject[] item;

    void Start()
    {
        heartManager = GameObject.FindObjectOfType<HeartManager>();
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
        if (player != null)
        {
            initPosPlayer = player.transform.position;
        }
        mapManager = FindObjectOfType<MapManager>();
    }
    public void ResetGame()
    {
        // updateScore.ResetScore();
        pauseMenuUI.SetActive(false);
        player.transform.position = initPosPlayer;
        heartManager.health = 3;
        heartManager.UpdateHealth();
        mapManager.LoadCheckpoint();
        foreach (GameObject item in item)
        {
            item.SetActive(true);
        }
        Time.timeScale = 1;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Death"))
        {
            heartManager.Dama(1);

            if (heartManager.health == 0)
            {
                Time.timeScale = 0;
                pauseMenuUI.SetActive(true);

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Death"))
        {
            heartManager.Dama(1);

            if (heartManager.health == 0)
            {
                Time.timeScale = 0;

                pauseMenuUI.SetActive(true);

            }
        }
        if(other.CompareTag("DeathTile"))
        {
            heartManager.Dama(3);
            if (heartManager.health == 0)
            {
                Time.timeScale = 0;
                pauseMenuUI.SetActive(true);
            }
        }
    }
}
