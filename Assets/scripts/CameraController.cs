using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

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
	} */  // 这段代码可以让摄像机缩放自适应不同屏幕分辨率

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = offset + player.transform.position;
	}
}
