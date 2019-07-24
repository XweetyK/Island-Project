using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController: MonoBehaviour {
    [SerializeField] float _movSpeed;
    [SerializeField] float _rotSpeed;
    [SerializeField] Transform _cam;
    Vector3 MovementDirection;
    Rigidbody _rb;
    float _angle;

    private void Start() {
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update() {

        MovementDirection = (Input.GetAxis("Horizontal") * _cam.right + Input.GetAxis("Vertical") * _cam.forward) * _movSpeed;
        _rb.velocity = new Vector3(MovementDirection.x, _rb.velocity.y, MovementDirection.z);

        if (_rb.velocity != Vector3.zero) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(new Vector3(_rb.velocity.x, 0, _rb.velocity.z)), Time.deltaTime * _rotSpeed);
        }
    }
}
