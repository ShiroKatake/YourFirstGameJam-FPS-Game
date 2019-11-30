using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionIndicator : MonoBehaviour {
	MouseManager mm;

	public TextMeshProUGUI textUI;
	public Renderer[] rs;

	void Start() {
		mm = FindObjectOfType<MouseManager>();
	}

	void Update() {
		if (mm.selectedObject != null) {

			//This is the space occupied by the object's visual in world space
			rs = mm.selectedObject.GetComponentsInChildren<SkinnedMeshRenderer>();
			Bounds bigBounds = rs[0].bounds;
			foreach (var r in rs) {
				bigBounds.Encapsulate(r.bounds);
			}
			
			textUI.text = mm.selectedObject.GetComponent<Health>().objectName + " (E)";

			Vector3[] screenSpaceVertices = new Vector3[8];
			screenSpaceVertices[0] = Camera.main.WorldToScreenPoint(new Vector3(bigBounds.center.x + bigBounds.extents.x, bigBounds.center.y + bigBounds.extents.y, bigBounds.center.z + bigBounds.extents.z)); //v3BackTopRight
			screenSpaceVertices[1] = Camera.main.WorldToScreenPoint(new Vector3(bigBounds.center.x + bigBounds.extents.x, bigBounds.center.y + bigBounds.extents.y, bigBounds.center.z - bigBounds.extents.z)); //v3FrontTopRight
			screenSpaceVertices[2] = Camera.main.WorldToScreenPoint(new Vector3(bigBounds.center.x + bigBounds.extents.x, bigBounds.center.y - bigBounds.extents.y, bigBounds.center.z + bigBounds.extents.z)); //v3BackBottomRight
			screenSpaceVertices[3] = Camera.main.WorldToScreenPoint(new Vector3(bigBounds.center.x + bigBounds.extents.x, bigBounds.center.y - bigBounds.extents.y, bigBounds.center.z - bigBounds.extents.z)); //v3FrontBottomRight

			screenSpaceVertices[4] = Camera.main.WorldToScreenPoint(new Vector3(bigBounds.center.x - bigBounds.extents.x, bigBounds.center.y + bigBounds.extents.y, bigBounds.center.z + bigBounds.extents.z)); //v3BackTopLeft
			screenSpaceVertices[5] = Camera.main.WorldToScreenPoint(new Vector3(bigBounds.center.x - bigBounds.extents.x, bigBounds.center.y + bigBounds.extents.y, bigBounds.center.z - bigBounds.extents.z)); //v3FrontTopLeft
			screenSpaceVertices[6] = Camera.main.WorldToScreenPoint(new Vector3(bigBounds.center.x - bigBounds.extents.x, bigBounds.center.y - bigBounds.extents.y, bigBounds.center.z + bigBounds.extents.z)); //v3BackBottomLeft
			screenSpaceVertices[7] = Camera.main.WorldToScreenPoint(new Vector3(bigBounds.center.x - bigBounds.extents.x, bigBounds.center.y - bigBounds.extents.y, bigBounds.center.z - bigBounds.extents.z)); //v3FrontBottomLeft

			float min_x = screenSpaceVertices[0].x;
			float min_y = screenSpaceVertices[0].y;
			float max_x = screenSpaceVertices[0].x;
			float max_y = screenSpaceVertices[0].y;

			for (int i = 1; i < 8; i++) {
				if(screenSpaceVertices[i].x < min_x) {
					min_x = screenSpaceVertices[i].x;
				}
				if (screenSpaceVertices[i].y < min_y) {
					min_y = screenSpaceVertices[i].y;
				}
				if (screenSpaceVertices[i].x > max_x) {
					max_x = screenSpaceVertices[i].x;
				}
				if (screenSpaceVertices[i].y > max_y) {
					max_y = screenSpaceVertices[i].y;
				}
			}

			RectTransform rt = GetComponent<RectTransform>();
			rt.position = new Vector2(min_x, min_y);
			rt.sizeDelta = new Vector2(max_x - min_x, max_y - min_y);

			for (int i = 0; i < transform.childCount; i++) {
				transform.GetChild(i).gameObject.SetActive(true);
			}
			textUI.gameObject.SetActive(true);
		} else {
			for (int i = 0; i < transform.childCount; i++) {
				transform.GetChild(i).gameObject.SetActive(false);
			}
			textUI.gameObject.SetActive(false);
		}
	}
}
