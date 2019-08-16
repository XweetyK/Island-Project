using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField] Transform _target;
    [SerializeField] float _speedX;
    [SerializeField] float _speedY;
    [SerializeField] float _clampYMin;
    [SerializeField] float _clampYMax;
    private Transform _childX;
    float _rotX;
    float _rotY;

    void Start() {
        _childX = transform.GetChild(0);
    }
    void Update() {
        CamMouseLook();
        gameObject.transform.position = _target.position;
    }

    void CamMouseLook() {
        if (Input.GetButton("Fire2")) {
            _rotX += _speedX * Input.GetAxis("Mouse Y") * Time.deltaTime;
            _rotY = _speedY * Input.GetAxis("Mouse X") * Time.deltaTime;
            _rotX = Mathf.Clamp(_rotX, _clampYMin, _clampYMax);

            _childX.transform.localRotation = Quaternion.Euler(-_rotX, 0, 0);
            gameObject.transform.Rotate(0.0f, _rotY, 0.0f);
        }
    }
}
