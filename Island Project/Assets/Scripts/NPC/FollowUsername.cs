using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUsername : MonoBehaviour 
{ 
    void Update()
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("MainCamera").transform.position);
    }
}
