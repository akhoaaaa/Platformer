using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicGround;
    public AudioClip backGround;
    public GameObject onMusic;
    public GameObject offMusic;
    
    void Start() {
        musicGround.clip = backGround;
        musicGround.Play();
        offMusic.SetActive(false);
        Debug.Log("play");
    }

    public void On(){
        onMusic.SetActive(true);
        offMusic.SetActive(false);
        musicGround.Play();
    }
    public void Off(){
        offMusic.SetActive(true);
        onMusic.SetActive(false);
        musicGround.Stop();
    }
}
