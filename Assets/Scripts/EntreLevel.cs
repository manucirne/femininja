using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntreLevel : MonoBehaviour
{

    public AudioClip beHappy;

    private AudioSource audioSource;

    int Level = 0;
    string NextLevel = "MainMenu";
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
		audioSource.clip = beHappy;
        audioSource.loop = true;
		audioSource.Play();

        Level++;
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void Next()
    {
        SceneManager.LoadScene(NextLevel);

    }

    public void Again()
    {
        SceneManager.LoadScene("Level1");

    }
}
