using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {
	public Rigidbody2D rb;
	public float speed;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		rb.velocity = transform.up * speed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
