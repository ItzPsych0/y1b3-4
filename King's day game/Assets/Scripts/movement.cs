using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class movement : MonoBehaviour
{
    public float movespeed = 5f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MovementUpdate();
    }

    private void MovementUpdate()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var input = new Vector3();

        input += transform.forward * y;
        input += transform.right * x;
        input = Vector3.ClampMagnitude(input, 1f);

        transform.Translate(input * movespeed * Time.deltaTime, Space.World);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            movespeed = 8f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            movespeed = 5f;
        }
    }

}
