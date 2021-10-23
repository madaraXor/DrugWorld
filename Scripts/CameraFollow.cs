using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] Vector3 offsetCamera;
    [SerializeField] float _horizontalSpeed;
    [SerializeField] float _verticalSpeed;
    public float _horizontal;
    public float _vertical;
    // Start is called before the first frame update
    void Start()
    {
        offsetCamera = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
		{
            
			_horizontal = _horizontalSpeed * (Input.GetAxis("Mouse X") - Input.GetAxis("Mouse X") * 2) * Time.deltaTime;
			_vertical = _verticalSpeed * (Input.GetAxis("Mouse Y") - Input.GetAxis("Mouse Y") * 2)  * Time.deltaTime;
			transform.Translate(_horizontal, _vertical , 0);
		}
        else
        {
            Vector3 cameraPosition = target.position + offsetCamera;
            transform.position = cameraPosition;
        }
    }
}
