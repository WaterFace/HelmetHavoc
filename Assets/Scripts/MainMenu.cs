using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public void Exit()
    {
        Application.Quit();
    }

    public void LoadLevel()
    {
        Application.LoadLevel("level1");
    }
}
