using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController PlayerController;
    public float sensitivity = 200f;
    public float clampAngle = 50f;

    private float verticalRotation;
    private float horizontalRotation;


    private void Start()
    {
        this.verticalRotation = this.transform.localEulerAngles.x;
        this.horizontalRotation = this.transform.localEulerAngles.y;

    }

    private void Update()
    {
        Look();
        Debug.DrawRay(this.transform.position, this.transform.forward * 2, Color.blue);
        
    }

    private void Look()
    {
        float mouseVertical = -Input.GetAxis("Mouse Y");
        float mouseHorizontal = Input.GetAxis("Mouse X");

        this.verticalRotation += mouseVertical * this.sensitivity * Time.deltaTime;
        this.horizontalRotation += mouseHorizontal * this.sensitivity * Time.deltaTime;

        this.verticalRotation = Mathf.Clamp(verticalRotation, -clampAngle, clampAngle);

        this.transform.localRotation = Quaternion.Euler(this.verticalRotation, 0f, 0f);
        this.PlayerController.transform.rotation = Quaternion.Euler(0, this.horizontalRotation, 0f);

    }
}
