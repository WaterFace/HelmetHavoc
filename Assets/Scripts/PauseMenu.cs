using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    internal bool paused;

    [HideInInspector] public bool isAlive;

    public GameObject pauseMenu;

    void Start()
    {
        isAlive = false;
    }

    void Update()
    {
        if (!isAlive)
        {
            return;
        }

        if (Input.GetButtonUp("Cancel"))
        {
            paused = !paused;
        }

        pauseMenu.SetActive(paused);

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
