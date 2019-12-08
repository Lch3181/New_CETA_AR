using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Camera cam;
    private bool CursorLock;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 RotationY = Vector3.zero;
    private Vector3 RotationX = Vector3.zero;
    public Joystick joystickMovement;
    public Joystick joystickCamera;
    public float moveSpeed;
    public float sensivity;

    public bool canMove;

    void Start()
    {
        canMove = true;
        controller = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
    }

    //Determines if the player can interact with the trigger.
    public void toggleTriggerCollide()
    {
        if(this.CompareTag("Player"))
        {
            this.tag = "Untagged";
            Debug.Log(this.tag);
        }
        else
        {
            this.tag = "Player";
            Debug.Log(this.tag);
        }
    }

    public void toggleMove()
    {
        canMove = !canMove;
    }

    void FixedUpdate()
    {
        Movement();

        ToggleCursorLock();
    }

    void Movement()
    {
        if (canMove)
        {
            if (controller.isGrounded)
            {
                //Feed moveDirection with input.
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //keyboard
                moveDirection = new Vector3(joystickMovement.Horizontal, 0, joystickMovement.Vertical); //virtual joystick
                moveDirection = transform.TransformDirection(moveDirection);
                //Feed Rotation with input
                if (CursorLock) // keyboard
                {
                    RotationY = new Vector3(0, Input.GetAxisRaw("Mouse X"), 0);
                    RotationX = new Vector3(Input.GetAxisRaw("Mouse Y"), 0, 0);
                }
                //virtual joystick
                RotationY = new Vector3(0, joystickCamera.Horizontal, 0);
                RotationX = new Vector3(joystickCamera.Vertical, 0, 0);
            }
            //Applying gravity to the controller
            moveDirection.y -= 20f * Time.deltaTime;
            //Apply Movement
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
            //Apply Rotation
            transform.Rotate(RotationY * sensivity);
            cam.transform.Rotate(-RotationX * sensivity);

        }
    }

    void ToggleCursorLock()
    {
        //Right Click to toggle cursor lock
        if (canMove)
        {
            if (Input.GetMouseButtonDown(1))
            {
                CursorLock = !CursorLock;

                //apply
                if (CursorLock)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
        }
    }
}
