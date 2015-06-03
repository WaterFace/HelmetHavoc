using UnityEngine;
using System.Collections;

public class PlayerMenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject deathMenu;

    private PauseMenu _pauseMenu;
    private DeathMenu _deathMenu;
    private HealthManager health;

    // Use this for initialization
    void Start()
    {
        _pauseMenu = Instantiate<GameObject>(pauseMenu).GetComponent<PauseMenu>();
        _deathMenu = Instantiate<GameObject>(deathMenu).GetComponent<DeathMenu>();

        health = GetComponent<HealthManager>();

        _pauseMenu.isAlive = true;
        _deathMenu.isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        _pauseMenu.isAlive = health.isAlive;
        _deathMenu.isAlive = health.isAlive;
    }
}
