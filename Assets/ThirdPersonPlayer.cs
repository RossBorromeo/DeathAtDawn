using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonPlayer : MonoBehaviour
{

    //Essentials
    
    public CharacterController controller;
    public Transform cam;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;


    //Movement
    public float walkspeed;
    public float sprintspeed;
    float truespeed;
    Vector2 movement;

    //Jumping
    public float jumpHeight;
    public float gravity;
    bool isGrounded;
    Vector3 velocity;

    private void Start()
    {
        truespeed = walkspeed;
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller.height = 2f;
        controller.center = new Vector3(0f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(transform.position + Vector3.up * 0.1f,.1f,1);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1;

        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            truespeed = sprintspeed;
        }


        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            truespeed = walkspeed;
        }
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Vector3 direction = new Vector3(movement.x, 0, movement.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * truespeed * Time.deltaTime);
        }

        //Jumping            
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("Jump Pressed");
            velocity.y = Mathf.Sqrt((jumpHeight * 10) * -2f * gravity);
        }

        if (velocity.y > -20)
        {
            velocity.y += (gravity * 10) * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);
    }
}
