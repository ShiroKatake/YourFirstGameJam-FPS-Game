using UnityEngine;
using UnityEngine.Events;

public class PlayerEnemyLook : MonoBehaviour {

	[Header("Looking")]
	public float mouseSensitivity = 100f;
	public Transform currentBody;

	float xRotation = 0f;
	SwitchPOV pov;

	private void Start() {
		Cursor.lockState = CursorLockMode.Locked;
		pov = GetComponent<SwitchPOV>();
	}

	private void Update() {
		currentBody = pov.currentBody.transform;
		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * -mouseSensitivity * Time.deltaTime;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -89f, 89f);

		transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
		currentBody.Rotate(Vector3.up * mouseX);
	}
}
