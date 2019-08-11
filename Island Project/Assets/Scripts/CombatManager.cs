using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {
    enum Turn{ENEMY, PLAYER};
    Turn _turn;
    [SerializeField] CombatInput _player;
    private void Update() {
        switch (_turn) {
            case Turn.ENEMY:
                Debug.Log("enemy turn");
                if (Input.GetButton("Left")){
                    _turn = Turn.PLAYER;
                }
                break;
            case Turn.PLAYER:
                Debug.Log("player turn");
                _player.NewTurn();
                if (_player.EndTurn) {
                    _turn = Turn.ENEMY;
                }
                break;
            default:
                break;
        }
    }
}
