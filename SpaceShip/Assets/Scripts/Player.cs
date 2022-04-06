using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;

    private float _fireRate = 0.1f;
    private float _nextFire = 0.0f;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update() {

        Move();
        if (Input.GetMouseButton(0) && Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;
            Shoot();
        }
       
    }

    private void Shoot()
    {
        foreach (Transform child in gameObject.transform) {
            Instantiate(_bullet, child.position, child.rotation);
        }
    }

    private void Move()
    { 
        Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentMousePosition = FixPositions(currentMousePosition);
        transform.position = currentMousePosition;       
    }

    private Vector3 FixPositions(Vector3 currentMousePosition)
    {
        currentMousePosition.z = 0;
        if (currentMousePosition.x < -8) currentMousePosition.x = -8;
        if (currentMousePosition.x > 8) currentMousePosition.x = 8;
        if (currentMousePosition.y < -4) currentMousePosition.y = -4;
        if (currentMousePosition.y > 4) currentMousePosition.y = 4;
        return currentMousePosition;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Destroy(collider.gameObject);
            GameManager._instance.TakeDamage();
        }
        if (collider.CompareTag("Coin"))
        {
            Destroy(collider.gameObject);
            GameManager._instance.AddCoin();
        }
    }
}
