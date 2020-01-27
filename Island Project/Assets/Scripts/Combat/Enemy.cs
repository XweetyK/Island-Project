using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour{
    [SerializeField] protected string _name;
    [SerializeField] protected int _life;
    [SerializeField] protected int _attack;
    [SerializeField] protected int _defense;
    [SerializeField] protected int _missChance;
    [SerializeField] protected float _animTime;
    [SerializeField] protected Sprite _redBox;
    [SerializeField] protected Animator _anim;
    [SerializeField] protected Animator _messageAnim;
    [SerializeField] protected Animator _missedAnim;
    [SerializeField] protected Image _missedBox;
    [SerializeField] protected Text _messageText;
    protected CharacterStats _character;
    protected int _initDef;
    protected int _initAtk;
    protected bool _protect;
    protected bool _dead;

    public abstract int Act();
    public abstract void GetDamage(int damage);
    public abstract int Health();
    public abstract int MaxLife();
    public abstract void Revive();
}
