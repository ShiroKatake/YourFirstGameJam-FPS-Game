using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class PlayerEnemyShoot : MonoBehaviour {
	private EnemyController m_EnemyController;

	[Header("Shooting")]
	public Transform firepoint;
	public Transform fireDirection;

	void Start() {
		m_EnemyController = GetComponent<EnemyController>();
		DebugUtility.HandleErrorIfNullGetComponent<EnemyController, PlayerEnemyMove>(m_EnemyController, this, gameObject);
	}

	void Update() {
		if (Input.GetButton("Fire")) {
			m_EnemyController.OrientTowards(fireDirection.position);
			m_EnemyController.TryAtack((fireDirection.position - firepoint.position).normalized);
		}
	}
}
