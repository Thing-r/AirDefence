using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public int maxSpeed;
    public int turnSpeed;
    public ShootingPoint[] blasters;

    private Transform tr;
    private Rigidbody rb;

    private float ntruePitch;
    private float ntrueYaw;
    private float trueYaw;
    private float truePitch;

    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        //   Cursor.lockState = CursorLockMode.Confined;
        //   Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        if (!Input.GetMouseButton (1)) 
        {
         truePitch = -Input.GetAxis("Mouse Y");
        trueYaw = Input.GetAxis("Mouse X");
       ntrueYaw = Mathf.Lerp(ntrueYaw, trueYaw, Time.deltaTime * 4);
       ntruePitch = Mathf.Lerp(ntruePitch, truePitch, Time.deltaTime * 4);
        }


        if (Input.GetMouseButton(0))    //  Left Click on the mouse to shoot 1
        {
            for (int i = 0; i < blasters.Length; i++)
            {
                ShootingPoint b = blasters[i];
                b.Shoot();
            }
        }
    }

    void FixedUpdate()
    {
        float accel = 0;
        float moveX = 0;
        float moveY = 0;
        float currentSpeed = GetComponent<Rigidbody>().velocity.magnitude;

        if (Input.GetKey(KeyCode.W)) accel = 5000;
        else if (Input.GetKey(KeyCode.S)) accel = -5000;

        if (Input.GetKey(KeyCode.E))
        {
            moveY = 2000;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveY = -2000;
        }

        Quaternion rot = Quaternion.Euler(tr.eulerAngles.x, tr.eulerAngles.y, 0);
        rb.AddForce(rot * Vector3.forward * accel);
        rb.AddForce(rot * Vector3.right * moveX);
        rb.AddForce(rot * Vector3.up * moveY);
        tr.Rotate(ntruePitch, ntrueYaw, 0);

        if (currentSpeed > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
