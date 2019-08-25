using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour {
    [SerializeField] CombatInput _player;
    [SerializeField] Image _playerHealth;
    [SerializeField] Image _enemyHealth;
    [SerializeField] Text _lifePercent;
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

    private void switchTurns(){
        switch (_turn)
        {
            case Turn.ENEMY:
                StartCoroutine("EnemyTurn", 2.0f);
                break;
        }
    }
    private void Update()
    {
        
        if(_turn == Turn.PLAYER)
        {
            if (_player.EndTurn)
            {
                _turn = Turn.ENEMY;
                StartCoroutine("PlayerTurn", 2.0f);
            }
        }
        UiUpdate();
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

    private void UiUpdate() {
        _playerHealth.fillAmount=((_playerStats.Health * 100) / _playerStats.Life) * 0.01f;
        _enemyHealth.fillAmount=((_enemy.Health() * 100) / _enemy.MaxLife()) * 0.01f;
        _lifePercent.text = _playerStats.Health.ToString();
    }

    IEnumerator EnemyTurn(float time){
        //enemy actions
        int _dmg = Combat(_enemy.Act(), _playerStats.Defense);
        Debug.LogWarning("Damage Done: " + _dmg);
        if (_dmg > 0)
        {
            Debug.LogWarning("attacking! combat manager");
            _playerStats.GetDamage(_dmg);
        }
        yield return new WaitForSeconds(time);
        //other turn
        _turn = Turn.PLAYER;
        _player.NewTurn();
        yield return null;
    }

    IEnumerator PlayerTurn(float time){
        yield return new WaitForSeconds(time);
        //other turn
        switchTurns();
        yield return null;
    }
}
