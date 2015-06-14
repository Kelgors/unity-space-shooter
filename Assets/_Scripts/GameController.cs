using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public Vector3 spawnValues;
	public float hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	private int score;

	private bool restart;
	private bool gameOver;

	void Start () {
		restart = false;
		gameOver = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}

	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int i = 0; i < hazardCount; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			if (gameOver) {
				restartText.text = "Press 'R' to restart";
				restart = true;
				break;
			}
			yield return new WaitForSeconds (waveWait);
		}
	}

	void Update () {
		if (restart && Input.GetKeyDown(KeyCode.R)) {
			Application.LoadLevel (Application.loadedLevel);
		}
	}

	public void SetGameOverState() {
		gameOver = true;
		gameOverText.text = "GAME OVER";
	}

	public void AddScore(int value) {
		score += value;
		UpdateScore ();
	}

	void UpdateScore () {
		scoreText.text = "" + score;
	}
}
