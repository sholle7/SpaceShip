using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    static private GameObject _hitEffect;
    [SerializeField] private int _damage;
    private Rigidbody2D _rb;
    private float _velocity = 15f;

    void Awake()
    {
        if(_hitEffect == null)_hitEffect = (GameObject)Resources.Load("Prefabs/Effects/HitEffect");
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        if (CanDestroy()) Destroy(gameObject);
    }
    private void Move()
    {
        _rb.MovePosition(_rb.position + new Vector2(0, _velocity * Time.deltaTime));
    }
    private bool CanDestroy()
    {
        return transform.position.y > 5;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            GameObject hit = Instantiate(_hitEffect, _rb.transform.position, _rb.transform.rotation);
            Destroy(hit, 0.2f);

            collider.GetComponent<Obstacle>().TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
