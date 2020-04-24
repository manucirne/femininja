using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{

    public AudioClip sadGirl;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {

        audioSource = GetComponent<AudioSource>();
		audioSource.clip = sadGirl;
        audioSource.loop = true;
		audioSource.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
