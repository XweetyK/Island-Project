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

    //DDR game
    [Header("DDR Config")]

    [SerializeField] float _defaultKeySpeed = 200.0f;
    [SerializeField] float _defaultKeyDelay = 2.0f;
    [SerializeField] float _initDelay = 1.0f;
    [SerializeField] int _keyCant = 10;
    float _keySpeed = 1.0f;
    float _keyDelay = 2.0f;

    bool _active = false;

    public static KeySpawner Instance { get; private set; }
    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        _keys = new List<GameObject>();
    }
    public void StartGame()
    {
        Debug.Log("DDRSTART   " + gameObject.name);
        OnStopGame();
        _active = true;
        _keySpeed = _defaultKeySpeed;
        _keyDelay = _defaultKeyDelay;
        StartCoroutine(DDRUpdate());
    }
    public void OnStopGame(){
        _active = false;
        _keys.Clear();
    }

    IEnumerator DDRUpdate(){
        yield return new WaitForSeconds(_initDelay);
        int keycont = 0;
        int rand;
        while (_active)
        {
            rand = Random.Range(0, _keyPrefab.Length - 1);
            keycont++;
            GameObject go = Instantiate(_keyPrefab[rand], _SpawnPoint.transform);
            DDRKey key = go.GetComponent<DDRKey>();
            key.speed = _keySpeed;
            key.setTarget(_EndZone);
            _keys.Add(go);
            yield return new WaitForSeconds(_keyDelay);
            if(_keyCant == keycont && _keys.Count == 0){
                CombatInput.Instance.StopDDR();
            }
        }
        yield return null;
    }

    public void RemoveKey(GameObject go){
        foreach (GameObject key in _keys){
            if(key == go){
                _keys.Remove(key);
                Destroy(go);
            }
        }
    }
    public void InputAttack(DDRKey.KeyTypes type){
        if(_keys.Count == 0) {
            return;
        }
        DDRKey target = _keys[0].GetComponent<DDRKey>();
        if (target != null && target.KeyType == type)
        {
            CombatInput.Instance.GetDDRInput(CombatInput.DDRInput.KILL);
            RemoveKey(target.gameObject);
        }
        else
        {
            CombatInput.Instance.GetDDRInput(CombatInput.DDRInput.MISS);
        }
    }

    public bool Active
    {
        get { return _active; }
        set { _active = value; }
    }

    public float KeySpeed
    {
        get { return _keySpeed; }
        set { _keySpeed = value; }
    }
}
