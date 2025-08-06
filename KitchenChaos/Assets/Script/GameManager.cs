using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePause;
    public event EventHandler OnGameUnPause;
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }
    private State state;
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 1f;
    private float gamePlayingTimer = 0f;
    private float gamePlayingTimerMax = 300f;
    private bool isGamePaused = false;
    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }
    private void Start()
    {
        GameInput.Instance.OnPauseAction += Instance_OnPauseAction;
        state = State.CountdownToStart;
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void Instance_OnPauseAction(object sender, EventArgs e)
    {
        PauseGame();
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if(waitingToStartTimer<0f)
                {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this,EventArgs.Empty);
                }
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    gamePlayingTimer = gamePlayingTimerMax;
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                 break;
            case State.GameOver:
                break;
        }
    }
    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }
    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }
    public bool IsGameOver()
    {
        return state == State.GameOver;
    }
    public float CountdownToStartTimer()
    {
        return countdownToStartTimer;
    }
    public float GetGamePlayingTimerNormalized()
    {
        
        return 1-(gamePlayingTimer/gamePlayingTimerMax);
    }
    public void PauseGame()
    {
        isGamePaused = !isGamePaused;
        if(isGamePaused)
        {
            OnGamePause?.Invoke(this,EventArgs.Empty);
            Time.timeScale = 0f; 
        }
        else
        {
            OnGameUnPause?.Invoke(this, EventArgs.Empty);
            Time.timeScale = 1f;
        }
    }
}
