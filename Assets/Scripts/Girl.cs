using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl : MonoBehaviour
{

    public AudioClip happy;
    private AudioSource audioSource;

    public Animator anim_girl;
    public GameObject Ninja;
    // public GameObject Fem;
    public bool isSaved;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
		audioSource.Play();
        anim_girl = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        anim_girl.SetBool("isSaved", isSaved);
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if((other.gameObject.GetComponent<PlayerController>().feminism >= 2) && (!isSaved) && (other.gameObject.GetComponent<PlayerController>().vitoria)){
                // Vector3 posLove = other.gameObject.GetComponent<Transform>().position;
                // posLove.y += 3f;
                // Instantiate(Fem, posLove, Quaternion.identity);
                other.gameObject.GetComponent<PlayerController>().feminism += 10;
                isSaved = true;
                audioSource.PlayOneShot(happy);
            }
            
        }
    }
}
