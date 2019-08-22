using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {
    [SerializeField] private int _maxLife;
    [SerializeField] private int _attack;
    [SerializeField] private int _defense;
    [SerializeField] private int _level;
    [SerializeField] private int _growth;
    [SerializeField] private int _missChance;
    [SerializeField] private int _health;
    private int _xp;
    private int _xpLimit;


    public int Health{
        get { return _health; }
        set { _health = value; }
    }
    public int Life {
        get { return _maxLife; }
    }
    public int Defense {
        get { return _defense; }
    }
    public int Level {
        get { return _level; }
    }
    public int XP {
        get { return _xp; }
    }
    public void GetDamage(int damage) {
        _health -= damage;
    }
    public void SumDefense(int plusDefense) {
        _defense += plusDefense;
    }
    public void GainLevel() {
        _level++;
    }
    public void sumXP (int plusXP) {
        _xp += plusXP;
    }

    private void LevelUp() {
        _level++;
        _maxLife += _level * _growth;
        _defense += _level * _growth;
    }
}
