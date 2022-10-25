using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Move : MonoBehaviour
{
    public float speed = 10;
    public float rotationSpeed = 90;
    public float gravity = -20f;
    public float jumpSpeed = 15;
    bool jump = false;
    float hori, verti;

    Vector3 moveDirection = Vector3.zero;

    CharacterController characterController;
    Vector3 moveVelocity;
    Vector3 turnVelocity;
    GameObject red;
    GameObject blue;
    GameObject black;
    GameObject green;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        red = GameObject.Find("red");
        blue = GameObject.Find("blue");
        black = GameObject.Find("black");
        green = GameObject.Find("green");
    }

    void Update()
    {
        hori = Input.GetAxis("Horizontal");
        verti = Input.GetAxis("Vertical");

        moveDirection = new Vector3(hori, 0, verti);
        
        jump = !jump && Input.GetKeyDown(KeyCode.Space);
    }

    private void FixedUpdate()
    {
        if (characterController.isGrounded)
        {
            moveVelocity = transform.forward * speed * verti;
            turnVelocity = transform.up * rotationSpeed * hori;

            if (Input.GetButtonDown("Jump"))
            {
                moveVelocity.y = jumpSpeed;
            }
        }
        moveVelocity.y += gravity * Time.deltaTime;
        characterController.Move(moveVelocity * Time.deltaTime);
        transform.Rotate(turnVelocity * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "redBox")
        {
            speed = 1;
            moveVelocity = transform.forward * speed * verti;
            red.SetActive(false);
        }
        else if (other.gameObject.tag == "greenBox")
        {
            speed = 20;
            moveVelocity = transform.forward * speed * verti;
            green.SetActive(false);
        }
        else if (other.gameObject.tag == "blueBox")
        {
            jumpSpeed = 30;
            moveVelocity.y = jumpSpeed;
            blue.SetActive(false);
        }
        else if (other.gameObject.tag == "blackBox")
        {
            jumpSpeed = 0;
            moveVelocity.y = jumpSpeed;
            black.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.tag == "blueBox")
        //{
        //    Debug.Log("It's ALIVE and blue");
        //    other.gameObject.GetComponent<Renderer>().material.color = Color.blue;
        //}
    }
}

/*
 * Task Description:
Create a scene with object that moves with charachter controller vertically and horizontally on a plane object, and also jumps. +
Place different spherical objects with green, red, blue and black colors in your scene.
Whenever the charachter controller touches the object, the ball should disapear and:
-If the ball color is red, the speed of charachter should decrease.
-If the ball is green the speed should increase.
-If the ball is blue, the charachter jump height should increase.
-If the ball is black, the charachter should not be able to jump.
Bonus tasks:
Add also yellow, white balls
-If the ball is yellow the controls should be reversed (you press left, player goes to right)
-If the ball is white apply random force to white ball without making it(the ball itself) trigger(is trigger option in the ball collider)
-Change the color of the charachter controller capsule to the color of the ball it collided with.
 */