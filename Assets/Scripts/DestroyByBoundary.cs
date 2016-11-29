using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {
	
	void OnTriggerExit(Collider other) {
		// Destroy everything that leaves the trigger
		Destroy(other.gameObject);
	}

	//public bool characterInQuicksand;
	void OnTriggerExit2D(Collider2D other) {
		Destroy (other.gameObject);
		//characterInQuicksand = false;
	}
}
