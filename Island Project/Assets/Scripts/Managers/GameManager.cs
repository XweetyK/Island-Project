﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public enum GameMode { EXPLORE, COMBAT };
    private GameMode _gameMode = GameMode.EXPLORE;
    [SerializeField] GameObject _combatCanvas;
    [SerializeField] GameObject _dialogCanvas;
    [SerializeField] GameObject _playerCanvas;
    [SerializeField] GameObject _exploreTerrain;
    [SerializeField] GameObject _combatTerrain;
    [SerializeField] GameObject _playerCamera;
    [SerializeField] GameObject _combatCamera;
    [SerializeField] GameObject _interaction;
    GameObject _player;
    List<GameObject> _mapEnemies;
    [SerializeField] Animator _effectTransition;
    [SerializeField] Animator _combatCanvasAnim;

    //UI elements
    [Header("Ui elements")]
    [SerializeField] GameObject winnerText;

    bool _pausable = true;

    bool _readyCombat = false;
    bool _readyExplore = false;

    EnemyMov _contactEnemy;

    //Global inputs
    bool _playerInput = true;
    bool _playerInteract = true;

    public static GameManager Instance { get; private set; }
    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }
        _combatCanvas.SetActive(false);
        _dialogCanvas.SetActive(true);
        _playerCanvas.SetActive(true);
        _mapEnemies = new List<GameObject>();
    }

    void Start() {
        _player = GameObject.FindGameObjectWithTag("Protagonist");
    }

    // Update is called once per frame
    void Update() {
        GameModeChecker();
    }

    private void GameModeChecker() {
        switch (_gameMode) {
            case GameMode.EXPLORE:
                ExploreMode();
                break;
            case GameMode.COMBAT:
                CombatMode();
                break;
        }
    }

    private void ExploreMode() {
        if (!_readyExplore) {
            _combatTerrain.SetActive(false);
            _combatCanvas.SetActive(false);
            _dialogCanvas.SetActive(true);
            _playerCanvas.SetActive(true);
            _combatCamera.SetActive(false);
            _exploreTerrain.SetActive(true);
            _player.SetActive(true);
            _playerInput = true;
            _playerCamera.SetActive(true);
            _interaction.SetActive(true);
            foreach (GameObject enemy in _mapEnemies) {
                enemy.SetActive(true);
            }
            _effectTransition.SetBool("_win", false);
            _readyCombat = false;
            _readyExplore = true;
        }
    }

    private void CombatMode() {
        if (!_readyCombat) {
            _playerInput = false;
            _effectTransition.SetTrigger("_contact");
            foreach (GameObject enemy in _mapEnemies) {
                enemy.GetComponent<EnemyMov>().IsFrozen(true);
            }
            Invoke("CameraTransitions", 1.38f);
            Invoke("AnimateCanvas", 5.0f);
            _combatTerrain.SetActive(true);
            _readyExplore = false;
            _readyCombat = true;
        }
    }

    private void CameraTransitions() {
        _exploreTerrain.SetActive(false);
        _player.SetActive(false);
        _interaction.SetActive(false);
        foreach (GameObject enemy in _mapEnemies) {
            enemy.SetActive(false);
        }
        _combatCanvas.SetActive(true);
        _dialogCanvas.SetActive(false);
        _playerCanvas.SetActive(false);
        _combatCamera.SetActive(true);
        _effectTransition.SetTrigger("_loaded");
    }

    private void AnimateCanvas() {
        _combatCanvasAnim.SetTrigger("_combatStart");
    }

    public bool PlayerInput {
        get { return _playerInput; }
        set { _playerInput = value; }
    }
    public bool PlayerInteract {
        get { return _playerInteract; }
        set { _playerInteract = value; }
    }

    public void SetActualEnemy(EnemyMov enemy) {
        _contactEnemy = enemy;
    }

    public void ChangeGameMode(GameMode mode) {
        _gameMode = mode;
    }

    public void ChangeGameMode(int mode) {
        switch (mode) {
            case 1:
                _gameMode = GameMode.COMBAT;
                break;
            case 2:
                _gameMode = GameMode.EXPLORE;
                if (_contactEnemy != null) {
                    _contactEnemy.Dead();
                }
                //_mapEnemies.Remove(_contactEnemy.gameObject);
                _contactEnemy = null;
                break;
        }
    }

    public void EndCombat(bool won) {
        if (!won) {
            _effectTransition.SetBool("_lose", true);
            Invoke("Gameover",3.0f);
        }
    }

    public void Gameover(){
        CharacterStats.Instance.Restart();
    }

    public void EndCombat(bool won, string name, int exp, bool levelUp)
    {
        switch (won)
        {
            case true:
                _effectTransition.SetBool("_win", true);
                Text winText = winnerText.GetComponent<Text>();
                if(winText != null){
                    winText.text = name + " was defeated." + "\n" + exp + " experience obtained.";
                }
                if (levelUp){
                    winText.text += System.Environment.NewLine + "Level Up!";
                }
                break;
            case false:
                break;
        }
    }

    public void AddEnemy(GameObject enemy) {
        _mapEnemies.Add(enemy);
    }
    public void RemoveEnemy(GameObject enemy) {
        _mapEnemies.Remove(enemy);
    }

   public bool Pausable {
        get { return _pausable; }
        set { _pausable = value; }
    }

    public int EnemiesInMap() {
        return _mapEnemies.Count;
    }
}
