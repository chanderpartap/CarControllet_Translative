using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //Max speed of car
    public float maxSpeed = 200f;
    public float acceleration = 20f;
    public float deceleration = 15f;

    public float brake = 60f;

    public float turnDecelration = 30f;

    public float steerSpeed = 70f;

    Transform rightAxis, leftAxis;

    float currentSpeed = 0;
    //to convert Km/h -> m/s
    const float KMPH_TO_MPS = 0.278f;

    // Start is called before the first frame update
    void Start()
    {
        rightAxis = transform.Find("RotAxisR");
        leftAxis = transform.Find("RotAxisL");
    }

    // Update is called once per frame
    void Update()
    {
        //accelerate
        if (Input.GetKey(KeyCode.UpArrow))
        {
            AdjustSpeed(acceleration * KMPH_TO_MPS * Time.deltaTime);
        }
        else
        {
            AdjustSpeed(-deceleration * KMPH_TO_MPS * Time.deltaTime);
        }
        //brake
        if (Input.GetKey(KeyCode.DownArrow))
        {
            AdjustSpeed(-brake * KMPH_TO_MPS * Time.deltaTime);
        }
        //left steer
        if(Input.GetKey(KeyCode.LeftArrow) && currentSpeed > 5 * KMPH_TO_MPS)
        {
            AdjustSteering(-steerSpeed, leftAxis.position);
        }
        //right steer
        if(Input.GetKey(KeyCode.RightArrow) && currentSpeed > 5 * KMPH_TO_MPS)
        {
            AdjustSteering(steerSpeed, rightAxis.position);
        }

        //perform Movement
        transform.Translate(0, 0, currentSpeed * Time.deltaTime);
        Debug.Log(currentSpeed);
    }

    void AdjustSpeed(float newSpeed)
    {
        currentSpeed += newSpeed;
        if(currentSpeed > maxSpeed * KMPH_TO_MPS)
        {
            currentSpeed = maxSpeed * KMPH_TO_MPS;
        }

        if (currentSpeed < 0)
        {
            currentSpeed = 0;
        }
    }

    void AdjustSteering(float speed, Vector3 rotationAxis)
    {
        transform.RotateAround(rotationAxis, Vector3.up, speed * Time.deltaTime);

        if(currentSpeed > 30 * KMPH_TO_MPS)
        {
            AdjustSpeed(-turnDecelration + KMPH_TO_MPS * Time.deltaTime);
        }
    }
}
