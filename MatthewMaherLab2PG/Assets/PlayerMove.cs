using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private CharacterController CC;
    [SerializeField] private float moveSpeed;
    private float velocity_y;
    private float grav = -9.8f;
    float camRotation = 0f;
    public Transform CamTransform;
    public float MouseSensitivity;
    // Start is called before the first frame update
    void Start()
    {
        CC = gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseInputY = Input.GetAxis("Mouse Y") * MouseSensitivity;
        camRotation -= mouseInputY;
        camRotation = Mathf.Clamp(camRotation, -90f, 90f);
        CamTransform.localRotation = Quaternion.Euler(new Vector3(camRotation, 0f, 0f));

        float mouseInputX = Input.GetAxis("Mouse X") * MouseSensitivity;
        transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0f, mouseInputX, 0f));

        if (!CC.isGrounded)
        {
            velocity_y += grav * Time.deltaTime;
        }
        else
        {
            velocity_y = 0;
        }

        Vector3 movement = Vector3.down * -velocity_y;

        // these three lines didn't fix it even when they were removed
        float ForwardMovement = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        float SideMovement = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        movement += (transform.forward * ForwardMovement) + (transform.right * SideMovement);

        CC.Move(movement);
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10))
            {
                if (hit.collider.gameObject.GetComponent<Wood>())
                {
                    hit.collider.gameObject.GetComponent<Wood>().OnHit();
                }
                else if (hit.collider.gameObject.GetComponent<Rock>())
                {
                    hit.collider.gameObject.GetComponent<Rock>().OnHit();
                }
            }
        }
    }
}
