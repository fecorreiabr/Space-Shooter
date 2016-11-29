using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameInitializer : MonoBehaviour {

	public string startScene;

	public void StartGame () {
		if (startScene != null) {
			SceneManager.LoadScene (startScene);
		}
	}

	public void QuitGame () {
		Application.Quit ();
	}
}
