using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] heart;
    public int maxHealth= 3;
    public int health= 3;
    void Start()
    {
        health = maxHealth;
        Debug.Log(heart.Length);

        UpdateHealth();
    }

    public void Dama(int damage)
    {
        health -= damage;
        health = Mathf.Clamp(health,0,maxHealth);
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        for(int i = 0 ; i < heart.Length;i++)
        {
            if(i < health)
            {
                heart[i].SetActive(true);
            }
            else{
                heart[i].SetActive(false);
            }
        }
    }

    
}
