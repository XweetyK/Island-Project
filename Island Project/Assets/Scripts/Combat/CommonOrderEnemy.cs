using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonOrderEnemy : Enemy
{
    bool _attacked;
    private void Start() {
        _character = FindObjectOfType<CharacterStats>();
        _protect = _dead = false;
    }
    private void Update() {
        if (_life<=0) {
            _dead = true;
            _anim.SetBool("_dead",true);
        }
    }

    public override bool Act() {
        int _rand= Random.Range(0, 100);
        if (_rand<60) {
            Debug.Log("enemy attacks!");
            Attack();
        }
        if (_rand >= 60 && _rand <= 80) {
            Debug.Log("enemy's special!");
            Special();
        }
        if (_rand > 80) {
            Debug.Log("enemy blocked!");
            Defense();
        }
        return true;
    }

    private void Attack() {
        if (_attacked) {
            int rand = Random.Range(0, 100);
            if (rand > _missChance) {
                _character.GetDamage(_attack * 2 - _character.Defense);
            } else { Miss(); }
            _attacked = false;
            return;
        }
        _anim.SetTrigger("_attack1");
        _attacked = true;
        Invoke("Attack", _animTime);
    }
    private void Special() {
        if (_attacked) {
            int rand = Random.Range(0, 100);
            if (rand > _missChance) {
                _character.GetDamage(_attack * 5 - _character.Defense * 2 );
            } else { Miss(); }
            _attacked = false;
            return;
        }
        _anim.SetTrigger("_attack2");
        _attacked = true;
        Invoke("Attack", _animTime);
    }
    private void Defense() {
        _anim.SetTrigger("_block");
        _protect = true; 
    }

    private void Miss() {
        Debug.Log("missed!");
    }

    public override void GetDamage(int damage) {
        if (_protect) {
            _anim.SetTrigger("_block");
            _protect = false;
            return;
        } else {
            _anim.SetTrigger("_hit");
            _life -= damage;
        }
    }

}
