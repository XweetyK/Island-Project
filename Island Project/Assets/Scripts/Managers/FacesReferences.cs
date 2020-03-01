using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Characters { HERO, CRIS, ORDER, ICON };
public class FacesReferences : MonoBehaviour {
    [SerializeField] private Sprite[] _crisFaces;
    [SerializeField] private Sprite[] _heroFaces;
    [SerializeField] private Sprite[] _orderFaces;
    [SerializeField] private Sprite[] _icons;

    public static FacesReferences Instance { get; private set; }
    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

        public Sprite GetFace(Characters chara, int faceIndex) {
        switch (chara) {
            case Characters.HERO:
                if (faceIndex < _heroFaces.Length) {
                    return _heroFaces[faceIndex];
                } else {
                    Debug.Log("index out of array range");
                    return null;
                }
            case Characters.CRIS:
                if (faceIndex < _crisFaces.Length) {
                    return _crisFaces[faceIndex];
                } else {
                    Debug.Log("index out of array range");
                    return null;
                }
            case Characters.ORDER:
                if (faceIndex < _orderFaces.Length) {
                    return _orderFaces[faceIndex];
                } else {
                    Debug.Log("index out of array range");
                    return null;
                }
            case Characters.ICON:
                if (faceIndex < _icons.Length) {
                    return _icons[faceIndex];
                } else {
                    Debug.Log("index out of array range");
                    return null;
                }
        }
        return null;
    }
}
