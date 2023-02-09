using TMPro;
using UnityEngine;

public class CheatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI godMod;
    [SerializeField] private TextMeshProUGUI DoubleJump;
    [SerializeField] private TextMeshProUGUI WallJump;
    [SerializeField] private TextMeshProUGUI Astral;
    [SerializeField] private TextMeshProUGUI MassiveDamage;

    void Update()
    {
        godMod.color = GameManager.instance.godMod ? Color.green : Color.red;
        DoubleJump.color = GameManager.instance.enableDoubleJumping ? Color.green : Color.red;
        WallJump.color = GameManager.instance.enableWallJumping ? Color.green : Color.red;
        Astral.color = GameManager.instance.enableAstral ? Color.green : Color.red;
        MassiveDamage.color = GameManager.instance.damage != 5 ? Color.green : Color.red;
    }
}
