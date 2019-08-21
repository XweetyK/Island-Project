using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {
    [SerializeField] CombatInput _player;
    private Enemy _enemy;
    enum Turn{ENEMY, PLAYER};
    Turn _turn;
    bool _endTurn = false;
    private void Start() {
        _turn = Turn.PLAYER;
        _enemy = FindObjectOfType<Enemy>();
    }

    private void Update() {
        switch (_turn) {
            case Turn.ENEMY:
                if (_enemy.Act()) { _turn = Turn.PLAYER; _player.NewTurn(); }
                break;
            case Turn.PLAYER:
                if (_player.EndTurn) {
                    _turn = Turn.ENEMY;
                }
                break;
            default:
                break;
        }
    }

}
