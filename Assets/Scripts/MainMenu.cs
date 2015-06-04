using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Button level1;
    public Button level2;
    public Button level3;
    public Button level4;
    public Button level5;
    public Button level6;

    void Start()
    {
        SaveLoad.Load();
        int progress = SaveLoad.progress.progress;

        level1.interactable = progress >= 0;
        level2.interactable = progress >= 1;
        level3.interactable = progress >= 2;
        level4.interactable = progress >= 3;
        level5.interactable = progress >= 4;
        level6.interactable = progress >= 5;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void LoadLevel(int level)
    {
        Application.LoadLevel("level" + level.ToString());
    }

    public void Menu()
    {
        Application.LoadLevel("MainMenu");
    }

    public void ResetProgress()
    {
        SaveLoad.progress.progress = 0;
        SaveLoad.Save();

        Menu();
    }
}
