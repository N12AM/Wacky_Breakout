using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallRespawn : MonoBehaviour
{
    [SerializeField] private GameObject prefabBall = null;
    private Camera _camera;
    private int _ballsAlreadySpawned = 0;
    private StatusUpdate _statusUpdate;
    private GameObject _ball;
    private float _perimeter;
    private float _rad;
    
    // Start is called before the first frame update
    void Start()
    {
           _statusUpdate = GameObject.FindGameObjectWithTag("hud").GetComponent<StatusUpdate>();
           _ball = GameObject.FindGameObjectWithTag("Ball");
           _perimeter = _ball.GetComponent<CircleCollider2D>().radius * 0.8f;
           _rad = _perimeter;


           //setting the vector position of where the ball will spawn


           //setting the vector position to the world location


    }

    public void Respawn(Vector3 location)
    {
           //created a new ball if ball respawn count is less ot equal to TotalNumberOfBalls in the ConfigurationUtils 
           if (_ballsAlreadySpawned < ConfigurationUtils.TotalNumberOfBalls)
           {
                  //call the method to increment the _ballAlreadySpawned count
                  TotalBallSpawned();
                //  _rad += _perimeter;
                //  location.x += _rad;
                  var newBall = Instantiate(prefabBall);
                  newBall.transform.position = location;
                  _statusUpdate.BallCount(_ballsAlreadySpawned);

           }

           //    _location = new Vector2((float)Screen.width/2 , (float)Screen.height / 3);
                                                      
     //   _location.z = -_camera.transform.position.z;
                                                      
    //    if(_camera != null)
                                                                                                             
     //       _location = _camera.ScreenToWorldPoint(_location);
        

 
    }

    public int TotalBallSpawned()
    {
           if(_ballsAlreadySpawned <= ConfigurationUtils.TotalNumberOfBalls)
              _ballsAlreadySpawned += 1;
           return _ballsAlreadySpawned;
           
    }

}
