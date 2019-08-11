using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour{
    [SerializeField] protected string _name;
    [SerializeField] protected int _life;
    [SerializeField] protected int _attack;
    [SerializeField] protected int _defense;
    [SerializeField] protected int _missChance;
    [SerializeField] protected float _animTime;
    [SerializeField] protected Animator _anim;
    protected CharacterStats _character;
    protected int _initDef;
    protected int _initAtk;
    protected bool _protect;
    protected bool _dead;

    public abstract bool Act();
    public abstract void GetDamage(int damage);
}
