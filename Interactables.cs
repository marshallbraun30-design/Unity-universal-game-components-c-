using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UIElements;
using static PlayerMovement;

public class CameraMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Vector3 StartPosition;
    Vector3 crouchposition;
    float timer = 0f;
    public PlayerMovement player;
    void Start()
    {
        StartPosition = transform.localPosition;
        crouchposition = transform.localPosition - new Vector3(0, 0.5f, 0);

    }

    // Update is called once per frame
    float sensetivity = 250;
    float ViewRotate = 0f;
    float Walkfreq = 6f;
    float runfreq = 10f;
    float walkAmp = 0.03f;
    float runAmp = 0.06f;
    float crouchfreq = 0.3f;
    float crouchAmp = 0.06f;


    void Update()
    {
        float mousey = Input.GetAxis("Mouse Y");
        ViewRotate -= mousey * sensetivity * Time.deltaTime;
        ViewRotate = Mathf.Clamp(ViewRotate, -90f, 90f);

        Vector3 Targetpos = StartPosition;

        if (player.Currentstate == Playerstate.Crouching)
        {
            Targetpos = crouchposition;
        }

        if (player.Currentstate == Playerstate.Running)
        {
            timer += Time.deltaTime;
            float bob = Mathf.Sin(timer * runfreq) * runAmp;
            transform.localPosition = Targetpos + Vector3.up * bob;
        }



        else if (player.Currentstate == Playerstate.Walking)
        {
            timer += Time.deltaTime;
            float bob = Mathf.Sin(timer * Walkfreq) * walkAmp;
            transform.localPosition = Targetpos + Vector3.up * bob;
        }

        else if (player.Currentstate == Playerstate.Crouching)
        {
            timer += Time.deltaTime;
            float bob = Mathf.Sin(timer * crouchfreq) * crouchAmp;
            transform.localPosition = Targetpos + Vector3.up * bob;
        }

        else
        {
            timer = 0f;
            transform.localPosition = Targetpos;
        }



        transform.localRotation = Quaternion.Euler(ViewRotate, 0, 0);

    }

}
