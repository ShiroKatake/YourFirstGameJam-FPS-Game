using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {
	public LayerMask switchableLayerMask;
	public GameObject selectedObject;
	public string hitObject;

	private int m_layerMask;

	private void Start() {
		m_layerMask = LayerMask.GetMask(switchableLayerMask.ToString());
	}

	private void Update() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		if(Physics.Raycast(ray, out hitInfo, switchableLayerMask)) {
			hitObject = hitInfo.collider.name;
			if (hitInfo.collider.gameObject.tag == "Switchable") {
				GameObject hitObject = hitInfo.transform.root.gameObject;
				SelectObject(hitObject);
			} else {
				ClearSelection();
			}
		} else {
			ClearSelection();
		}
	}

	void SelectObject(GameObject obj) {
		if(selectedObject != null) {
			if (obj == selectedObject)
				return;
			ClearSelection();
		}

		selectedObject = obj;
	}

	void ClearSelection() {
		selectedObject = null;
	}
}
