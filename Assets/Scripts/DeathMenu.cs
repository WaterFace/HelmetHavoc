using UnityEngine;
using System.Collections;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathMenu;

    [HideInInspector] public bool isAlive;

    void Start()
    {
        isAlive = false;
    }
    
    void Update()
    {
        deathMenu.SetActive(!isAlive);
        if (!isAlive)
        {
            Time.timeScale = 0f;
        }
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
