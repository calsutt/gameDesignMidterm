using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
		

	public float speed =1.0f;
	Rigidbody2D enemyBody;
	Transform enemyPosition;
	float enemyWidth;
	float enemyHeight;

	public LayerMask enemyMask;

	// Use this for initialization
	void Start () {

		SpriteRenderer enemySprite = this.GetComponent<SpriteRenderer> ();
		enemyBody = this.GetComponent<Rigidbody2D> ();

		enemyWidth = enemySprite.bounds.extents.x; //gets enemy width on x axis
		enemyHeight = enemySprite.bounds.extents.y; //gets enemy height on y axis

		enemyPosition = this.transform;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Vector2 lineCastPos = enemyPosition.position.toVector2 () - enemyPosition.right.toVector2 () * enemyWidth + Vector2.up * enemyHeight;

		bool isGrounded = Physics2D.Linecast (lineCastPos, lineCastPos + Vector2.down, enemyMask);

		bool isBlocked = Physics2D.Linecast (lineCastPos, lineCastPos - enemyPosition.right.toVector2 () * .05f);

		Debug.DrawLine (lineCastPos, lineCastPos - enemyPosition.right.toVector2 () * .05f);

		Debug.DrawLine (lineCastPos, lineCastPos + Vector2.down);

		Debug.Log ("IsGrounded: " + isGrounded + " isBlocked: " + isBlocked + "\nenemy position: " + enemyPosition.position +
			" speed: " + speed);
		if (!isGrounded || isBlocked) {

			Vector3 currRot = enemyPosition.eulerAngles; //rotation property of transform
			currRot.y += 180;
			enemyPosition.eulerAngles = currRot;

		}
		//If enemy is blocked or is not grounded
		//always move forward
		Vector2 myVelocity = enemyBody.velocity;
		myVelocity.x = enemyPosition.right.x * -speed;
		enemyBody.velocity = myVelocity;
	}

}
