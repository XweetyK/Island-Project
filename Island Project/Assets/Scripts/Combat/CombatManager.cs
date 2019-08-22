using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {
    [SerializeField] CombatInput _player;
    CharacterStats _playerStats;
    private Enemy _enemy;
    enum Turn{ENEMY, PLAYER};
    Turn _turn;
    bool _endTurn = false;
    private void Start() {
        _turn = Turn.PLAYER;
        _enemy = FindObjectOfType<Enemy>();
        _playerStats = FindObjectOfType<CharacterStats>();
    }

    private void Update() {
        switch (_turn) {
            case Turn.ENEMY:
                //if (_enemy.Act()) { _turn = Turn.PLAYER; _player.NewTurn(); }

                int _dmg = Combat(_enemy.Act(), _playerStats.Defense);
                Debug.LogWarning("Damage Done: "+_dmg);
                if (_dmg > 0) {
                    Debug.LogWarning("attacking! combat manager");
                    _playerStats.GetDamage(_dmg);
                }
                _turn = Turn.PLAYER;
                _player.NewTurn();
                break;

            case Turn.PLAYER:
                if (_player.EndTurn) {
                    _turn = Turn.ENEMY;
                }
                break;
        }
    }

    private int Combat(int damage, int defense) {
            Debug.LogWarning("Initial Damage: "+ damage);
        if (damage == 4943) {
            //The enemy is defending--------------
            return -1;
        } else {
            int _d = damage - defense;
            return _d;
        }
    }

}
