using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public float maxZoom = 10f;
    public float minZoom = 3f;

    public float speed = 2.0f;
    private float zoomS = 2.0f;
    // Use this for initialization

    public float minix = -20.0f;
    public float maxx = 60.0f;
    public float minY = -45.0f;
    public float maxY = 45.0f;
    public float sensX = 100.0f;
    public float sensY = 100.0f;

    float rotationY = 0.0f;
    float rotationX = 0.0f;
         
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        Vector3 newposition = new Vector3(moveX, 0, moveZ);
        transform.position += newposition;
      
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if(transform.position.y + scroll * -zoomS > minZoom && transform.position.y + scroll * -zoomS < maxZoom) {
            transform.Translate(0, scroll * -zoomS, scroll * zoomS, Space.World);
        }
        if (Input.GetMouseButton(2))
        {
            rotationX += Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
            rotationY += Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;
            rotationY = Mathf.Clamp(rotationY, minY, maxY);
            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
    }
}
