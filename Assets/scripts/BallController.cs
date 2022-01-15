using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    [SerializeField] private GameObject prefabBall;
    [SerializeField] private Sprite red;
    [SerializeField] private Sprite green;
    [SerializeField] private Sprite blue;
    [SerializeField] private float _speedUpValueMultiplier = 1.6f;
    [SerializeField] private float _speedDownValueMultipler = 0.625f;

    private GameProperty _gameProperty;

    private Camera _camera;
    private Rigidbody2D _rigidbody2D;
    private float _defaultAngle = 90f;
    private Timer _timer;
    private Timer _timer2;
    private float _time;
    private bool _ballStartToMove;
    private Vector2 _angle;

    private BallRespawn _ballRespawn;

    private Vector3 _location;
    private bool _destroyed = false;

    private BallCountEvent _ballCountEvent;
    private Vector2 _force = Vector2.zero;

    private bool _speedUpMultiplied = false;
    private bool _speedDownMultiplied = false;
    

    private void Awake()
    {
           _ballCountEvent = new BallCountEvent();
    }

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _timer = gameObject.AddComponent<Timer>();
        _timer2 = gameObject.AddComponent<Timer>();
        _ballRespawn = _camera.GetComponent<BallRespawn>();
        _gameProperty = GameObject.FindGameObjectWithTag("property").GetComponent<GameProperty>();
        
        
        
        //_timer.Duration = ConfigurationUtils.BallLifetimeSeconds;

        _timer.Duration = 1f;
        _timer.Run();

        _timer2.Duration = ConfigurationUtils.BallLifetimeSeconds;
        _timer2.Run();

        var position = transform.position;
        
        // //setting the vector position of where the ball will spawn
         _location = new Vector2((float)Screen.width/2 , (float)Screen.height / 4);
         _location.z = -_camera.transform.position.z;
        //
        // //setting the vector position to the world location
         if(_camera != null)
             _location = _camera.ScreenToWorldPoint(_location);
        //    _location.x = _paddle.transform.position.x;
        // //setting the actual position to the new position
      //   transform.position = _location;

        //need to convert _defaultAngle from degrees to Radians
        _defaultAngle = (_defaultAngle * Mathf.PI) / 180;
        _angle = new Vector2(Mathf.Cos(_defaultAngle) , Mathf.Sin(_defaultAngle));
        
        
        //_rigidbody2D.AddForce(_angle * (ConfigurationUtils.BallImpulseForce * Time.deltaTime) , ForceMode2D.Impulse);
        
        //using the RigidBody property (velocity) to set the initial speed of the ball
        //this causes the ball to move at a constant speed 
        //ForceMode2D.Impulse gives a sudden thrust of force which we don't want here
       // _rigidbody2D.velocity = _angle * (ConfigurationUtils.BallImpulseForce * Time.deltaTime); 


    //   gameObject.GetComponent<SpriteRenderer>().sprite = GameProperty.Sprite;

    
             var spValue =  GameObject.FindGameObjectWithTag("property").GetComponent<GameProperty>().SpriteSwap;

             if (spValue == 0)
             {
                    gameObject.GetComponent<SpriteRenderer>().sprite = blue;
             }
             else if (spValue == 1)
             { 
                    gameObject.GetComponent<SpriteRenderer>().sprite = red;
             }
             else if (spValue == 2)
             {
                    gameObject.GetComponent<SpriteRenderer>().sprite = green;
             }
             else if (spValue == 3)
             {
                    gameObject.GetComponent<SpriteRenderer>().sprite = blue;
             }
             BallCountEventManager.AddInvoker(this);
       
       
         
    }

    public void ChangeColor(int value)
    {
           if (value == 0)
           {
                  gameObject.GetComponent<SpriteRenderer>().sprite = blue;
              
                  _speedDownMultiplied = true;
                  SpeedDownBall();
                  
           }
           else if (value == 1)
           {
                gameObject.GetComponent<SpriteRenderer>().sprite = red;
                
            
                _speedUpMultiplied = true;
                SpeedUpBall();
           }
           else if (value == 2)
           {
                  gameObject.GetComponent<SpriteRenderer>().sprite = green;
           }
           else if (value == 3)
           {
                  gameObject.GetComponent<SpriteRenderer>().sprite = blue;
           }
           
    }

    private void SpeedUpBall()
    {
           if (_ballStartToMove && _speedUpMultiplied)
           {
                  var direction = gameObject.GetComponent<Rigidbody2D>().velocity;
                  var rd2d = GetComponent<Rigidbody2D>();
                  rd2d.velocity = _speedUpValueMultiplier * direction;
                  _speedUpMultiplied = false;
           } 
    }

    private void SpeedDownBall()
    {
           if (_ballStartToMove && _speedDownMultiplied)
           {
                  var direction = gameObject.GetComponent<Rigidbody2D>().velocity;
                  var rd2d = GetComponent<Rigidbody2D>();
                  rd2d.velocity = _speedDownValueMultipler * direction;
                  _speedDownMultiplied = false;
           } 
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer.Finished && !_ballStartToMove)
        {
               _ballStartToMove = true;
               _force = _angle * (ConfigurationUtils.BallImpulseForce * Time.deltaTime);
              _rigidbody2D.AddForce(_force,ForceMode2D.Impulse);
              
              var direction = gameObject.GetComponent<Rigidbody2D>().velocity;
              var rd2d = GetComponent<Rigidbody2D>();
              rd2d.velocity = direction * _gameProperty.GetSpeedMultiplier;
        }
        
        //destroy the ball if the ball falls to the ground 
        //player fails to catch the ball with the paddle
        if (transform.position.y + 0.5f <= ScreenUtils.ScreenBottom ||  _timer2.Finished  )// && _ballStartToMove) )
        {
            //instantiate a new ball when the current ball gets destroyed
            if (!_destroyed)
            {    
                   _ballRespawn.Respawn(GetRandomLocation());
                   AudioManager.Play(AudioClipName.BallDestroyed);
                   _destroyed = true;
                   
                   //invokes a event when ever a ball gets destroyed , main purpose to display End Message
                   _ballCountEvent.Invoke(1);
                   Destroy(gameObject); 
            }
        }
    }

    private Vector2 GetRandomLocation()
    {
           var ran = Random.value;
           if (ran <= 0.5f)
           {
                  _location.x = Random.Range(ScreenUtils.ScreenLeft + 1f, ScreenUtils.ScreenRight / 2);
           }
           else
           {
                  _location.x = Random.Range(ScreenUtils.ScreenRight/2, ScreenUtils.ScreenRight - 1f);
           }

           return _location;
    }

    //this sets the direction of the ball after it hits the paddle
    public void SetDirection(Vector2 direction)
    {
     var rd2d = GetComponent<Rigidbody2D>();
     var speed = rd2d.velocity.magnitude;
     rd2d.velocity = direction * speed;
     AudioManager.Play(AudioClipName.Bounce);
    }
    
    public void BallCountEventListener(UnityAction<int> listener)
    {
           _ballCountEvent.AddListener(listener);
    }
}
