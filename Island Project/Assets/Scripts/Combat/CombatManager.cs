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
    [SerializeField] KeySpawner _kSpawner;

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
            int _d = damage - defense;
            return _d;
        }
    }

    private void UiUpdate() {
        _playerHealth.fillAmount = ((CharacterStats.Instance.Health * 100) / CharacterStats.Instance.Life) * 0.01f;
        _enemyHealth.fillAmount = ((_enemy.Health() * 100) / _enemy.MaxLife()) * 0.01f;
        if (CharacterStats.Instance.Health > 0) {
            _lifePercent.text = CharacterStats.Instance.Health.ToString();
        } else {
            _lifePercent.text = "0";
        }
        if (CharacterStats.Instance.Health < CharacterStats.Instance.Life/3) {
            _playerAnim.SetBool("_lowHealth", true);
        }
        if (CharacterStats.Instance.Health <= 0) {
            _playerAnim.SetBool("_isDead", true);
        }
    }

    IEnumerator EnemyTurn(float time) {
        //enemy actions
        int _dmg = Combat(_enemy.Act(), CharacterStats.Instance.Defense);
        if (_dmg > 0) {
            StartCoroutine(TakeDamage(_dmg, 1.1f));
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
        _playerAnim.SetTrigger("_damaged");
        CharacterStats.Instance.GetDamage(damage);
    }

    private void GameOver() {
        if (!_endGame) {
            _endGame = true;
        }
    }
    IEnumerator EndCombat() {
        yield return new WaitForSeconds(2.0f);
        if (!_endGame) {
            _endGame = true;
            GameManager.Instance.EndCombat(true);
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
