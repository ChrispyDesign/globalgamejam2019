using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal_Movement : MonoBehaviour
{

    public Vector3 speed;
    public float variable;


    [Header("Eliptical")]
    public float alpha = 0f;

    public float tilt = 45f;

    
    public bool eliptical = false;
    private bool jumping = false;

    private Vector3 OR;

    private void Awake()
    {
        OR = transform.eulerAngles;

        if (eliptical)
            Invoke("Restart", 2f);
    }

    private void FixedUpdate()
    {
        if (jumping)
        {
            transform.localPosition = new Vector2(0f + (2f * MCos(alpha) * MCos(tilt)) - (1f * MSin(alpha) * MSin(tilt)),
                                               0f + (2f * MCos(alpha) * MSin(tilt)) + (1f * MSin(alpha) * MCos(tilt)));
            alpha += 4f;
        }

        transform.Rotate(speed.x * Time.fixedDeltaTime, speed.y * Time.fixedDeltaTime, speed.z * Time.fixedDeltaTime);

        if (alpha >= 300)
        {
            jumping = false;
            Invoke("Restart", 3f);
        }


    }


    float MCos(float value)
    {
        return Mathf.Cos(Mathf.Deg2Rad * value);
    }

    float MSin(float value)
    {
        return Mathf.Sin(Mathf.Deg2Rad * value);
    }

    void Restart()
    {
        alpha = 0.0f;
        transform.eulerAngles = OR;
        jumping = true;
    }
}

