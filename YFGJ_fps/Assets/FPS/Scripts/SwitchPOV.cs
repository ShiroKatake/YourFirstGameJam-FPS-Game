using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPOV : MonoBehaviour {
	Health m_Health;
	EnemyManager m_EnemyManager;
	MouseManager mm;

	public bool isEmpty { get; private set; }
	//Side note, do something about being able to select yourself

	// Start is called before the first frame update
	void Start() {
		mm = FindObjectOfType<MouseManager>();
		m_EnemyManager = FindObjectOfType<EnemyManager>();
		m_Health = GetComponent<Health>();
		m_Health.onDie += OnDie;
	}

	// Update is called once per frame
	void Update() {
		if (mm.selectedObject != null && Input.GetButtonDown("Use")) {
			Switch(mm.selectedObject);
		}
	}

	void Switch(GameObject target) {
		//If 
		//Update UI with "target" health (name, health, and possibly ammo(?))
		//Disable player controls in this.transform (old enemy)
		//Disable enemy operating script in "target" (new enemy)
		//Enable player controls in "target" (new enemy)
		//Enably enemy operating script in this.transform (old enemy)
		//Move camera from old parent to "target" parent (new enemy) (inherits postion and rotation) (requires camera movement, possibly translating/lerping)
	}

	void OnDie() {
		//If no enemies are left to switch, you lose
		if(m_EnemyManager.enemies.Count <= 0) {
			isEmpty = true;
		} else {
			//Find nearest switchable as a GameObject
			//Then Switch()
		}
	}
}
