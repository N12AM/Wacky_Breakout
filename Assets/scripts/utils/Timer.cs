using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _totalSeconds = 0f;
    private float _elapsedSeconds = 0f;
    private bool _running = false;
    private bool _started = false;



    public void Run()
    {
        if (_totalSeconds > 0)
        {
            _running = true;
            _started = true;
            _elapsedSeconds = 0;
        }
    }

    #region property

    

    
    public float Duration
    {
        set
        {
            if (!_running)
            {
                _totalSeconds = value;
            }
        }
    }

    public bool Finished
    {
        get { return _started && !_running;  }
    }

    public bool Running
    {
        get { return _running; } 
    }

    #endregion
        
    

    // Update is called once per frame
    void Update()
    {
        if (_running)
        {
            _elapsedSeconds += Time.deltaTime;
            if (_elapsedSeconds >= _totalSeconds)
            {
                _running = false;
            }
        }
    }
}
