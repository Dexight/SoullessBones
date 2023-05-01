using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [SerializeField] GameObject m_bar;
    [SerializeField] Enemy m_enemy;
    [SerializeField] Image m_Hbar;
    [SerializeField] Image m_Dbar;

    int maxHp;
    int curHp;
    void Start()
    {
        m_bar.SetActive(false);
    }

    public void StartFight()
    {
        m_bar.SetActive(true);
    }
    public void EndFight()
    {
        m_bar.SetActive(false);
    }
    public void Initialize(int hp)
    {
        maxHp = hp;
        curHp = hp;
        m_Hbar.fillAmount = 1;
        m_Dbar.fillAmount = 1;
    }
    public void Damaged(int newHP)
    {
        m_Dbar.fillAmount = (float)curHp / maxHp;
        curHp = newHP;
        m_Hbar.fillAmount = (float)curHp / maxHp;

        StartCoroutine(afterDamage(curHp));
    }
    IEnumerator afterDamage(int newHP)
    {
        yield return new WaitForSeconds(1);
        if (newHP == curHp)
        {
            m_Dbar.fillAmount = (float)curHp / maxHp;
        }
    }
}
