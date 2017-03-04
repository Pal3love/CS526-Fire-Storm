using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public const float CAMERA_LEFT_BOUNDARY = -15f;
	public const float CAMERA_RIGHT_BOUNDARY = 16f;
	public const float CAMERA_BOTTOM_BOUNDARY = -13.5f;
	public const float CAMERA_TOP_BOUNDARY = 14f;

	public static float pixelsToUnits = 1f;
	public static float scale = 1f;
	public Vector2 nativeResolution = new Vector2 (16, 9);
	public GameObject player;
	private Vector3 offset;

	/* void Awake () {
		var camera = GetComponent<Camera> ();
		if (camera.orthographic) {
			scale = Screen.height / nativeResolution.y;
			pixelsToUnits *= scale;
			camera.orthographicSize = (Screen.height / 2.0f) / pixelsToUnits;
		}
	} */  // 这段代码可以让摄像机自适应不同屏幕分辨率

	// Use this for initialization
	void Start () {
		offset = Vector3.zero;
	}
	
	// Performs better than Update ()
	void LateUpdate () {
		float tempCameraPosZ = transform.position.z;
		Vector3 newCameraPos = offset + player.transform.position;
		newCameraPos.z = tempCameraPosZ;
		if (newCameraPos.x < CAMERA_LEFT_BOUNDARY) {
			newCameraPos.x = CAMERA_LEFT_BOUNDARY;
		}
		if (newCameraPos.x > CAMERA_RIGHT_BOUNDARY) {
			newCameraPos.x = CAMERA_RIGHT_BOUNDARY;
		}
		if (newCameraPos.y < CAMERA_BOTTOM_BOUNDARY) {
			newCameraPos.y = CAMERA_BOTTOM_BOUNDARY;
		}
		if (newCameraPos.y > CAMERA_TOP_BOUNDARY) {
			newCameraPos.y = CAMERA_TOP_BOUNDARY;
		}
		transform.position = newCameraPos;
	}
}
