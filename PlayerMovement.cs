using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.UIElements;
using static PlayerMovement;

public enum Playerstate
{
    Idle,
    Walking,
    Running,
    Jumping,
    Climbing,
    Crouching,
}

public class PlayerMovement : MonoBehaviour

{
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Capsule = GetComponent<CapsuleCollider>();
    }

    public Transform Feetcheck;

    CapsuleCollider Capsule;




    float walk = 5;
    float run = 14f;
    float crouch = 2.5f;
    float sensetivity = 250;
    public float Jumpforce = 8f;





    // Update is called once per frame

    public Playerstate Currentstate = Playerstate.Idle;
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float mousex = Input.GetAxis("Mouse X");
        float mousey = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up * mousex * sensetivity * Time.deltaTime);



        StateFunction(horizontal, vertical);

        MovementFunction(horizontal, vertical);

        Jumpfunction();




    }


    void StateFunction(float horizontal, float vertical)
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Currentstate = Playerstate.Crouching;
            Capsule.height = 1.0f;
        }


        else if (horizontal == 0 && vertical == 0)
        {
            Currentstate = Playerstate.Idle;
            Capsule.height = 2.0f;

        }
        else if ((horizontal != 0 || vertical != 0) && !Input.GetKey(KeyCode.LeftShift))
        {
            Currentstate = Playerstate.Walking;
            Capsule.height = 2.0f;

        }
        else if ((horizontal != 0 || vertical != 0) && Input.GetKey(KeyCode.LeftShift))
        {
            Currentstate = Playerstate.Running;
            Capsule.height = 2.0f;

        }



    }
    void MovementFunction(float horizontal, float vertical)
    {

        Vector3 Movement = (transform.forward * vertical + transform.right * horizontal);
        switch (Currentstate)
        {

            case Playerstate.Walking:
                rb.linearVelocity = new Vector3(Movement.x * walk, rb.linearVelocity.y, Movement.z * walk);
                break;
            case Playerstate.Running:
                rb.linearVelocity = new Vector3(Movement.x * run, rb.linearVelocity.y, Movement.z * run);
                break;

            case Playerstate.Crouching:
                rb.linearVelocity = new Vector3(Movement.x * crouch, rb.linearVelocity.y, Movement.z * crouch);
                break;
        }
    }

    void Jumpfunction()
    {

        if (Input.GetKeyDown(KeyCode.Space) && Grounded())
        {
            rb.AddForce(Vector3.up * Jumpforce, ForceMode.Impulse);
            return;

        }

        else
        {
            return;

        }
    }
    bool Grounded()

    {
        if (Physics.Raycast(Feetcheck.position, Vector3.down, 0.5f))
        {
            Debug.Log("Grounded: ");
            return true;


        }
        else { return false; }
    }

}