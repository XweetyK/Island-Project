using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] int[] _spawnArea;
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] [Range(0, 20)] int _maxEnemyCant;
    float _time = 0;

    private void Update() {
        _time += Time.deltaTime;
        if (_time > 10) {
            if (GameManager.Instance.EnemiesInMap() < _maxEnemyCant) {
                InstantiateEnemy();
            }
            _time = 0;
        }
    }

    void InstantiateEnemy() {
        GameObject newEnemy = Instantiate(_enemyPrefab);
        newEnemy.transform.parent = this.gameObject.transform;
        newEnemy.GetComponent<NavMeshAgent>().Warp(GenerateRandomPosition());
    }

    Vector3 GenerateRandomPosition() {
        Vector3 _randPos;
        _randPos.x = Random.Range(_spawnArea[0], _spawnArea[2]);
        _randPos.y = _spawnArea[4];
        _randPos.z = Random.Range(_spawnArea[1], _spawnArea[3]);
        return _randPos;
    }
}
