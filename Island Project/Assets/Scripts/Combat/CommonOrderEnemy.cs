using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonOrderEnemy : Enemy
{
    private int _maxLife;
    bool _attacked;
    private void Start() {
        _character = FindObjectOfType<CharacterStats>();
        _protect = _dead = false;
        _maxLife = _life;
    }
    private void Update() {
        if (_life<=0) {
            _dead = true;
            _anim.SetBool("_dead",true);
        }
    }

    public override int Act() {
        int _rand= Random.Range(0, 100);
        if (_rand<60) {
            Debug.Log("enemy attacks!");
            return Attack();
        }
        if (_rand >= 60 && _rand <= 80) {
            Debug.Log("enemy's special!");
            return Special();
        }
        if (_rand > 80) {
            Debug.Log("enemy blocked!");
            return Defense();
        }
        return -1;
    }

    private int Attack() {
        _anim.SetTrigger("_attack1");
        _messageText.text = "Enemy ORDER Attacks!";
        _messageAnim.SetTrigger("_enemyTurn");
        int rand = Random.Range(0, 100);
        if (rand > _missChance) {
            int _a = _attack * 2;
            Invoke("CamShake", 1.1f);
            return _a;
        } else {  return Miss(); }
    }
    private int Special() {
        _anim.SetTrigger("_attack2");
        _messageText.text = "Enemy ORDER [CENSORES] you!";
        _messageAnim.SetTrigger("_enemyTurn");
        int rand = Random.Range(0, 100);
        if (rand > _missChance) {
            int _a = _attack * 5;
            Invoke("CamShake", 1.2f);
            return _a;
        } else { return Miss(); }
    }
    private int Defense() {
        _anim.SetTrigger("_block");
        _messageText.text = "Enemy ORDER is ready!";
        _messageAnim.SetTrigger("_enemyTurn");
        _protect = true; 
        return 4943;
    }

    private int Miss() {
        Debug.Log("missed!");
        if (_missedBox.sprite != _redBox) {
            _missedBox.sprite = _redBox;
        }
        _missedAnim.SetTrigger("_missed");
        return 0;
    }

    public override void GetDamage(int damage) {
        if (_protect) {
            _anim.SetTrigger("_block");
            _messageText.text = "Enemy ORDER blocked your attack!";
            _messageAnim.SetTrigger("_enemyTurn");
            _protect = false;
            return;
        } else {
            if (damage > 0) {
                _anim.SetTrigger("_hit");
                _life -= damage - _defense * 2;
            }
        }
    }
    public override int Health() {
        return _life;
    }
    public override int MaxLife() {
        return _maxLife;
    }

    void CamShake() {
        CombatManager.Instance.CombatCamera.StartShake();
    }
}
