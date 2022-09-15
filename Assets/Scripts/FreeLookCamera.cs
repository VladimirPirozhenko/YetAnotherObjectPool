using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]  
public class FreeLookCamera : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float mouseSensitivity;

    private Camera cam;
    void Start()
    {
        cam = GetComponent<Camera>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
           speed *= speedMultiplier;    
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            speed /= speedMultiplier;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = transform.position + (-transform.right * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = transform.position + (transform.right * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = transform.position + (transform.forward * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = transform.position + (-transform.forward * speed * Time.deltaTime);
        }
        float newRotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;
        float newRotationY = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * mouseSensitivity;
        transform.localEulerAngles = new Vector3(newRotationY, newRotationX, 0f);
    }
}
