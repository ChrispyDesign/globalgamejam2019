using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal_Movement : MonoBehaviour
{

    public Vector3 speed;
    public Vector2 variable;


    [Header("Eliptical")]
    public Vector2 jumpingSpeed;
    public float alpha = 0f;
    public float JS;

    public float tilt = 45f;

    
    public bool eliptical = false;
    private bool jumping = false;

    private Vector3 OR;

    private void Awake()
    {
        OR = transform.eulerAngles;

        

        JS = Random.Range(jumpingSpeed.x, jumpingSpeed.y);

        if (eliptical)
            Invoke("Restart", JS);
        if (speed.x != 0)
        speed.x = speed.x + Random.Range(variable.x, variable.y);

        if (speed.y != 0)
            speed.y = speed.y + Random.Range(variable.x, variable.y);

        if (speed.z != 0)
            speed.z = speed.z + Random.Range(variable.x, variable.y);
    }

    private void FixedUpdate()
    {
        if (jumping)
        {
            transform.localPosition = new Vector2(0f + (2f * MCos(alpha) * MCos(tilt)) - (1f * MSin(alpha) * MSin(tilt)),
                                               0f + (2f * MCos(alpha) * MSin(tilt)) + (1f * MSin(alpha) * MCos(tilt)));
            alpha += 3f;
        }

        transform.Rotate(speed.x * Time.fixedDeltaTime, speed.y * Time.fixedDeltaTime, speed.z * Time.fixedDeltaTime);

        if (alpha >= 300)
        {
            jumping = false;
            Invoke("Restart", JS);
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
        JS = Random.Range(jumpingSpeed.x, jumpingSpeed.y);
    }
}

