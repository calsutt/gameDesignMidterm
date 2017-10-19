using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour {

    //create variables here
    // m_ usually means a member variable
    public float velocity;
    public float maxVelocity;
    public float jumpForce;

    public bool isGrounded;

    private Rigidbody2D rigBody;
    private Animator anim;

    public AudioClip jumpSFX;
	public AudioClip coinCollect;
	public AudioClip damageSFX;


    AudioSource audio;

	public GameObject respawn;

	private GameMaster gm;


	public int curHealth=3;
	public int maxHealth =3;
	public bool deathCheck;
	public bool hurt;

	public GameObject bullet;
	public Transform bulletPoint;

	//game over overlay
	public GameObject gameOverScreen;

    //tend to not write anything in void awake, causes lag
    /* private void Awake()
    {
    }
    //every fixed frame
     void FixedUpdate()
    {
    }*/
    // Use this for initialization
    void Start () {
        rigBody = gameObject.GetComponent<Rigidbody2D>();//get give us access to Ridhgidbody2d component
        anim = gameObject.GetComponent<Animator>();//get access to animator component

        audio = GetComponent<AudioSource>();//get access to audio component

		gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<GameMaster> ();
		gameOverScreen.SetActive (false);
	}
	
	//Update is called once per frame
	void Update () {
        anim.SetBool("isGrounded", isGrounded); //setting grounded value in animation
        anim.SetFloat("Speed", Mathf.Abs(rigBody.velocity.x)); //setting velocity value in animation
		anim.SetBool("isAlive", deathCheck);
		anim.SetBool("isDamaged", hurt);


        float h = Input.GetAxis("Horizontal");//nonmember variable because declared in a function
			
        if (Input.GetAxis("Horizontal") < -0.001f)
        {
			transform.localScale = new Vector3(-1f, 1f, 1f); 
        }
        if (isGrounded)
        {
            rigBody.AddForce((Vector2.right * velocity)*h);//allows player to move left and rightx`
        }
        if (Input.GetAxis("Horizontal") > 0.001f)
        {
			transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigBody.AddForce(Vector2.up * jumpForce); //adding force vertically to jump 
            audio.PlayOneShot(jumpSFX, 1f); //plays jump sound
        }
        if (!isGrounded)
        {
            velocity = 150f;//signifies air speed
        }
        else
        {
            velocity = 200f;//signifies ground speed
        }

		if (curHealth > maxHealth) {
			curHealth = maxHealth;
		}
		if (curHealth <= 0) {
			StartCoroutine ("DelayedRestart");
		}
		if (Input.GetKeyDown (KeyCode.Z)) {
			Instantiate (bullet, bulletPoint.position, bulletPoint.rotation);
		}
		if (Input.GetKeyDown (KeyCode.R) && deathCheck) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
		if (!deathCheck) {
			Time.timeScale = 1;
		}
    }
	void OnTriggerEnter2D(Collider2D col){
		if (col.CompareTag ("KillZone")) {
			transform.position = respawn.transform.position; //this shouldnt be finish but i think i fucked up the tags...shit was buggin i promise. dont worry i got you cal

		}
		if (col.CompareTag ("Coin")) {
			Destroy (col.gameObject);
			gm.points += 1;
			audio.PlayOneShot (coinCollect, 1.0f);
		}
		if (col.CompareTag ("gem")) {
			Destroy (col.gameObject);
			gm.points += 5;
			audio.PlayOneShot (coinCollect, 1.0f);
		}
		if (col.CompareTag ("level2Box")) {
			SceneManager.LoadScene ("scene2");
			Debug.Log ("SCENE CHANGED");
		}
	}
	void FixedUpdate () 
	{

		Vector3 easeVelocity = rigBody.velocity;
		easeVelocity.y = rigBody.velocity.y;
		easeVelocity.z = 0.0f;
		easeVelocity.x *= 0.0f;
		if (isGrounded) 
		{

			rigBody.velocity = easeVelocity;

		}

		//Limiting the speed of the character*
		if ( rigBody.velocity.x > maxVelocity ) 

		{

			rigBody.velocity = new Vector2 ( maxVelocity, 0f );

		}

		if ( rigBody.velocity.x < -maxVelocity ) 

		{

			rigBody.velocity = new Vector2 ( -maxVelocity, 0f );

		}
	}
	void Death (){
		deathCheck = true;

		if(deathCheck){
			Debug.Log ("We Dead");
			gameOverScreen.SetActive (true);
			Time.timeScale = 0;//freeze game
		}

	}
	IEnumerator DelayedRestart(){
		yield return new WaitForSeconds (1);
		Death ();
	}
	public void Damage(int dmg){
		audio.PlayOneShot (damageSFX, 1.0f);
		curHealth -= dmg;
	}
}	
