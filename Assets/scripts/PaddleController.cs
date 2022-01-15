using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{

    private Rigidbody2D _rigidbody;
    private float _movePaddle;
    private float _boxHalfWidth;
    private Camera _camera;
//    private bool _topSideCollided = false;
    
    float _halfColliderWidth;
    float _halfColliderHeight;

    private bool _isPaddleMovable = true;



    public void IsPaddleMovable(bool value)
    {
        _isPaddleMovable = value;
    } 

    private const float BounceAngleHalfRange = 150 * Mathf.Deg2Rad;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxHalfWidth = GetComponent<BoxCollider2D>().size.x / 2;
        
        
        BoxCollider2D bc2d = GetComponent<BoxCollider2D>();
        _halfColliderWidth = bc2d.size.x / 2;
        _halfColliderHeight = bc2d.size.y / 2;
        
        _camera = Camera.main;
        var position = transform.position;
        position.x = 0f;
        transform.position = position;
    }
    

    private void FixedUpdate()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            if (_isPaddleMovable)
            {
                Vector2 position = _rigidbody.position;
                position.x += horizontalInput * ConfigurationUtils.PaddleMoveUnitsPerSecond *
                              Time.deltaTime;
             position.x = CalculateClampedX(position.x);
                         _rigidbody.MovePosition(position);   
            }
            
        }


        

    }
    
    float CalculateClampedX(float x)
    {
        // clamp left and right edges
        if (x - _halfColliderWidth/4 < ScreenUtils.ScreenLeft)
        {
            x = ScreenUtils.ScreenLeft + _halfColliderWidth/4;
        }
        else if (x + _halfColliderWidth/4 > ScreenUtils.ScreenRight)
        {
            x = ScreenUtils.ScreenRight - _halfColliderWidth/4;
        }
        return x;
    }
    
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball") &&
            TopCollision(coll))
        {
            // calculate new ball direction
            float ballOffsetFromPaddleCenter = transform.position.x -
                                               coll.transform.position.x;
            float normalizedBallOffset = ballOffsetFromPaddleCenter /
                                         _halfColliderWidth;
            float angleOffset = normalizedBallOffset * BounceAngleHalfRange;
            float angle = Mathf.PI / 2 + angleOffset;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
      
            // tell ball to set direction to new direction
            BallController ballScript = coll.gameObject.GetComponent<BallController>();
            ballScript.SetDirection(direction);
        }
    }

    bool BallCollidedWithTopSide(Collision2D coll)
    {
        if (coll.transform.position.x > transform.position.x - _boxHalfWidth &&
            coll.transform.position.x < transform.position.x + _boxHalfWidth)
        {
            return true;
        }

        return false;
    }

     bool TopCollision(Collision2D coll)
     {
         const float tolerance = 0.05f;

//         // on top collisions, both contact points are at the same y location
         ContactPoint2D[] contacts = coll.contacts;
         try
         {
             return Mathf.Abs(contacts[0].point.y - contacts[1].point.y) < tolerance;
         }
         catch (Exception )
         {
           //  print(e);
             return false;
         }
        
     }
    


    







}

