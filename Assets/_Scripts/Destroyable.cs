using UnityEngine;
using System.Collections;

public class Destroyable : MonoBehaviour {

	private GameController gameController;

	public GameObject explosion;
	public GameObject playerExplosion;
	public GameObject enemyExplosion;
	public int scoreValue;

	void Start() {
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = (GameController) gameControllerObject.GetComponent<GameController> ();
		}
		if (gameControllerObject == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Boundary") return;
		Destroy (other.gameObject);
		Destroy (gameObject);
		Instantiate (explosion, transform.position, transform.rotation);
		if (other.tag == "Player") {
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.SetGameOverState ();
		} else if (other.tag == "PlayerBolt") {
			gameController.AddScore (scoreValue);
		}
	}

}
