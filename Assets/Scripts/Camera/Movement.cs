using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    float Speed = 100f;
    [SerializeField]
    float VerticalSpeed = -10f;
    [SerializeField]
    float Sensitivity = .25f;
    bool m_rotate = false;

    // Update is called once per frame
    void Update()
    {
        //Horizontal
        if(Input.GetKey(KeyCode.A))
        {
            transform.position += (-transform.right * Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += (transform.right * Speed * Time.deltaTime);
        }
        //Forward
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += (transform.forward * Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += (-transform.forward * Speed * Time.deltaTime);
        }
        //Vertical
        if(Input.GetKey(KeyCode.Q))
        {
            transform.position += (transform.up * VerticalSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.position += (-transform.up * VerticalSpeed * Time.deltaTime);
        }
        //Rotation
        if (m_rotate)
        {
            float newRotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * Sensitivity;
            float newRotationY = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * Sensitivity;
            transform.localEulerAngles = new Vector3(newRotationY, newRotationX);
        }
        
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            m_rotate = true;
        }
        if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            m_rotate = false;
        }
    }
}
