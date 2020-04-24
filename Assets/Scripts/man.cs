using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class man : MonoBehaviour
{
    public AudioClip grunt;
    private AudioSource audioSource;

    public GameObject Ninja;
    public GameObject balao;
    private Animator animator;
    public GameObject clone;
    public bool isCrying = false;
    public bool isJoy = false;
    bool attack;

    
    // Start is called before the first frame update
    void Start()
    {

        audioSource = GetComponent<AudioSource>();
		audioSource.Play();

        Vector3 bpos = GetComponent<Transform>().position;
        bpos.y += 3;
        bpos.x += 2;
        clone = (GameObject)Instantiate(balao, bpos, Quaternion.identity);
        animator = GetComponent<Animator>();

        //Destroy(clone, 1.0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        //Control = Ninja.GetComponent("PlayerController");
        animator.SetBool("isCrying", isCrying);
        animator.SetBool("isJoy", isJoy);
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(GameObject.Find("Ninja").GetComponent<PlayerController>().isAttacking){
                isCrying = true;
                audioSource.PlayOneShot(grunt);
			    Destroy(clone);
                if(GameObject.Find("Ninja").GetComponent<PlayerController>().manJoy){
                    GameObject.Find("Ninja").GetComponent<PlayerController>().manJoy = false;
                    isJoy = true;
                }
                
            }
                        
        }
    }
}
