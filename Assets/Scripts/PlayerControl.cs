using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, yMin, yMax;
}

public class PlayerControl : MonoBehaviour {
	public Rigidbody2D rb;
	public int score = 0;
	public float speed = 10;
	public Boundary boundary;

	public GameObject shot;
	public Transform[] shotSpawns;
	public float fireRate;

	public AudioClip[] lasers;				// Array of clips for when the player shoots.
	public float laserDelay = 1f;			// Delay for when the shoot should happen.

	public TouchPad touchPad;

	private float nextFire = 0.0f;
	private int laserIndex;
	private AudioSource audioSource;
	private Quaternion calibrationQuaternion;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		audioSource = GetComponent<AudioSource> ();
		CalibrateAccelerometer ();
	}

	void FixedUpdate() {
//		float moveHorizontal = Input.GetAxis ("Horizontal");
//		float moveVertical = Input.GetAxis ("Vertical");

//		Vector3 movement = new Vector3 (moveHorizontal, moveVertical);

//		Vector3 accelerationRaw = Input.acceleration;
//		Vector3 acceleration = FixAcceleration (accelerationRaw);
//		Vector3 movement = new Vector3 (acceleration.x, acceleration.y);

		Vector2 direction = touchPad.GetDirection ();
		Vector3 movement = new Vector3 (direction.x, direction.y);
		rb.velocity = movement * speed;

		rb.position = new Vector3 (
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 
			Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax)
		);
	}

	void Update () {
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			foreach (var shotSpawn in shotSpawns) {
				Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			} 
			Laser();
		}
		
	}

	void Laser(){
		//yield return new WaitForSeconds (laserDelay);

		if (!audioSource.isPlaying) {
			laserIndex = LaserRandom ();
			audioSource.clip = lasers [laserIndex];
			audioSource.Play();
		}
	}

	int LaserRandom()
	{
		int i = Random.Range(0, lasers.Length);

		if(i == laserIndex)
			return LaserRandom();
		else
			return i;
	}

	void CalibrateAccelerometer() {
		Vector3 accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
		calibrationQuaternion = Quaternion.Inverse (rotateQuaternion);
	}

	Vector3 FixAcceleration(Vector3 acceleration) {
		Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
		return fixedAcceleration;
	}

	/*
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.RightArrow)) {
			Vector3 newPosition = transform.position;
			newPosition.x = Mathf.Cl;
			rigi
			transform.position = newPosition;
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			Vector3 newPosition = transform.position;
			newPosition.x -= speed;
			transform.position = newPosition;
		}
	}
	*/
}
