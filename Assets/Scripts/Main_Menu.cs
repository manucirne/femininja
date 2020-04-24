using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public AudioClip kungFu;

    private AudioSource audioSource;

     public string newGameScene;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("feminism",0);
        PlayerPrefs.SetInt("ninjaPoint",4);

        audioSource = GetComponent<AudioSource>();
		audioSource.clip = kungFu;
        audioSource.loop = true;
		audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        print(PlayerPrefs.GetInt("ninjaPoint", 0));
        
    }

    public void Start_game()
    {
        SceneManager.LoadScene(newGameScene);

    }
}
