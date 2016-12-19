using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float cameraSpeed;
    public float zoomSpeed;

    private Vector3 mouseClickPos;
    private Quaternion rotateStart;
    Vector2 angleLimit;


    void Start () {
        cameraSpeed = 20f;
        zoomSpeed = 9f;
        mouseClickPos = Vector3.zero;
        rotateStart = Quaternion.identity;

        angleLimit = new Vector2(25f, 90f);
    }
	
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * cameraSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += -transform.forward * cameraSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += -transform.right * cameraSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * cameraSpeed * Time.deltaTime;
        }

        // Rotate
        if (Input.GetKey(KeyCode.Mouse2))
        {
            if (Input.GetKeyDown(KeyCode.Mouse2))
            {
                mouseClickPos = Input.mousePosition;
                rotateStart = transform.rotation;
            }

            float rotateSpeed = 1.0f;

            float xDiff = Input.mousePosition.x - mouseClickPos.x;
            float yDiff = Input.mousePosition.y - mouseClickPos.y;

            transform.Rotate(Vector3.up * xDiff * rotateSpeed, Space.World);

            //
            Debug.Log("EulerAngles: " + transform.eulerAngles);
            transform.Rotate(Vector3.left * yDiff * rotateSpeed, Space.Self);
            if (transform.eulerAngles.x < angleLimit.x)
            {
                transform.eulerAngles = new Vector3(angleLimit.x, transform.rotation.y, transform.rotation.z);
            }

            if (transform.eulerAngles.x > angleLimit.y)
            {
                transform.eulerAngles = new Vector3(angleLimit.y, transform.rotation.y, transform.rotation.z);
            }



            mouseClickPos = Input.mousePosition;
        }
        

        

        // Zoom
        float mouseWheelInput = Input.GetAxis("Mouse ScrollWheel");
        Zoom(mouseWheelInput);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "RegimentFlag")
            {
                Debug.Log("Hitting flag!");
            }
        }
    }

    public void Zoom(float amount)
    {
        transform.position -= Vector3.up * amount * zoomSpeed;
    }
}
