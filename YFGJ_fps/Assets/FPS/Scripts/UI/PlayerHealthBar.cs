using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour {
	[Tooltip("Image component dispplaying current health")]
	public Image healthFillImage;

	Health m_PlayerHealth;
	public GameObject currentCharacterController;

	private void Start() {
		if (FindObjectOfType<PlayerCharacterController>() == null) {
			currentCharacterController = FindObjectOfType<PlayerEnemyMove>().gameObject;
		} else {
			currentCharacterController = FindObjectOfType<PlayerCharacterController>().gameObject;
		}
		m_PlayerHealth = currentCharacterController.GetComponent<Health>();
		DebugUtility.HandleErrorIfNullGetComponent<Health, PlayerHealthBar>(m_PlayerHealth, this, currentCharacterController.gameObject);
	}

	void Update() {
		// update health bar value
		healthFillImage.fillAmount = m_PlayerHealth.currentHealth / m_PlayerHealth.maxHealth;
	}
}
