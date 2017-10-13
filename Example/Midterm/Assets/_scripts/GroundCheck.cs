using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {


	private PlayerController player;
	// Use this for initialization
	void Start () {
		player = gameObject.GetComponentInParent<PlayerController> ();//component of a script or something like that

	}

	void OnTriggerEnter2D( Collider2D col)
	{
		player.isGrounded = true;
	}

	void OnTriggerStay2D( Collider2D col)
	{
		player.isGrounded = true;
	}

	void OnTriggerExit2D( Collider2D col)
	{
		player.isGrounded = false;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
