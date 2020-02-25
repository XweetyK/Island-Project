using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIelements : MonoBehaviour
{
    [SerializeField] List<GameObject> elements;
    
    public GameObject getElement(int number)
    {
        return elements[number];
    }
}
