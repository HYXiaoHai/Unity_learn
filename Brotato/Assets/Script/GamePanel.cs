using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    public static GamePanel instance;

    public Slider _hpSlider;
    public Slider _expSlider;
    public TMP_Text _moneyCount;//金币
    public TMP_Text _expCount;//等级 LV.0
    public TMP_Text _hpCount;//生命 10/15
    public TMP_Text _countDown;//倒计时 15
    public TMP_Text _waveCount;//波次15
    
    private void Awake()
    {
        instance = this;

        _hpSlider = GameObject.Find("HpSlider").GetComponent<Slider>();
        _expSlider = GameObject.Find("ExpSlider").GetComponent<Slider>();

        _moneyCount = GameObject.Find("MoneyCount").GetComponent<TMP_Text>();
        _expCount = GameObject.Find("ExpCount").GetComponent<TMP_Text>();
        _hpCount = GameObject.Find("HpCount").GetComponent<TMP_Text>();
        _countDown = GameObject.Find("CountDown").GetComponent<TMP_Text>();
        _waveCount = GameObject.Find("WaveCount").GetComponent<TMP_Text>();


    }
    private void Start()
    {
        //更新生命
        RenewHp();
        //更新经验条
        RenewExp();
        //更新金币
        RenewMoney();

        RenewWaveCount();   
    }

    //更新生命
    public void RenewHp()
    {
        _hpCount.text = Player.instance.hp + "/" + Player.instance.maxHp;
        _hpSlider.value = Player.instance.hp / Player.instance.maxHp;

    }
    //更新经验条
    public void RenewExp()
    {
        _expSlider.value = Player.instance.exp % Player.instance.maxExp / Player.instance.maxExp;
        _expCount.text = "LV." + Player.instance.exp/ Player.instance.maxExp;
    }
    //更新金币
    public void RenewMoney()
    {
        _moneyCount.text = Player.instance.money.ToString();
    }
    //更新波次
    public void RenewWaveCount()
    {
        _waveCount.text = "第 "+GameManage.Instance.currentWave.ToString()+" 关";
    }
    //更新倒计时
    public void RenewCountDown(float time)
    {
        _countDown.text = time.ToString("F0");
    }
    void Update()
    {
        
    }
}
