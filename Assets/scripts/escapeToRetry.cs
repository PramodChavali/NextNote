using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartOnEscape : MonoBehaviour
{
	private void Awake()
	{

	}
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			RestartScene();
		}
	}

	private void RestartScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}