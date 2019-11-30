using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class PlayerEnemyMove : MonoBehaviour {

	[Header("Movement")]
	public float speed = 12f;
	public float gravity = -9.81f;
	public float groundDistance = 0.4f;
	public LayerMask groundMask;
	public Transform groundCheck;

	private CharacterController m_CharacterController;
	private EnemyController m_EnemyController;
	private Vector3 velocity;
	private bool isGrounded;

	private void Start() {
		m_CharacterController = GetComponent<CharacterController>();
		DebugUtility.HandleErrorIfNullGetComponent<CharacterController, PlayerEnemyMove>(m_CharacterController, this, gameObject);

		m_EnemyController = GetComponent<EnemyController>();
		DebugUtility.HandleErrorIfNullGetComponent<EnemyController, PlayerEnemyMove>(m_EnemyController, this, gameObject);
	}

	private void Update() {
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

		if(isGrounded && velocity.y < 0) {
			velocity.y = -2f;
		}

		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		Vector3 move = transform.right * x + transform.forward * z;

		m_CharacterController.Move(move * speed * Time.deltaTime);

		velocity.y += gravity * Time.deltaTime;

		m_CharacterController.Move(velocity * Time.deltaTime);
	}
}
