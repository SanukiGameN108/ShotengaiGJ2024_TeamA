using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MainGameManager : MonoSingleton<MainGameManager>
{
    public enum State
    {
        Title,
        MainGame,
        ResultWait,
        Result,
    }

    public GameObject title_menu;
    public GameObject result_menu;
    public State state;
    public float result_wait_time;
     float result_wait_timer;

    public void StartMainGame()
    {
        ChangeState(State.MainGame);
    }

    public void StartResult()
    {
        ChangeState(State.ResultWait);
    }

    void ChangeState(State _state)
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

        if (state == State.ResultWait)
        {
            var buildings = GameObject.FindGameObjectsWithTag("Building");
            bool moving = false;
            foreach (var building in buildings)
            {
                var buil_rb = building.GetComponent<Rigidbody>();
                if(buil_rb != null)
                {
                    var speed = buil_rb.velocity.sqrMagnitude;
                    if(0.1f <= speed)
                    {
                        moving = true;
                        break;
                    }
                }
            }

            result_wait_timer += Time.deltaTime;
            if(result_wait_time<= result_wait_timer || moving)
            {
                result_wait_timer = 0;
                ChangeState( State.Result);
            }
        }
    }
}
