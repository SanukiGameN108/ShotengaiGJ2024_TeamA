using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainGameManager : MonoSingleton<MainGameManager>
{
    public enum State
    {
        Title,
        MainGame,
        Result,
    }

    public GameObject title_menu;
    public GameObject result_menu;
    public State state;

    public void StartMainGame()
    {
        ChangeState(State.MainGame);
    }

    public void ChangeState(State _state)
    {
        state = _state;

        if (state == State.MainGame)
        {
                title_menu.SetActive(false);
        }
        else if (state == State.Result)
        {
            var result =result_menu.GetComponent<ResulScript>();
            result?.StartResultEvent();
        }
    }

    public bool IsMainGame()
    {
        return state == State.MainGame;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
