using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Prime31;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

	public AudioClip attackSound;
	public AudioClip swordSound;
	public AudioClip femSound;
	public AudioClip jumpSound;
	public AudioClip dieSound;
	public AudioClip endSound;
	public AudioClip hohohohoho;
	public AudioClip boydie;
	public AudioClip ninjahurt;
	public AudioClip onfire;

    private AudioSource audioSource;

	public GameObject Balao;
	public string newGameScene;
	// public string NextLevel;
	// public int Level = 0;

	private Animator animator;
	public GameObject Man;
	public GameObject Boyfriend;
	public GameObject Girl;
	public GameObject Fem;
	public int manPoint = 4;
	public int boyfriendPoint = 10;
	public int ninjaPoint = 4;
 
 	public CharacterController2D.CharacterCollisionState2D flags;
 	public float walkSpeed = 4.0f;     // Depois de incluido, alterar no Unity Editor
 	public float jumpSpeed = 8.0f;     // Depois de incluido, alterar no Unity Editor
	public float doubleJumpSpeed = 6.0f; //Depois de incluido, alterar no Editor
 	public float gravity = 9.8f;       // Depois de incluido, alterar no Unity Editor

	public bool doubleJumped; // informa se foi feito um pulo duplo
 	public bool isGrounded;		// Se está no chão
 	public bool isJumping, isAttacking;		// Se está pulando
	public bool isDucking;
	public bool isFalling;      // Se estiver caindo
	public bool isFacingRight;      // Se está olhando para a direita
	public bool isDead;
	public bool isClimbing;
	public bool vitoria;

	public bool manJoy = false;


 	private Vector3 moveDirection = Vector3.zero; // direção que o personagem se move
 	private CharacterController2D characterController;	//Componente do Char. Controller

	private BoxCollider2D boxCollider;
   	private float colliderSizeY,colliderSizeX ;
   	private float colliderOffsetY, colliderOffsetX;
	public int feminism;
	


    void Start()
    {

		audioSource = GetComponent<AudioSource>();
		audioSource.clip = onfire;
        audioSource.loop = true;
		audioSource.Play();


		Instantiate(Girl, new Vector3(57f,7f, 0f), Quaternion.identity);
		Instantiate(Man, new Vector3(1f, -3.1f, 0f), Quaternion.identity);
		Instantiate(Man, new Vector3(18f, -3.1f, 0f), Quaternion.identity);
		Instantiate(Man, new Vector3(40f, -3.1f, 0f), Quaternion.identity);
		Instantiate(Boyfriend, new Vector3(55f, 7f, 0f), Quaternion.identity);
		vitoria = false;

		
		animator = GetComponent<Animator>();
    	characterController = GetComponent<CharacterController2D>(); //identif. o componente
		boxCollider = GetComponent<BoxCollider2D>();
		colliderSizeY = boxCollider.size.y;
       	colliderOffsetY = boxCollider.offset.y;
		colliderSizeX = boxCollider.size.x;
       	colliderOffsetX = boxCollider.offset.x;
	}

    void Update()
    {

		animator.SetFloat("movementX", Mathf.Abs(moveDirection.x/walkSpeed)); // +Normalizado
       	animator.SetFloat("movementY", moveDirection.y);
       	animator.SetBool("isGrounded", isGrounded);
       	animator.SetBool("isJumping", isJumping);
       	animator.SetBool("isDucking", isDucking);
       	animator.SetBool("isFalling", isFalling);
		animator.SetBool("isAttacking", isAttacking);
		animator.SetBool("isDead", isDead);
		animator.SetBool("isClimbing", isClimbing);


		if(moveDirection.y < 0)
           isFalling = true;
       else
           isFalling = false;

        moveDirection.x = Input.GetAxis("Horizontal"); // recupera valor dos controles
		moveDirection.x *= walkSpeed;

        if(moveDirection.x < 0) {
			transform.eulerAngles = new Vector3(0,180,0);
			isFacingRight = false;
		}
		else if(moveDirection.x > 0)
		{
			transform.eulerAngles = new Vector3(0,0,0);
			isFacingRight = true;
		}

		if(Input.GetButton("Fire1"))
			{
				//audioSource.PlayOneShot(attackSound);
				//audioSource.PlayOneShot(swordSound);
				isAttacking = true;
				boxCollider.size = new Vector2(2*colliderSizeX, colliderSizeY);
				boxCollider.offset = new Vector2(colliderOffsetX,colliderOffsetY);
				characterController.recalculateDistanceBetweenRays();
				
			}
		else{
			isAttacking = false;
			boxCollider.size = new Vector2(colliderSizeX, colliderSizeY);
			boxCollider.offset = new Vector2(colliderOffsetX,colliderOffsetY);
			characterController.recalculateDistanceBetweenRays();
		}

		if(isGrounded) {				// caso esteja no chão
			moveDirection.y = 0.0f; // se no chão nem subir nem descer
			isJumping = false;
			doubleJumped = false; // se voltou ao chão pode faz pulo duplo
			if(Input.GetButton("Jump"))
			{
				audioSource.PlayOneShot(jumpSound);
				moveDirection.y = jumpSpeed;
				isJumping = true;
			}

			
		}
      	else {            // caso esteja pulando 
         
			if(Input.GetButtonUp("Jump") && moveDirection.y > 0) // Soltando botão diminui pulo
			{	
				moveDirection.y *= 0.5f;
			}

			if(Input.GetButtonDown("Jump") && !doubleJumped) // Segundo clique faz pulo duplo
           	{
               moveDirection.y = doubleJumpSpeed;
			   audioSource.PlayOneShot(jumpSound);
               doubleJumped = true;
           	}

      	}

		moveDirection.y -= gravity * Time.deltaTime;	// aplica a gravidade

		animator.speed = 1.0f;
		if(isClimbing) {
			Debug.Log(Input.GetAxis("Vertical"));
			if(Input.GetAxis("Vertical") > 0) {
				moveDirection.y = walkSpeed;
				isJumping = true;
			}
			else if(Input.GetAxis("Vertical") < 0) {
				moveDirection.y = -walkSpeed;
			}
			else {
				if(!isGrounded) {
					Debug.Log("entrou aqui");
					moveDirection.y = 0.0f;
					animator.speed = 0.0f;
				}
			}
		}


		characterController.move(moveDirection * Time.deltaTime);	// move personagem	

		flags = characterController.collisionState; 	// recupera flags
		isGrounded = flags.below;				// define flag de chão

		if(Input.GetAxis("Vertical") < 0 && moveDirection.x == 0){
           	if(!isDucking){
               boxCollider.size = new Vector2(boxCollider.size.x, 2*colliderSizeY/3);
               boxCollider.offset = new Vector2(boxCollider.offset.x, colliderOffsetY-colliderSizeY/6);
               characterController.recalculateDistanceBetweenRays();
           	}
           	isDucking = true;
       		} else {
				if(isDucking){
					boxCollider.size = new Vector2(boxCollider.size.x, colliderSizeY);
					boxCollider.offset = new Vector2(boxCollider.offset.x, colliderOffsetY);
					characterController.recalculateDistanceBetweenRays();
					isDucking = false;
				}
       		}
    	}

	void OnTriggerEnter2D(Collider2D other)
    {

		if(other.gameObject.layer == LayerMask.NameToLayer("LadderEffectors")) {
           isClimbing = true;
       }


        if(other.gameObject.layer == LayerMask.NameToLayer("Man"))
        {
            if(isAttacking && !other.gameObject.GetComponent<man>().isJoy){
				manPoint -= 1;
				if (manPoint <= 0){
					manJoy = true;
					Vector3 posFem = other.gameObject.GetComponent<Transform>().position;
					posFem.y += 3f;
					//StartCoroutine(waiter());
					Instantiate(Fem, posFem, Quaternion.identity);
					manPoint = 4;
				}
				
                
            }
			else if(!other.gameObject.GetComponent<man>().isJoy){
				ninjaPoint -= 1;
				audioSource.PlayOneShot(ninjahurt);
				if (ninjaPoint <= 0){
					audioSource.PlayOneShot(dieSound);
					isDead = true;
					StartCoroutine(die());
				}
			}
            
        }

		if(other.gameObject.layer == LayerMask.NameToLayer("Fem"))
        {
			audioSource.PlayOneShot(femSound);
			feminism++;
			Destroy(other.gameObject);
		}

		if(other.gameObject.layer == LayerMask.NameToLayer("Boyfriend"))
        {
			if(isAttacking){
				boyfriendPoint -= 1;
				if (boyfriendPoint <= 0){
					audioSource.PlayOneShot(boydie);
					Destroy(other.gameObject);
					vitoria = true;
				}
			}

			else{
				ninjaPoint -= 1;
				audioSource.PlayOneShot(ninjahurt);
				if (ninjaPoint <= 0){
					audioSource.PlayOneShot(hohohohoho);
					isDead = true;
					StartCoroutine(wait());
					audioSource.PlayOneShot(dieSound);
					StartCoroutine(die());
				}
			}
			
		}

		if(other.gameObject.layer == LayerMask.NameToLayer("House"))
        {
			if(feminism >= 12){
				audioSource.PlayOneShot(endSound);
				StartCoroutine(end());
			}
			
		}

	}

	IEnumerator die(){
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene("GameOver");
	}
	IEnumerator wait(){
		yield return new WaitForSeconds(2f);
	}

	IEnumerator end(){
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene(newGameScene);
	}

	private void OnTriggerExit2D(Collider2D other) 
	{
       if(other.gameObject.layer == LayerMask.NameToLayer("LadderEffectors")) {
           isClimbing = false;
       }
   }

}
