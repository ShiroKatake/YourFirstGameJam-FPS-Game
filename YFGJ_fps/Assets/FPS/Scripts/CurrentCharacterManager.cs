using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentCharacterManager : MonoBehaviour {
	public GameObject[] characters;
	private GameObject currentCharacter;
	private int characterIndex;

	void Start() {
		characters = GameObject.FindGameObjectsWithTag("Switchable");
	}

	// Update is called once per frame
	void Update() {

	}
}
