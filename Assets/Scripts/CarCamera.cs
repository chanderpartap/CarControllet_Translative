using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCamera : MonoBehaviour
{
    [Header("Camera Controls")]
    public Transform car;
    public float height = 4;
    public float zDistance = 10f;
    public float turnTimeout = 0.25f;
    public float camRotation = 50;
    //flag
    float angleChangeTime = -1;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = car.position;
        transform.rotation = car.rotation;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = car.position;

        transform.Translate(0, height, -zDistance);

        float angle = Vector3.Angle(car.forward, transform.forward);
        if (angle > 1)
        {
            if (angleChangeTime == -1)
            {
                angleChangeTime = 0;
            }

            angleChangeTime += Time.deltaTime;
            if (angleChangeTime > turnTimeout)
            {
                float resultDirection = Vector3.Cross(car.forward, transform.forward).y;
                float rotationDirection;
                if (resultDirection > 0)
                {
                    rotationDirection = -1;
                }
                else
                {
                    rotationDirection = 1;
                }
                transform.RotateAround(car.position, Vector3.up, rotationDirection * camRotation * Time.deltaTime);
            }
        }
        else
        {
            angleChangeTime = -1;
        }

    }
}
