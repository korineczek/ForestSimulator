using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    [Header("Zoom Settings")]
    public float maxZoom = 10f;
    public float minZoom = 3f;
    public float scrollSpeed;
    private float zoomS = 2.0f;

    [Header("Movement Settings"), Range(1f,10f)]
    public float movementSpeed = 2.0f;
    [Range(1f, 10f)]

    [Header("Rotation Settings")]
    Quaternion defaultRotation;
    public float leftLimit = -20f;
    public float rightLimit = 20f;
    public float downLimit = -45.0f;
    public float upLimit = -10f;
    public float sensX = 100.0f;
    public float sensY = 100.0f;

    float rotationY = 0.0f;
    float rotationX = 0.0f;
         
	void Start () {
        defaultRotation = transform.rotation;
	    rotationX = transform.rotation.eulerAngles.y;
	    rotationY = transform.rotation.eulerAngles.x;
	}
	
	// Update is called once per frame
	void Update () {
        float moveX = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        float rotate = Input.GetAxis("Rotate"); //XBOX360 axis setup
        Vector3 flatForward = Vector3.Scale(transform.forward, new Vector3(1, 0, 1)).normalized;

        transform.position += transform.right * moveX;
        transform.position += flatForward * moveZ;
        rotationX += rotate * sensX/2f * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, leftLimit, rightLimit);
        transform.localEulerAngles = new Vector3(rotationY, rotationX, 0);

        //Vector3 newposition = new Vector3(moveX, 0, moveZ);
        //transform.position += newposition;

        float scroll = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        //Xbox controller zoom
	    float zoom = Input.GetAxis("Zoom")*scrollSpeed;
	    Vector3 xboxZoom = transform.forward*zoom;
        Vector3 zoomVector = transform.forward * scroll;
	    transform.position += xboxZoom;

        Vector3 whereWeTryToGo = (transform.position + zoomVector);
        if (whereWeTryToGo.y > minZoom && whereWeTryToGo.y < maxZoom)
        {
            transform.position += zoomVector;
        }

        //if (transform.position.y + scroll * -zoomS > minZoom && transform.position.y + scroll * -zoomS < maxZoom) {
        //    transform.Translate(0, scroll * -zoomS, scroll * zoomS, Space.World);
        //}
        if (Input.GetMouseButton(2))
        {
            rotationX += Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
            rotationX = Mathf.Clamp(rotationX, leftLimit, rightLimit);
            rotationY += Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;
            rotationY = Mathf.Clamp(rotationY, downLimit, upLimit);
            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
    }
}
