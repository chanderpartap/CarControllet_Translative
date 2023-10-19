using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    #region PUBLICS
    [Header("Car Control Variables (in Km/Hr)")]
    //Max speed of car
    public float maxSpeed = 200f;
    public float acceleration = 20f;
    public float deceleration = 15f;

    public float brake = 60f;

    public float turnDecelration = 15f;

    public float steerSpeed = 70f;
    #endregion

    #region PRIVATES
    Transform rightAxis, leftAxis;

    float currentSpeed = 0;
    //to convert Km/h -> m/s
    const float KMPH_TO_MPS = 0.278f;
    #endregion
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
            AdjustCarSpeed(acceleration * KMPH_TO_MPS * Time.deltaTime);
        }
        else
        {
            AdjustCarSpeed(-deceleration * KMPH_TO_MPS * Time.deltaTime);
        }
        //brake
        if (Input.GetKey(KeyCode.DownArrow))
        {
            AdjustCarSpeed(-brake * KMPH_TO_MPS * Time.deltaTime);
        }
        //left steer
        if(Input.GetKey(KeyCode.LeftArrow) && currentSpeed > 5 * KMPH_TO_MPS)
        {
            AdjustCarSteering(-steerSpeed, leftAxis.position);
        }
        //right steer
        if(Input.GetKey(KeyCode.RightArrow) && currentSpeed > 5 * KMPH_TO_MPS)
        {
            AdjustCarSteering(steerSpeed, rightAxis.position);
        }

        //perform Movement
        transform.Translate(0, 0, currentSpeed * Time.deltaTime);
        Debug.Log(currentSpeed);
    }
    void AdjustCarSpeed(float newSpeed)
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

    void AdjustCarSteering(float speed, Vector3 rotationAxis)
    {
        transform.RotateAround(rotationAxis, Vector3.up, speed * Time.deltaTime);

        if(currentSpeed > 30 * KMPH_TO_MPS)
        {
            AdjustCarSpeed(-turnDecelration + KMPH_TO_MPS * Time.deltaTime);
        }
    }
}
