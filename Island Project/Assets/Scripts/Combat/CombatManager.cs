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
    [SerializeField] Image _playerFace;
    [SerializeField] Animator _playerAnim;

    public struct DamageType {
        public int dmg;
        public bool special;
    }

    private Enemy _enemy;
    enum Turn { ENEMY, PLAYER };
    Turn _turn;
    bool _endTurn = false;
    bool _endGame = false;

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
            if (_endTurn && _enemy.Health() > 0) {
                _turn = Turn.ENEMY;
                StartCoroutine("PlayerTurn", 2.0f);
            } else if (_enemy.Health() < 0) {
                StartCoroutine(EndCombat());
            }
        }
        if (CharacterStats.Instance.Health <= 0) {
            GameOver();
        }
        UiUpdate();
    }

    private int Combat(int damage, int defense) {
        if (damage == 4943) {
            //The enemy is defending--------------
            return -1;
        } else {
            int _d = Mathf.RoundToInt(damage * damage / (damage + defense));
            return _d;
        }
    }

    private void UiUpdate() {
        _playerHealth.fillAmount = ((CharacterStats.Instance.Health * 100) / CharacterStats.Instance.MaxLife) * 0.01f;
        _enemyHealth.fillAmount = ((_enemy.Health() * 100) / _enemy.MaxLife()) * 0.01f;
        if (CharacterStats.Instance.Health > 0) {
            _lifePercent.text = CharacterStats.Instance.Health.ToString();
        } else {
            _lifePercent.text = "0";
        }
        if (CharacterStats.Instance.Health < CharacterStats.Instance.MaxLife/3) {
            _playerAnim.SetBool("_lowHealth", true);
        }
        if (CharacterStats.Instance.Health <= 0) {
            _playerAnim.SetBool("_isDead", true);
        }
    }

    IEnumerator EnemyTurn(float time) {
        //enemy actions
        DamageType attack = _enemy.Act();
        int damageDone;
        if (!attack.special){
            damageDone = Combat(attack.dmg, CharacterStats.Instance.Defense);
        }
        else{
            damageDone = Combat(attack.dmg, CharacterStats.Instance.SpecialDefense); ;
        }
        if (damageDone > 0) {
            StartCoroutine(TakeDamage(damageDone, 1.1f));
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
        if (damage > 0) {
            _combatCam.StartShake();
        }
        _endTurn = true;
    }

    IEnumerator TakeDamage(int damage, float delay) {
        yield return new WaitForSeconds(delay);
        if (!_player._protected) {
            _playerAnim.SetTrigger("_damaged");
            CharacterStats.Instance.GetDamage(damage);
        } else {
            _player.Protected();
        }
    }

    private void GameOver() {
        if (!_endGame) {
            _endGame = true;
        }
        GameManager.Instance.EndCombat(false);
    }
    IEnumerator EndCombat() {
        yield return new WaitForSeconds(2.0f);
        if (!_endGame) {
            _endGame = true;
            int exp = 3;
            bool didLevelUp = CharacterStats.Instance.sumXP(exp);
            GameManager.Instance.EndCombat(true, "ORDER" , exp, didLevelUp);
        }
    }

    private void OnDisable() {
        _endGame = false;
        _endTurn = false;
        _turn = Turn.PLAYER;
        CombatInput.Instance.Restart();
        if (_enemy != null) {
            _enemy.Revive();
        }
    }


}
