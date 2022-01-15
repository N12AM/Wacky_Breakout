using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StatusUpdate : MonoBehaviour
{
    [FormerlySerializedAs("_text")] [SerializeField] private Text text = null;
    [FormerlySerializedAs("_score")] [SerializeField] private Text score = null;
    [FormerlySerializedAs("_nextSpawn")] [SerializeField] private Text nextSpawn = null;
    [SerializeField] private Text timer = null;
    [SerializeField] private Text paddleTimer = null;
    
    private BallRespawn _ballRespawn;
    private const string RemainingBallsPrefix = "Balls Remaining : ";
    private const string ScorePrefix = "Score : ";
    private const string NextRespawnPrefix = "Next Spawn in : ";
    private const string SpeedDebugTextPrefix = "Debug SpeedUp Effect : ";
    private const string FreezeDebugTextPrefix = "Debug Freeze Effect : ";
    private const int TotalRemaining = 4;
    private int _startingScore = 0;
    private int _currentScore = 0;
    private int _spawnTimer = 0;
    private int _ballsRemaining = 0;
    private Timer _timer;
    private float _totalTime =  0;
    private float _currentTime = 0;
    private float ballPropertyTimer = 0;
    private float _paddlePropertyTimer = 0;
    private bool _startTimer;
    private bool ballPropertyTimerStarted = false;
    private bool _paddlePropertyTimerStarted = false;


    private bool StartTimer
    {
        get => _startTimer;
        set => _startTimer = value;
    }

    private float TotalTime
    {
        get => _totalTime;
        set => _totalTime = value;
    }

    public int CurrentScore => _currentScore;

    // Start is called before the first frame update
    void Start()
    {
        text.text = RemainingBallsPrefix + TotalRemaining.ToString();
        score.text = ScorePrefix + _startingScore.ToString();
        nextSpawn.text = NextRespawnPrefix + _spawnTimer.ToString();
        
        _ballRespawn = Camera.main.GetComponent<BallRespawn>();

        _timer = gameObject.AddComponent<Timer>();
        
        //adding this as a listener for the Event system
        EventManager.AddListener(ScoreCount);
        
    }


    private void Update()
    {
        if (!_timer.Finished && StartTimer)
        {
            if(_currentTime > 0)
                _currentTime -= Time.deltaTime;
        }
        
        if (_timer.Finished)
        {
            _currentTime = 0;
            StartTimer = false;
        }
        
        NextBallSpawnsInSeconds(_currentTime);

        if (ballPropertyTimerStarted)
        {
            ballPropertyTimer -= Time.deltaTime;
            timer.text = SpeedDebugTextPrefix + ballPropertyTimer.ToString();
        }

        if (ballPropertyTimer <= 0)
        {
            ballPropertyTimerStarted = false;
            ballPropertyTimer = 0;
            timer.text = SpeedDebugTextPrefix + ballPropertyTimer.ToString();

        }
        if (_paddlePropertyTimerStarted)
        {
            _paddlePropertyTimer -= Time.deltaTime;
            paddleTimer.text = FreezeDebugTextPrefix + _paddlePropertyTimer.ToString();
        }

        if (_paddlePropertyTimer <= 0)
        {
            _paddlePropertyTimerStarted = false;
            _paddlePropertyTimer = 0;
            paddleTimer.text = FreezeDebugTextPrefix + _paddlePropertyTimer.ToString();

        }
    }

    public void TimerUpdate(float value)
    {
        ballPropertyTimerStarted = true;
        ballPropertyTimer = value;
    }
    public void PaddleTimerUpdate(float value)
    {
        _paddlePropertyTimerStarted = true;
        _paddlePropertyTimer = value;
    }

    public void NextSpawnTime(float getTime)
    {
        TotalTime = (getTime);
        _currentTime = _totalTime;
        _timer.Duration = getTime;
        _timer.Run();

        StartTimer = true;
    }


    public void BallCount(int spawned )
    {
       // _ballsRemaining = ConfigurationUtils.TotalNumberOfBalls - _ballRespawn.TotalBallSpawned();
       _ballsRemaining = TotalRemaining - spawned;
       if (_ballsRemaining <= 0)
       {
           _ballsRemaining = 0;
       }
       if(text != null)
            text.text = RemainingBallsPrefix + _ballsRemaining.ToString();
    }

    //this act as a delegate for the score update event system
    public void ScoreCount(int score)
    {
        _currentScore += score;
        this.score.text = ScorePrefix + _currentScore.ToString();
    }

    private void NextBallSpawnsInSeconds(float ballSpawnTime)
    {
         var time = (int) ballSpawnTime;
        nextSpawn.text = NextRespawnPrefix + time.ToString();
    }
    
  
 
}
