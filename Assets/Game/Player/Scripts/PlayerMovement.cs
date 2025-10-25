using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private Joystick MovementJoystick;
    [SerializeField]
    private float PlayerSpeed;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (MovementJoystick.Direction.y != 0)
        {
            rb.velocity = new Vector3(MovementJoystick.Direction.x * PlayerSpeed, 0, MovementJoystick.Direction.y * PlayerSpeed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}