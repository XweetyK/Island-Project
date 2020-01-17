using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField] Transform _target;
    [SerializeField] float _speedX;
    [SerializeField] float _speedY;
    [SerializeField] float _scale;
    private Transform _childX;
    private Transform _camera;
    float _rotX;
    float _rotY;
    float _limitAngle = 25;

    void Start() {
        _childX = transform.GetChild(0);
        _camera = transform.GetChild(0).GetChild(0);
    }
    void Update() {
        CamMouseLook();
        gameObject.transform.position = _target.position;
    }

    void CamMouseLook() {
        _limitAngle += Input.mouseScrollDelta.y * 3.3f;
        _limitAngle = Mathf.Clamp(_limitAngle, 5.0f, 25.0f);

        if (Input.GetButton("Fire2")) {
            _rotX += _speedX * Input.GetAxis("Mouse Y") * Time.deltaTime;
            _rotY = _speedY * Input.GetAxis("Mouse X") * Time.deltaTime;
            gameObject.transform.Rotate(0.0f, _rotY, 0.0f);
        }

        _rotX = Mathf.Clamp(_rotX, -30.0f, _limitAngle);
        _childX.transform.localRotation = Quaternion.Euler(-_rotX, 0, 0);
        float _dist = Vector3.Distance(this.transform.position, _camera.transform.position);
        Debug.Log(_dist);
        if ((_dist<25.0f && Input.mouseScrollDelta.y<0)||(_dist>7.0f && Input.mouseScrollDelta.y>0)) {
            _camera.transform.position += Input.mouseScrollDelta.y * _camera.forward;
        }
    }
}
