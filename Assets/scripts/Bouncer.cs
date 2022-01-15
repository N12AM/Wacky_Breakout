using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bouncer : MonoBehaviour
{

    private int _heatth = 100;
    private int _damage = 10;
    private SpriteRenderer _spriteRenderer;

    private int Damage => _damage;


    // Start is called before the first frame update
    void Start()
    {
        var randomForceY = Random.Range(10, 20);
        var randomForceX = Random.Range(2, 4);
        var rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(new Vector2(randomForceX, randomForceY) ,ForceMode2D.Impulse);


        _spriteRenderer = GetComponent<SpriteRenderer>();
        

    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        _heatth -= Damage;
        var spriteRendererColor = _spriteRenderer.color;
        
        if(_spriteRenderer.color.a > 0)
            spriteRendererColor.a -= 0.1f;
        
        _spriteRenderer.color = spriteRendererColor;
        
    }
}
