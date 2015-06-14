using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	public float speed;
	public float tilt;
	public Boundary boundary;
	public GameObject shot;
	public Transform shotSpawn;

	public float fireRate;
	private float nextFire = 0f;

	void FixedUpdate () {
		float moveHonrizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHonrizontal, 0f, moveVertical);
		Rigidbody body = GetComponent<Rigidbody> ();
		body.velocity = movement * speed;
		body.position = new Vector3 (
			Mathf.Clamp(body.position.x, boundary.xMin, boundary.xMax),
			0f,
			Mathf.Clamp(body.position.z, boundary.zMin, boundary.zMax)
		);
	
		body.rotation = Quaternion.Euler (0.0f, 0.0f, body.velocity.x * -tilt);
	}

	void Update () {
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		}
	}
}
