using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    private int _health;
    private const float _velocity =5.5f;
    static private GameObject _explodeEffect;
    private Rigidbody2D _rb;
    void Awake()
    {
        _health = 5;
        if(_explodeEffect==null)_explodeEffect = (GameObject)Resources.Load("Prefabs/Effects/ExplodeEffect");
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        if (CanDestroy()) Destroy(gameObject);
    }
    private void Move()
    {
        _rb.MovePosition(_rb.position - new Vector2(0, _velocity * Time.deltaTime));
    }
    private bool CanDestroy()
    {
        return transform.position.y < -4;
    }
    public void TakeDamage()
    {
        _health--;
        if (_health == 0)
        {
            GameObject explode = Instantiate(_explodeEffect,_rb.transform.position,_rb.transform.rotation);
            Destroy(explode, 0.3f);

            Destroy(gameObject);
            GameManager._instance.AddScore();
        }
    }
}
