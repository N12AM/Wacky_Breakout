using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProperty : MonoBehaviour
{
    private static GameObject[] _colorChangingBalls = {null};
    private static GameObject[] _colorChangingBallsReduced = {null};
    
    [SerializeField] private float _speedUpValueMultiplier = 1.6f;
    private StatusUpdate _statusUpdate;
    private float _timerDuration = 15f;
    private float _timerAdded = 0f;
    private Timer _timer;
    private float _timerCount = 0;
    private bool _timerStart = false;
    private bool _timerActive = false;

    private int _spriteSwap = 0;
    private float _getSpeedMultiplier = 1f;

    private PaddleController _paddleScript;
    private float _paddleTotalFreezeTime = 5f;
    private float _paddleFreezerTimer = 0f;
    private bool _paddleTimerStart = false;
    private bool _paddleFreezeEffectActive = false;
    public float GetSpeedMultiplier
    {
        get => _getSpeedMultiplier;
        set => _getSpeedMultiplier = value;
    }

    public int SpriteSwap
    {
        get => _spriteSwap;
        set => _spriteSwap = value;
    }

    void Awake()
    {
        _spriteSwap = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        _timer = gameObject.AddComponent<Timer>();
        _statusUpdate = GameObject.FindGameObjectWithTag("hud").GetComponent<StatusUpdate>();
        _paddleScript = GameObject.FindWithTag("paddle").GetComponent<PaddleController>();
        
        SpeedUpEventManager.AddListener(SpeedUp);
        PaddleFreezeEventManager.AddListener(FreezerEffect);
    }

    private void Update()
    {
        if (_timerStart)
        {
            _timerCount -= Time.deltaTime;
        }

        if (_timerCount <= 0)
        {
            _timerStart = false;
            _timerCount = 0;
        }

        if (_timerCount == 0f && _timerActive)
        {
            SpriteSwap = 0;
            _colorChangingBallsReduced = GameObject.FindGameObjectsWithTag("Ball");
            foreach (var _gameObject in _colorChangingBallsReduced)
            {
                if(_gameObject != null)
                    _gameObject.GetComponent<BallController>().ChangeColor(0);
            }

            GetSpeedMultiplier = 1f;
            _timerActive = false;
        }

        if (_paddleTimerStart)
        {
            _paddleFreezerTimer -= Time.deltaTime;
        }
        
        if (_paddleFreezerTimer <= 0)
        {
            _paddleTimerStart = false;
            _paddleFreezerTimer = 0;
        if (_paddleFreezeEffectActive)
        {
            _paddleScript.IsPaddleMovable(true);
        }
        }
    }
    
    private void SpeedUp()
    {
      if (_timerCount == 0.0f)
      {
          _colorChangingBalls = GameObject.FindGameObjectsWithTag("Ball");
          foreach (var _gameObject in _colorChangingBalls)
          {
              _gameObject.GetComponent<BallController>().ChangeColor(1);
              SpriteSwap = 1;
          }
      }
      var remainingTime = 0f;
      if (_timerStart)
      {
          remainingTime =  _timerCount;
      }
      _timerAdded = _timerDuration + remainingTime;
      _timerStart = true;
      _timerCount = _timerAdded;
      _statusUpdate.TimerUpdate(_timerAdded);
      _timerActive = true;
      GetSpeedMultiplier = _speedUpValueMultiplier;
    }

    private void FreezerEffect()
    {
        
        _paddleScript.IsPaddleMovable(false);
        
        var remaining = 0f;
        if (_paddleTimerStart)
        {
            remaining = _paddleFreezerTimer;
        }

        var extraTimeAdded = _paddleTotalFreezeTime + remaining;
        _paddleFreezerTimer = extraTimeAdded;
        _statusUpdate.PaddleTimerUpdate(extraTimeAdded);
        _paddleFreezeEffectActive = true;
        _paddleTimerStart = true;

    }
}
