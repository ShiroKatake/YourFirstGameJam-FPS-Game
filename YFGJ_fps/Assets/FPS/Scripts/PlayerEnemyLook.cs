using UnityEngine;
using UnityEngine.Events;

public class PlayerEnemyLook : MonoBehaviour {

	[Header("Looking")]
	public float mouseSensitivity = 100f;
	public Transform playerBody;
	private float xRotation = 0f;

	private void Start() {
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void Update() {
		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * -mouseSensitivity * Time.deltaTime;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -89f, 89f);

		transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
		playerBody.Rotate(Vector3.up * mouseX);
	}
}
