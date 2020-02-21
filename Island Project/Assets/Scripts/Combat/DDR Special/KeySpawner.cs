using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] _keyPrefab;
    [SerializeField] GameObject _EndZone;
    [SerializeField] GameObject _SpawnPoint;
    List<GameObject> _keys;

    bool _active = false;

    private void Start()
    {
        //startGame(1.0f,2.0f,1.0f);
    }
    public void startGame(float initDelay ,float keyDelay, float keySpeed)
    {
        stopGame();
        _active = true;
        StartCoroutine(DDRUpdate(initDelay,keyDelay,keySpeed));
    }
    public void stopGame(){
        _active = false;
        _keys.Clear();
    }

    IEnumerator DDRUpdate(float initDelay = 1.0f, float keyDelay = 1.0f, float keySpeed = 1.0f){
        yield return new WaitForSeconds(initDelay);
        int keycont = 0;
        int rand;
        while (_active)
        {
            rand = Random.Range(0, _keyPrefab.Length - 1);
            keycont++;
            GameObject go = Instantiate(_keyPrefab[rand], transform);
            _keys.Add(go);
            DDRKey key = go.GetComponent<DDRKey>();
            key.speed = keySpeed;
            key.setTarget(_EndZone);
            yield return new WaitForSeconds(keyDelay);
            if(CombatManager.Instance.keyCant == keycont && _keys.Count == 0){
                CombatManager.Instance.stopDDR();
            }
        }
        yield return null;
    }

    public void removeKey(GameObject go){
        foreach (GameObject key in _keys){
            if(key == go){
                _keys.Remove(key);
                Destroy(go);
            }
        }
    }
    public void inputAttack(DDRKey.KeyTypes type){
        DDRKey target = _keys[0].GetComponent<DDRKey>();
        if (target != null && target.KeyType == type)
        {
            CombatManager.Instance.killKey(target.gameObject);
            removeKey(target.gameObject);
        }
        else
        {
            CombatManager.Instance.missKey();
        }
    }

    public bool active
    {
        get { return _active; }
        set { _active = value; }
    }
}
