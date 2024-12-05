using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public Transform player;
    public Vector2 startPoint;
    void Start()
    {
        startPoint = player.position;
    }
    public void SavePoint()
    {
        PlayerPrefs.SetFloat("Checkpoint_x", player.position.x);
        PlayerPrefs.SetFloat("Checkpoint_y", player.position.y);
    }
    public void LoadCheckpoint()
    {
        if (PlayerPrefs.HasKey("Checkpoint_x") && PlayerPrefs.HasKey("Checkpoint_y"))
        {
            float x = PlayerPrefs.GetFloat("Checkpoint_x");
            float y = PlayerPrefs.GetFloat("Checkpoint_y");

            Vector2 checkpointPosition = new Vector2(x, y);
            player.position = checkpointPosition;
        }
        else
        {
            player.position = startPoint;
        }
    }


}
