using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public float maxZoom = 10f;
    public float minZoom = 3f;

    public float speed = 2.0f;
    private float zoomS = 2.0f;
    // Use this for initialization

    //public float minimumx = 360.0f;
    //public float maxx = 360.0f;

   // public 


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
    }
}
