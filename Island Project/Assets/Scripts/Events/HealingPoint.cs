using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPoint : MonoBehaviour {
    public void Heal() {
        CharacterStats.Instance.Health = CharacterStats.Instance.MaxLife;
    }
}
