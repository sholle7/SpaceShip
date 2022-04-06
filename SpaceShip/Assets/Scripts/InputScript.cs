using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputScript : MonoBehaviour
{
    TMP_InputField _input;
    private void Awake()
    {
        _input = GetComponent<TMP_InputField>();
    }
    public void Submit()
    {
        GameManager._instance.SaveHighScore(_input.text);
    }
}
