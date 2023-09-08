using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.Windows;

public class MouseInput : MonoBehaviour
{
    private PlayerInputActions inputs;

    // Start is called before the first frame update
    private void Start()
    {
        inputs = new PlayerInputActions();
        inputs.UI.Enable();
    }

    // Update is called once per frame
    private void Update()
    {
        var mousePosition = inputs.UI.Position.ReadValue<Vector2>();
    }
}
