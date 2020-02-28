using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {
    [Header("Character Stats")]
    [SerializeField] private int _baseMaxLife;
    [SerializeField] private int _baseAttack;
    [SerializeField] private int _baseDefense;
    [SerializeField] private int _baseSpecialAttack;
    [SerializeField] private int _baseSpecialDefense;
    [SerializeField] private int _baseSpeed;

    private int _maxLife;
    private int _attack;
    private int _defense;
    private int _specialAttack;
    private int _specialDefense;
    private int _speed;

    [Header("Current State")]
    [SerializeField] private int _level;
    [SerializeField] private int _missChance;
    private int _health;

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
        DontDestroyOnLoad(this.gameObject);
    }
    public void Start()
    {
        updateStats();
        _health = _maxLife;
    }

    public int Health{
        get { return _health; }
        set { _health = value; }
    }
    public int MaxLife {
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
    }public int MaxXP {
        get { return _xpLimit; }
    }
    public void GetDamage(int damage) {
        _health -= damage;
    }
    public void SumDefense(int plusDefense) {
        _defense += plusDefense;
    }
    public bool sumXP (int plusXP) {
        bool didLevelUp = false;
        _xp += plusXP;
        if(_xp > _xpLimit){
            _xp = _xpLimit;
            LevelUp();
            didLevelUp = true;
        }
        return didLevelUp;
    }

    private void updateStats(){
        _maxLife = Mathf.FloorToInt(_baseMaxLife + _level * _maxLifeGrowth);
        _attack = Mathf.FloorToInt(_baseAttack + _level * _attackGrowth);
        _defense = Mathf.FloorToInt(_baseDefense + _level * _defenseGrowth);
        _specialAttack = Mathf.FloorToInt(_baseAttack + _level * _specialAttackGrowth);
        _specialDefense = Mathf.FloorToInt(_baseDefense + _level * _specialDefense);
        _speed = Mathf.FloorToInt(_baseSpeed + _level * _speedGrowth);
        _xpLimit = nextLevel(_level);
    }

    private void LevelUp() {
        _level++;
        updateStats();
        _xp = 0;
        _health = _maxLife;
    }

    private int nextLevel(int level){
        return Mathf.RoundToInt(0.04f * (level ^ 3) + 0.8f * (level ^ 2) + 2 * level);
    }
    public void SetStats(int[] stats) {
        _baseMaxLife = stats[0];
        _baseAttack = stats[1];
        _baseDefense = stats[2];
        _baseSpecialAttack = stats[3];
        _baseSpecialDefense = stats[4];
        _baseSpeed = stats[5];

        _level = stats[6];
        _xp = stats[7];

        updateStats();
    }
    public int[] GetStats() {
        int[] stats = new int[8];
        stats[0] = _baseMaxLife;
        stats[1] = _baseAttack;
        stats[2] = _baseDefense;
        stats[3] = _baseSpecialAttack;
        stats[4] = _baseSpecialDefense;
        stats[5] = _baseSpeed;
        stats[6] = _level;
        stats[7] = _xp;

        return stats;
    }
}
