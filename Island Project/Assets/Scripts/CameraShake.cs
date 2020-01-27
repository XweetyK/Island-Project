using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    [SerializeField] Transform _camTransform;

    [SerializeField] float _shakeDuration = 1f;
    [SerializeField] float _shakeAmount = 0.3f;
    [SerializeField] float _decreaseFactor = 0.5f;

    Vector3 _originalPos;
    bool _activateShake;
    float _duration;
    float _amount;

    void Start() {
        _originalPos = _camTransform.localPosition;
        _activateShake = false;
        _duration = _shakeDuration;
        _amount = _shakeAmount;
    }

    void Update() {
        if (_activateShake) {
            if (_shakeDuration > 0) {
                _camTransform.localPosition = _originalPos + Random.insideUnitSphere * _shakeAmount;

                _shakeDuration -= Time.deltaTime * _decreaseFactor;
                //_shakeAmount -= Time.deltaTime * _decreaseFactor;
            } else {
                _camTransform.localPosition = _originalPos;
                _activateShake = false;
            }
        }
    }
    public void StartShake() {
        _shakeAmount = _amount;
        _shakeDuration = _duration;
        _activateShake = true;
    }
}