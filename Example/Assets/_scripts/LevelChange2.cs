using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelChange2 : MonoBehaviour {

	// Use this for initialization

	void OnTriggerEnter2d(Collision2D other){
		SceneManager.LoadScene ("scene3");
		Debug.Log ("Here");
	}

}
