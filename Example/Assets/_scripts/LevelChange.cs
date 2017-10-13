using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelChange : MonoBehaviour {

	// Use this for initialization

	void OnTriggerEnter2d(Collision2D other){
		SceneManager.LoadScene ("firstlevel");
	}

}
