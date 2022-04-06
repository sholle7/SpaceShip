using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpot : MonoBehaviour
{
    [SerializeField]
    private Sprite _healthEnabled;
    [SerializeField]
    private Sprite _healthDisabled;

    private SpriteRenderer _sprite;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }
    public void SetEnabledLife()
    {
        _sprite.sprite = _healthEnabled;   
    }
    public void SetDisabledLife()
    {
        _sprite.sprite = _healthDisabled;
    }
}
