using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallSpawn : MonoBehaviour
{
    [SerializeField] private GameObject prefabBall = null;
    private Timer _timer;
    private Camera _camera;
    private bool _spawnStarted;
    private bool _ballSpawned;
    private Vector2 _spawnLocationMin;
    private Vector2 _spawnLocationMax;
    private Vector3 _location = Vector3.zero;
    private Vector3 location = Vector3.zero; 
    

    private float _timeCheck;
    private float _ballHalfRadius;
    private float _minSpawnX;
    private float _maxSpawnX;
    private float _minSpawnY;
    private float _maxSpawnY;


    private int _totalNumberOfBalls;
    private int _ballsUsed = 0;

    private BallRespawn _ballRespawn;
    private StatusUpdate _statusUpdate;

    
    
    // Start is called before the first frame update
    void Start()
    {
        _timer = gameObject.AddComponent<Timer>();
        _camera = Camera.main;
        _totalNumberOfBalls = ConfigurationUtils.TotalNumberOfBalls;
        _statusUpdate = GameObject.FindGameObjectWithTag("hud").GetComponent<StatusUpdate>();

        // _timer.Duration = Random.Range(ConfigurationUtils.BallSpawnMinDelay, ConfigurationUtils.BallSpawnMaxDelay);
        // _timer.Run();

        _minSpawnX = (float)Screen.width / 3;
        _maxSpawnX = Screen.width - _minSpawnX;

        var newBall = Instantiate(prefabBall);
        
        //setting the vector position of where the ball will spawn
        location = new Vector2((float)Screen.width/2 , (float)Screen.height / 4);
        
        
        //setting the vector position to the world location
        if (_camera != null)
        {
            location = _camera.ScreenToWorldPoint(location);
            _ballRespawn = _camera.GetComponent<BallRespawn>();
            location.z = -_camera.transform.position.z;
        }
        newBall.transform.position = location;

        _ballHalfRadius = newBall.GetComponent<CircleCollider2D>().radius / 2;
        var position = newBall.transform.position;
        _spawnLocationMin = new Vector2(position.x - _ballHalfRadius , position.y - _ballHalfRadius);
        _spawnLocationMax = new Vector2(position.x + _ballHalfRadius , position.y + _ballHalfRadius);
        
        //setting the actual position to the new position
        // transform.position = location;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_spawnStarted)
        {
            _spawnStarted = true;
            _timer.Duration = Random.Range(ConfigurationUtils.BallSpawnMinDelay, ConfigurationUtils.BallSpawnMaxDelay);
            _timer.Run();
        }

        if (_timer.Finished)
        {
            
            var  randomSpawnTime = Random.Range(ConfigurationUtils.BallSpawnMinDelay, ConfigurationUtils.BallSpawnMaxDelay);
            _timer.Duration = randomSpawnTime;

            
            var ran = Random.value;
            if (ran <= 0.5f)
            {
                location.x = Random.Range(ScreenUtils.ScreenLeft + 1f, ScreenUtils.ScreenRight / 2);
            }
            else
            {
                location.x = Random.Range(ScreenUtils.ScreenRight/2 , ScreenUtils.ScreenRight - 1f);
            }
            _ballRespawn.Respawn(location);
           
            _timer.Run();

            _ballsUsed++;
        }
    //    print(GameObject.FindGameObjectsWithTag("Ball").Length);
     //   print("total gameObjects : "+_total);
    }
    
}
