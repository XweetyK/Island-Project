using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSfx : MonoBehaviour
{
    public void HoverButton() {
        SfxManager.Instance.Player(null, SFX.UI, 0, true);
    }
    public void ClickButton() {
        SfxManager.Instance.Player(null, SFX.UI, 1, true);
    }
}
