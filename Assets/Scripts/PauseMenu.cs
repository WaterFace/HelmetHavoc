using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    internal bool paused;

    public GameObject menu;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetButtonUp("Cancel"))
        {
            paused = !paused;
        }

        menu.SetActive(paused);

        if (paused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
	}

    public void Resume()
    {
        paused = false;
    }

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void MainMenu()
    {
        Application.LoadLevel("MainMenu");
    }
}
