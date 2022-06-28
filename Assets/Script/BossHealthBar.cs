using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider bossHpbar;
    public Text bossHptext;

    private void Start()
    {
        SetBossMaxHealth();
    }
    private void Update()
    {
        SetBossHealth();
        bossHptext.text = "HP : 600/ " + BossController.Instance.currHealth;
    }
    public void SetBossMaxHealth()
    {
        bossHpbar.maxValue = BossController.Instance.maxHealth;
        bossHpbar.value = BossController.Instance.maxHealth;
    }
    public void SetBossHealth()
    {
        bossHpbar.value = BossController.Instance.currHealth;
    }
}
