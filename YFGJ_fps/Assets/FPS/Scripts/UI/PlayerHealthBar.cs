using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour {
	[Tooltip("Image component dispplaying current health")]
	public Image healthFillImage;

	public GameObject currentBody;
	GameObject newBody;
	Health m_PlayerHealth;

	SwitchPOV pov;

	private void Start() {
		if (FindObjectOfType<PlayerCharacterController>() == null) {
			currentBody = FindObjectOfType<PlayerEnemyMove>().transform.root.gameObject;
		} else {
			currentBody = FindObjectOfType<PlayerCharacterController>().gameObject;
		}
		m_PlayerHealth = currentBody.GetComponent<Health>();
		pov = FindObjectOfType<SwitchPOV>();
	}

	void Update() {
		newBody = pov.currentBody;
		if (currentBody != newBody) {
			currentBody = newBody;
			m_PlayerHealth = currentBody.GetComponent<Health>();
		}
		// update health bar value
		healthFillImage.fillAmount = m_PlayerHealth.currentHealth / m_PlayerHealth.maxHealth;
	}
}
