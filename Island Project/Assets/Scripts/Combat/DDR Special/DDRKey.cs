using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDRKey : MonoBehaviour
{
    public enum KeyTypes { DOWN = 0, UP, LEFT, RIGHT };

    KeyTypes _type;
    float _speed;

    private void Update()
    {
        transform.Translate(new Vector3(_speed, 0.0f));
    }

    public void setKey(KeyTypes type, float speed){
        _type = type;
        _speed = speed;
        setSprite(_type);
    }
    void detonate(){
        //stuff
    }

    void kill(){
        //stuff
    }
    void setSprite(KeyTypes type){
        //stuff
    }
}
