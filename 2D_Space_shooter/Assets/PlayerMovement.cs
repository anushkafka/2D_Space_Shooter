using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	float maxSpeed = 3.5f;
	float rotSpeed = 180f;
	float shipBoundaryRadius = 0.5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Rotate space ship
		Quaternion rot = transform.rotation;
		float z = rot.eulerAngles.z;
		z -= rotSpeed * Input.GetAxis ("Horizontal") * Time.deltaTime;
		rot = Quaternion.Euler (0, 0, z);
		transform.rotation = rot;

		// Move space ship
		Vector3 pos = transform.position;
		Vector3 posChange = new Vector3 (0,Input.GetAxis ("Vertical") * maxSpeed * Time.deltaTime,0);
//		pos.y = pos.y + Input.GetAxis ("Vertical");
		pos += rot * posChange;

		// Restrict space ship to camera boundaries
		if (pos.y + shipBoundaryRadius > Camera.main.orthographicSize) {
			pos.y = Camera.main.orthographicSize - shipBoundaryRadius;
		}
		if (pos.y - shipBoundaryRadius < -Camera.main.orthographicSize) {
			pos.y = Camera.main.orthographicSize + shipBoundaryRadius;
		}
		float screenRatio = (float)Screen.width / (float)Screen.height;
		float widthOrtho = Camera.main.orthographicSize * screenRatio;
		if (pos.x + shipBoundaryRadius > widthOrtho) {
			pos.x =  widthOrtho - shipBoundaryRadius;
		}
		if (pos.x + shipBoundaryRadius < -widthOrtho) {
			pos.x = -widthOrtho + shipBoundaryRadius;
		}
		transform.position = pos;
		
	}
}
