using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {
    [Header("Character Stats")]
    [SerializeField] private int _maxLife;
    [SerializeField] private int _attack;
    [SerializeField] private int _defense;
    [SerializeField] private int _specialAttack;
    [SerializeField] private int _specialDefense;
    [SerializeField] private int _speed;

    [Header("Current State")]
    [SerializeField] private int _level;
    [SerializeField] private int _missChance;
    [SerializeField] private int _health;

    [Header("Stat Growth")]
    [SerializeField] [Range(0.0f, 1.0f)] private float _maxLifeGrowth;
    [SerializeField] [Range(0.0f, 1.0f)] private float _attackGrowth;
    [SerializeField] [Range(0.0f, 1.0f)] private float _defenseGrowth;
    [SerializeField] [Range(0.0f, 1.0f)] private float _specialAttackGrowth;
    [SerializeField] [Range(0.0f, 1.0f)] private float _specialDefenseGrowth;
    [SerializeField] [Range(0.0f, 1.0f)] private float _speedGrowth;

    private int _xp;
    private int _xpLimit;

    public static CharacterStats Instance { get; private set; }
    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    public int Health{
        get { return _health; }
        set { _health = value; }
    }
    public int Life {
        get { return _maxLife; }
    }
    public int Attack {
        get { return _attack; }
    }
    public int Defense {
        get { return _defense; }
    }
    public int SpecialAttack
    {
        get { return _specialAttack; }
    }
    public int SpecialDefense
    {
        get { return _specialDefense; }
    }
    public int Speed
    {
        get { return _speed; }
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
    public void sumXP (int plusXP) {
        _xp += plusXP;
        if(_xp > _xpLimit){
            _xp = _xpLimit;
        }
        LevelUp();
    }

    private void LevelUp() {
        _level++;
        _maxLife = Mathf.FloorToInt(_level * _maxLifeGrowth);
        _attack = Mathf.FloorToInt(_level * _attackGrowth);
        _defense = Mathf.FloorToInt(_level * _defenseGrowth);
        _specialAttack = Mathf.FloorToInt(_level * _specialAttackGrowth);
        _specialDefense = Mathf.FloorToInt(_level * _specialDefense);
        _speed = Mathf.FloorToInt(_level * _speedGrowth);
        _xp = 0;
        _xpLimit = Mathf.FloorToInt(_xpLimit * 1.2f);
    }
}
