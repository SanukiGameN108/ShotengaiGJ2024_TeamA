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
    State state;

    public void StartMainGame()
    {
        ChangeState(State.MainGame);
    }

    public void ChangeState(State _state)
    {
        state = _state;

        if (state == State.MainGame)
        {
            var player_obj = GameObject.FindGameObjectWithTag("Player");
            if (player_obj)
            {
                var player = player_obj.GetComponent<Tamadii>();
                player?.Activate();
            }
                title_menu.SetActive(false);
        }
        else if (state == State.Result)
        {
            var player_obj = GameObject.FindGameObjectWithTag("Player");
            if (player_obj)
            {
                var player = player_obj.GetComponent<Tamadii>();
                player?.Deactivate();
            }

            var result =result_menu.GetComponent<ResulScript>();
            result?.StartResultEvent();
        }
    }
}
