using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    private const float _velocity = 3f;
    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        if (CanDestroy()) Destroy(gameObject);
    }
    public void Move()
    {
        _rb.MovePosition(_rb.position - new Vector2(0, _velocity * Time.deltaTime));
    }
    public bool CanDestroy()
    {
        return transform.position.y < -4;
    }
}
