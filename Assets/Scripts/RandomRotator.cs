using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {
	public float tumble;
	private Rigidbody2D rb;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		rb.angularVelocity = Random.value * tumble * 20;
	}

}
