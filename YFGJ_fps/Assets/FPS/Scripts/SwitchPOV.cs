using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwitchPOV : MonoBehaviour {
	public GameObject currentBody;
	public GameObject newBody;
	public float distance;
	Health m_Health;
	EnemyManager m_EnemyManager;
	MouseManager m_MouseManager;
	GameObject m_currentBodyControlComponent;
	GameObject m_newBodyControlComponent;

	public bool isMoving;

	public bool isEmpty { get; private set; }
	//Side note, do something about being able to select yourself

	// Start is called before the first frame update
	void Start() {
		m_MouseManager = FindObjectOfType<MouseManager>();
		m_EnemyManager = FindObjectOfType<EnemyManager>();
		currentBody = transform.root.gameObject;
		m_currentBodyControlComponent = currentBody.GetComponentInChildren<PlayerEnemyMove>().gameObject;
		m_Health = transform.root.GetComponent<Health>();
		m_Health.onDie += OnDie;
	}

	// Update is called once per frame
	void Update() {
		if (m_MouseManager.selectedObject != null) {
			newBody = m_MouseManager.selectedObject;
			if (Input.GetButtonDown("Switch")) {
				Switch(m_MouseManager.selectedObject);
			}
		}
		if (isMoving) {
			MoveCamera();
		}
	}

	void Switch(GameObject target) {
		m_Health.onDie -= OnDie;
		m_newBodyControlComponent = target.GetComponentInChildren<PlayerEnemyMove>(true).gameObject;
		newBody = m_newBodyControlComponent.transform.root.gameObject;

		m_Health = newBody.GetComponent<Health>();
		m_Health.onDie += OnDie;

		m_newBodyControlComponent.SetActive(true);
		transform.SetParent(m_newBodyControlComponent.transform, true);

		m_currentBodyControlComponent.SetActive(false);
		newBody.GetComponent<EnemyMobile>().enabled = false;
		newBody.GetComponent<NavMeshAgent>().enabled = false;

		currentBody.GetComponent<NavMeshAgent>().enabled = true;
		currentBody.GetComponent<EnemyMobile>().enabled = true;

		isMoving = true;
		currentBody = newBody;
		m_currentBodyControlComponent = m_newBodyControlComponent;

		//transform.position = Vector3.Lerp(transform.position, targetPlayerComponent.transform.position, 0.5f);
		//Update UI with "target" health (name, health, and possibly ammo(?))
		//Disable player controls in this.transform (old enemy)
		//Disable enemy operating script in "target" (new enemy)
		//Enable player controls in "target" (new enemy)
		//Enably enemy operating script in this.transform (old enemy)
		//Move camera from old parent to "target" parent (new enemy) (inherits postion and rotation) (requires camera movement, possibly translating/lerping)
	}

	void MoveCamera() {
		transform.position = Vector3.MoveTowards(transform.position, m_newBodyControlComponent.transform.position, 10f * Time.deltaTime);
		distance = Round(Vector3.Distance(transform.position, m_newBodyControlComponent.transform.position), 2);
		if (distance == 0) {
			isMoving = false;
		}
	}

	float Round(float value, int digits) {
		float mult = Mathf.Pow(10.0f, digits);
		return Mathf.Round(value * mult) / mult;
	}

	GameObject FindClosestSwitchable() {
		float distanceToClosestSwitchable = Mathf.Infinity;
		EnemyController closestSwitchable = null;
		List<EnemyController> switchables = m_EnemyManager.switchables;
		switchables.Remove(transform.root.GetComponent<EnemyController>());
		foreach (var switchable in switchables) {
			float distanceToSwitchable = (switchable.transform.position - transform.position).sqrMagnitude;
			if(distanceToSwitchable < distanceToClosestSwitchable) {
				distanceToClosestSwitchable = distanceToSwitchable;
				closestSwitchable = switchable;
			}
		}
		Debug.Log(closestSwitchable.gameObject);
		return closestSwitchable.gameObject;
	}

	void OnDie() {
		//If no enemies are left to switch, you lose
		if (m_EnemyManager.switchables.Count <= 0) {
			transform.parent = null;
			isEmpty = true;
		} else {
			Switch(FindClosestSwitchable());
		}
	}
}
