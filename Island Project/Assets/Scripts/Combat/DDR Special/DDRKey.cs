﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDRKey : MonoBehaviour
{
    public enum KeyTypes { DOWN = 0, UP, LEFT, RIGHT };

    [SerializeField] KeyTypes _type;
    float _speed;
    GameObject _target;

    private void LateUpdate()
    {
        transform.Translate(new Vector2(_speed * Time.deltaTime, 0.0f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == _target)
        {
            detonate();
        }
    }

    public void setTarget(GameObject gameObject)
    {
        _target = gameObject;
    }

    public float speed{
        get { return _speed; }
        set { _speed = value; }
    }


    void detonate(){
        Destroy(gameObject);
    }

    void kill(){
        //stuff
    }
}
