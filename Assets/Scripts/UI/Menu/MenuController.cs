using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum MenuState
{
    MainMenu,
    Instructions
}

public class MenuController : MonoBehaviour
{
    private MenuState menuState;

    private GameObject mainMenuButtons;
    private GameObject instructions;

    private void Start() {
        menuState = MenuState.MainMenu;

        mainMenuButtons = transform.GetChild(2).gameObject;
        mainMenuButtons.SetActive(true);
        instructions = transform.GetChild(3).gameObject;
        instructions.SetActive(false);
    }

    private void Update() {
        if (Input.GetKeyDown("escape")) {
            switch (menuState)
            {
                case MenuState.MainMenu:
                    Quit();
                    break;

                case MenuState.Instructions:
                    menuState = MenuState.MainMenu;
                    break;
            }
        }

        switch (menuState)
        {
            case MenuState.MainMenu:
                mainMenuButtons.SetActive(true);
                instructions.SetActive(false);
                break;

            case MenuState.Instructions:
                mainMenuButtons.SetActive(false);
                instructions.SetActive(true);
                break;
        }
    }

    public void Instructions() {
        menuState = MenuState.Instructions;
    }

    public void Quit() {
        Application.Quit(0);
    }

    public void GameStart() {
        StartCoroutine(LoadGameAsyncScene());
    }

    private IEnumerator LoadGameAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
