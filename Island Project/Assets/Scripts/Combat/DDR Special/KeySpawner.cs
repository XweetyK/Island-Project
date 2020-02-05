using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] _keyPrefab;
    [SerializeField] GameObject _EndZone;
    [SerializeField] GameObject _SpawnPoint;

    [SerializeField] float _keySpeed = 1.0f;
    [SerializeField] float _keyDelay = 2.0f;
    [SerializeField] float _initDelay = 1.0f;

    bool _active = false;

    private void Start()
    {
        startGame(_initDelay,_keyDelay,_keySpeed);
    }
    void startGame(float initDelay ,float keyDelay, float keySpeed)
    {
        _active = true;
        StartCoroutine(DDRUpdate(initDelay,keyDelay,keySpeed));
    }
    IEnumerator DDRUpdate(float initDelay = 1.0f, float keyDelay = 1.0f, float keySpeed = 1.0f){
        yield return new WaitForSeconds(initDelay);
        int rand;
        while (_active)
        {
            rand = Random.Range(0, _keyPrefab.Length - 1);
            GameObject go = Instantiate(_keyPrefab[rand], transform);
            DDRKey key = go.GetComponent<DDRKey>();
            key.speed = keySpeed;
            key.setTarget(_EndZone);
            yield return new WaitForSeconds(keyDelay);
        }
        yield return null;
    }

    public bool active
    {
        get { return _active; }
        set { _active = value; }
    }
}
