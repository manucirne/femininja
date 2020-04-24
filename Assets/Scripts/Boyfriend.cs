using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boyfriend : MonoBehaviour
{

    public AudioClip grunt;
    private AudioSource audioSource;

    public GameObject Ninja;
    public GameObject balaoB;
    private Animator animator;
    public GameObject clone;
    public bool isCrying = false;
    public bool isJoy = false;
    bool attack;
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {

        audioSource = GetComponent<AudioSource>();
		audioSource.Play();

        Vector3 bpos = GetComponent<Transform>().position;
        bpos.y += 3;
        bpos.x += 2;
        clone = (GameObject)Instantiate(balaoB, bpos, Quaternion.identity);
        animator = GetComponent<Animator>();
        count = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isCrying", isCrying);

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(GameObject.Find("Ninja").GetComponent<PlayerController>().isAttacking){
                isCrying = true;
                audioSource.PlayOneShot(grunt);
                if(count >= 5){
                    Destroy(clone);
                }
                count++;
			    
                
            }
                        
        }
    }
}
