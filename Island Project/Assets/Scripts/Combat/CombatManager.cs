using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour {
    [SerializeField] CombatInput _player;
    [SerializeField] Image _playerHealth;
    [SerializeField] Image _enemyHealth;
    [SerializeField] Text _lifePercent;
    [SerializeField] CameraShake _combatCam;

    private Enemy _enemy;
    enum Turn { ENEMY, PLAYER };
    Turn _turn;
    bool _endTurn = false;

    public static CombatManager Instance { get; private set; }
    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }
    private void Start() {
        _turn = Turn.PLAYER;
        _enemy = FindObjectOfType<Enemy>();
    }

    private void switchTurns() {
        switch (_turn) {
            case Turn.ENEMY:
                StartCoroutine("EnemyTurn", 2.0f);
                break;
        }
    }
    private void Update() {

        if (_turn == Turn.PLAYER) {
            if (_endTurn) {
                _turn = Turn.ENEMY;
                StartCoroutine("PlayerTurn", 2.0f);
            }
        }
        UiUpdate();
    }

    private int Combat(int damage, int defense) {
        Debug.LogWarning("Initial Damage: " + damage);
        if (damage == 4943) {
            //The enemy is defending--------------
            return -1;
        } else {
            int _d = damage - defense;
            return _d;
        }
    }

    private void UiUpdate() {
        _playerHealth.fillAmount = ((CharacterStats.Instance.Health * 100) / CharacterStats.Instance.Life) * 0.01f;
        _enemyHealth.fillAmount = ((_enemy.Health() * 100) / _enemy.MaxLife()) * 0.01f;
        _lifePercent.text = CharacterStats.Instance.Health.ToString();
    }

    IEnumerator EnemyTurn(float time) {
        //enemy actions
        int _dmg = Combat(_enemy.Act(), CharacterStats.Instance.Defense);
        Debug.LogWarning("Damage Done: " + _dmg);
        if (_dmg > 0) {
            Debug.LogWarning("attacking! combat manager");
            CharacterStats.Instance.GetDamage(_dmg);
        }
        yield return new WaitForSeconds(time);
        //other turn
        _turn = Turn.PLAYER;
        _endTurn = false;
        _player.NewTurn();
        yield return null;
    }

    IEnumerator PlayerTurn(float time) {
        yield return new WaitForSeconds(time);
        //other turn
        switchTurns();
        yield return null;
    }

    public CameraShake CombatCamera {
        get { return _combatCam; }
    }

    public void SendPlayerAttack(int damage) {
        _enemy.GetDamage(damage);
        _combatCam.StartShake();
        _endTurn = true;
    }
}
