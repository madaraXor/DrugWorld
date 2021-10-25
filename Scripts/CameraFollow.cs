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
    public float smoothSpeed = 0.125f;
    // Gestionnaire Menu
    GestionnaireMenu gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GestionnaireMenu").GetComponent<GestionnaireMenu>();
        offsetCamera = transform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetMouseButton(0) && gm.menuOpen == false)
		{
			_horizontal = _horizontalSpeed * (Input.GetAxis("Mouse X") - Input.GetAxis("Mouse X") * 2) * Time.deltaTime;
			_vertical = _verticalSpeed * (Input.GetAxis("Mouse Y") - Input.GetAxis("Mouse Y") * 2)  * Time.deltaTime;
			transform.Translate(_horizontal, _vertical , 0);
		}
        else
        {
            Vector3 cameraPosition = target.position + offsetCamera;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, cameraPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
