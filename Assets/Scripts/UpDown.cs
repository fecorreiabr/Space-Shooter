using UnityEngine;
using System.Collections;

public class UpDown : MonoBehaviour {

	public float speed = 0.05f;

	// Apos criacao dos objetos em memoria
	void Awake() {

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		pos.y -= speed;

		transform.position = pos;
	}
}
