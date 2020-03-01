using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasUpdater : MonoBehaviour
{
    [SerializeField] Animator _animPlayer;
    [SerializeField] Animator _animMission;
    [SerializeField] Image _lifeBar;
    [SerializeField] Image _expBar;
    [SerializeField] Text _life;
    [SerializeField] Text _exp;
    [SerializeField] Text _missionText;

    public static PlayerCanvasUpdater Instance { get; private set; }
    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }
    void Update()
    {
        if (CharacterStats.Instance.Health >= CharacterStats.Instance.MaxLife / 3) {
            _animPlayer.SetBool("Damaged", false);
        } else {
            _animPlayer.SetBool("Damaged", true);
        }

        _lifeBar.fillAmount = ((CharacterStats.Instance.Health * 100) / CharacterStats.Instance.MaxLife) * 0.01f;
        _expBar.fillAmount = ((CharacterStats.Instance.XP * 100) / CharacterStats.Instance.MaxXP) * 0.01f;
        _life.text = CharacterStats.Instance.Health.ToString();
        _exp.text = "LVL " + CharacterStats.Instance.Level.ToString();

        if (_missionText != null) {
            _missionText.text = EventManager.Instance.GetMission;
        }

    }
    public void NewMission() {
        _animMission.SetTrigger("Mission");
    }
}
