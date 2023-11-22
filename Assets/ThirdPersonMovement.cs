using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float walkspeed;
    public float sprintspeed;
    private float truespeed;

    public float turnSmoothTime = 0.1f;

    float turnSmoothVelocity;


    private void Start()
    {
        truespeed = walkspeed;
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3 (horizontal, 0, vertical).normalized;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            truespeed = sprintspeed;
        }


        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            truespeed = walkspeed;
        }

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move (moveDirection.normalized * truespeed * Time.deltaTime);
        }
    }
}
