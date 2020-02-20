using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public enum GameMode { EXPLORE, COMBAT };
    private GameMode _gameMode = GameMode.EXPLORE;
    [SerializeField] GameObject _combatCanvas;
    [SerializeField] GameObject _dialogCanvas;
    [SerializeField] GameObject _exploreTerrain;
    [SerializeField] GameObject _combatTerrain;
    [SerializeField] GameObject _playerCamera;
    [SerializeField] GameObject _combatCamera;
    [SerializeField] GameObject _interaction;
    GameObject _player;
    List<GameObject> _mapEnemies;
    [SerializeField] Animator _effectTransition;
    [SerializeField] Animator _combatCanvasAnim;

    bool _readyCombat = false;
    bool _readyExplore = false;

    EnemyMov _contactEnemy;

    //Global inputs
    bool _playerInput = true;

    public static GameManager Instance { get; private set; }
    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }
        _combatCanvas.SetActive(false);
        _mapEnemies = new List<GameObject>();
    }

    void Start() {
        _player = GameObject.FindGameObjectWithTag("Player");
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
        _combatCamera.SetActive(true);
        _effectTransition.SetTrigger("_loaded");
    }

    private void AnimateCanvas() {
        _combatCanvasAnim.SetTrigger("_combatStart");
    }

    public bool PlayerInput{
        get { return _playerInput; }
        set { _playerInput = value; }
    }

    public void SetActualEnemy(EnemyMov enemy) {
        _contactEnemy = enemy;
    }

    public void ChangeGameMode (GameMode mode){
        _gameMode = mode;
    }

    public void ChangeGameMode (int mode){
        switch (mode) {
            case 1:
                _gameMode = GameMode.COMBAT;
                break;
            case 2:
                Debug.Log("button working");
                _gameMode = GameMode.EXPLORE;
                _contactEnemy.Dead();
                //_mapEnemies.Remove(_contactEnemy.gameObject);
                _contactEnemy = null;
                break;
        }
    }

    public void EndCombat(bool won) {
        switch (won) {
            case true:
                _effectTransition.SetBool("_win", true);
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
}
