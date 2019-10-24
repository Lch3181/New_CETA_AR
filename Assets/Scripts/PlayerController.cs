using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Camera cam;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 RotationY = Vector3.zero;
    private Vector3 RotationX = Vector3.zero;
    public float moveSpeed;
    public float sensivity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (controller.isGrounded)
        {
            //facing direction debug
            Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * 10, Color.black);

            //Feed moveDirection with input.
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            //Feed Rotation with input
            RotationY = new Vector3(0, Input.GetAxisRaw("Mouse X"), 0);
            RotationX = new Vector3(Input.GetAxisRaw("Mouse Y"), 0, 0);
        }
        //Applying gravity to the controller
        moveDirection.y -= 20f * Time.deltaTime;
        //Apply Movement
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        transform.Rotate(RotationY * sensivity);
        cam.transform.Rotate(-RotationX * sensivity);

    }
}
