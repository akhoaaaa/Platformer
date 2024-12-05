using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject gameStart;
    public GameObject SettingUI;
    public GameObject MapUI;
    // private MapManager mapManager;

    // Start is called before the first frame update
    void Start()
    {
        // mapManager = FindObjectOfType<MapManager>();
        ShowMenu();
    }

    private void ShowMenu()
    {
        gameStart.SetActive(true);
    }
    // public void ShowMapManager()
    // {
    //     // MapUI.SetActive(true);
    //     gameStart.SetActive(false);

    //     // for (int i = 0; i < btn.Length; i++)
    //     // {

    //     //     int mapNumber = i + 1;
    //     //     btn[i].onClick.RemoveAllListeners();
    //     //     if (i == 0 || mapManager.isCompleteMap(i))
    //     //     {
    //     //         btn[i].interactable = true;
    //     //         btn[i].onClick.AddListener(() => LoadMap(mapNumber));
    //     //     }
    //     //     else
    //     //     {
    //     //         btn[i].interactable = false;
    //     //     }

    //     // }
    // }

    // private void LoadMap(int mapNumber)
    // {
    //     if (mapManager.isCompleteMap(mapNumber - 1) || mapNumber == 1)
    //     {
    //         UnityEngine.SceneManagement.SceneManager.LoadScene("Map" + mapNumber);
    //     }
    // }

    public void StartGame()
    {
        SceneManager.LoadScene("Map1");
    }

    public void BackMenu()
    {
        MapUI.SetActive(false);
        gameStart.SetActive(true);
    }

}
