using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyShoot : MonoBehaviour {
	private EnemyController m_EnemyController;

	[Header("Shooting")]
	public Transform firepoint;
	public Transform fireDirection;

	void Start() {
		m_EnemyController = GetComponentInParent<EnemyController>();
	}

	void OnEnable() {
		firepoint = Camera.main.transform.Find("Firepoint");
		fireDirection = Camera.main.transform.Find("FireDirection");
	}

	void Update() {
		if (Input.GetButton("Fire")) {
			m_EnemyController.OrientTowards(fireDirection.position);
			m_EnemyController.TryAtack((fireDirection.position - firepoint.position).normalized);
		}
	}
}
