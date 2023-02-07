using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatPanel : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [Header("CheatStats")]
    public bool godMod;
    public int damage = 5;
    public bool enableDoubleJumping;
    public bool enableWallJumping;
    public bool enableAstral;
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

    }
    void Update()
    {
        godMod = Player.GetComponent<HealthSystem>().godMod;
        damage = Player.GetComponent<AttackSystem>().SlashLeft.GetComponent<Slash>().damage;
        enableDoubleJumping = Player.GetComponent<DoubleJumping>().enabled;
        enableWallJumping = Player.GetComponent<WallJumping>().enabled;
        enableAstral = Player.GetComponent<Astral>().enabled;
    }
}
