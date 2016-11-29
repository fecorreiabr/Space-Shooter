using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public int maxSpawnWaves;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public Text scoreText;
	public GameObject levelText;
	public GameObject gameOverPanel;
	public GameObject completePanel;
	public string nextScene;

	private int spawnWavesCount;
	private bool gameOver;
	private GameObject player;
	private PlayerControl pc;

	void Start () {
		resetPlayer ();
		gameOver = false;
		gameOverPanel.SetActive (false);
		pc = player.GetComponent<PlayerControl>();
		//score = player.GetComponent(PlayerControl);
		spawnWavesCount = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves());
	}

	IEnumerator SpawnWaves() {
		yield return new WaitForSeconds (startWait);
		levelText.SetActive (false);
		while (true) {
			for (int i = 0; i < hazardCount; i++) {
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			spawnWavesCount += 1;

			yield return new WaitForSeconds (waveWait);

			if (spawnWavesCount >= maxSpawnWaves ) {
				completePanel.SetActive (true);
				break;
			}

			if (gameOver) {
				gameOverPanel.SetActive (true);
				break;
			}
		}
	}

	public void AddScore (int newScoreValue) {
		pc.score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore () {
		scoreText.text = "Score: " + pc.score;
	}

	public void GameOver() {
		gameOver = true;
	}

	public void RestartGame () {
		DestroyObject (player);
		SceneManager.LoadScene ("level1");
	}

	public void QuitGame () {
		Application.Quit ();
	}

	public void LoadNextLevel () {
		if (nextScene != null) {
			SceneManager.LoadScene (nextScene);
		}
	}

	private void resetPlayer() {
		player = GameObject.FindWithTag ("Player");
		DontDestroyOnLoad (player);
		player.transform.position = new Vector3 (0, -9.5f, 0);
	}
}
