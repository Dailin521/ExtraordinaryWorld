using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStates : MonoBehaviour
{
    //音乐
    private GameObject au;
    private AudioSource getHitAu;

    public event Action<int,int>UpdateHealthBarOnAttack;
    public CharacterDate_SO tempLateDate;
    public CharacterDate_SO characterDate;
    public AttackDate_SO tempLateAttack;
    public AttackDate_SO attackDate;
    [HideInInspector]
    public bool isCritical;
    public int red_Medicine;

    private void Awake() {
        if(tempLateDate!=null){
            characterDate=Instantiate(tempLateDate);
        }
        if(tempLateAttack!=null){
            attackDate=Instantiate(tempLateAttack);
        }
        au=GameObject.FindGameObjectWithTag("Audio");
        getHitAu=au.gameObject.transform.Find("GetHit").GetComponent<AudioSource>();
    }
    #region Read From Date_SO
    public int MaxHealth
    {
        get
        {
            if (characterDate != null)
            {
                return characterDate.maxHealth;
            }
            else return 0;
        }
        set
        {
            characterDate.maxHealth = value;
        }
    }
    public int CurrentHealth
    {
        get
        {
            if (characterDate != null)
            {
                return characterDate.currentHealth;
            }
            else return 0;
        }
        set
        {
            characterDate.currentHealth = value;
        }
    }
    public int BaseDefence
    {
        get
        {
            if (characterDate != null)
            {
                return characterDate.baseDefence;
            }
            else return 0;
        }
        set
        {
            characterDate.baseDefence = value;
        }
    }
    public int CurrentDefence
    {
        get
        {
            if (characterDate != null)
            {
                return characterDate.currentDefence;
            }
            else return 0;
        }
        set
        {
            characterDate.currentDefence = value;
        }
    }
    public int GoldPoint
    {
        get
        {
            if (characterDate != null)
            {
                return characterDate.goldPoint;
            }
            else return 0;
        }
        set
        {
            characterDate.goldPoint = value;
        }
    }
    public int BatPoint
    {
        get
        {
            if (characterDate != null)
            {
                return characterDate.batPoint;
            }
            else return 0;
        }
        set
        {
            characterDate.batPoint = value;
        }
    }
    public int SlimePoint
    {
        get
        {
            if (characterDate != null)
            {
                return characterDate.slimePoint;
            }
            else return 0;
        }
        set
        {
            characterDate.slimePoint = value;
        }
    }
    #endregion

    #region Combat
    public void TakeDamage(CharacterStates attacker,CharacterStates defender){
        int damage=Mathf.Max(attacker.CurrentDamage()-defender.CurrentDefence,0);
        Debug.Log("damage:"+damage);
        CurrentHealth=Mathf.Max(CurrentHealth-damage,0);

        if(attacker.isCritical){
            defender.GetComponent<Animator>().SetTrigger("Hit");
            Debug.Log("baoji");
            getHitAu.Play();
        }
        //TODO:update UI
        UpdateHealthBarOnAttack?.Invoke(CurrentHealth,MaxHealth);
        //TODO: 经验 update
        if(defender.CurrentHealth<=0){
            attacker.characterDate.UpdateExp(defender.characterDate.killPoint);
            attacker.attackDate.UpdateExp(defender.characterDate.killPoint);
            attacker.GoldPoint++;
            switch (defender.gameObject.name)
            {
                case "BatPBR":
                attacker.BatPoint++;
                break;
                case "BatPBR(Clone)":
                attacker.BatPoint++;
                break;
                case "SlimePBR":
                attacker.SlimePoint++;
                break;
                case "SlimePBR(Clone)":
                attacker.SlimePoint++;
                break;
            }
        }
    }
    public void TakeDamage(int damage,CharacterStates defender){
        // Debug.Log("damage:"+damage);
        Debug.Log("de:"+defender.CurrentDefence);
        int currentDamage=Mathf.Max(damage-defender.CurrentDefence,0);
        // Debug.Log("damage:"+damage);
        CurrentHealth=Mathf.Max(CurrentHealth-damage,0);
        //TODO:update UI
        UpdateHealthBarOnAttack?.Invoke(CurrentHealth,MaxHealth);
        //TODO: 经验 update
        
    }
    public void TakeDamage(int damage,CharacterStates attacker, CharacterStates defender){
        // Debug.Log("damage:"+damage);
        Debug.Log("de:"+defender.CurrentDefence);
        int currentDamage=Mathf.Max(damage-defender.CurrentDefence,0);
        // Debug.Log("damage:"+damage);
        CurrentHealth=Mathf.Max(CurrentHealth-damage,0);
        //TODO:update UI
        UpdateHealthBarOnAttack?.Invoke(CurrentHealth,MaxHealth);
        //TODO: 经验 update
        if(defender.CurrentHealth<=0){
            attacker.characterDate.UpdateExp(defender.characterDate.killPoint);
            attacker.attackDate.UpdateExp(defender.characterDate.killPoint);
            attacker.GoldPoint++;
        }
    }
    public void TakeDamage(float damage,CharacterStates attacker, CharacterStates defender){
        // Debug.Log("damage:"+damage);
        Debug.Log("de:"+defender.CurrentDefence);
        float currentDamage=Mathf.Max(damage-defender.CurrentDefence,0);
        // Debug.Log("damage:"+damage);
        CurrentHealth=(int)Mathf.Max(CurrentHealth-damage,0);
        //TODO:update UI
        UpdateHealthBarOnAttack?.Invoke(CurrentHealth,MaxHealth);
        //TODO: 经验 update
        if(defender.CurrentHealth<=0){
            attacker.characterDate.UpdateExp(defender.characterDate.killPoint);
            attacker.attackDate.UpdateExp(defender.characterDate.killPoint);
            attacker.GoldPoint++;
        }
    }

    private int CurrentDamage()
    {
        float coreDamage=UnityEngine.Random.Range(attackDate.minDamage,attackDate.maxDamage);
        if(isCritical){
            coreDamage*=attackDate.criticalMultiplier;
            Debug.Log("baoji:"+coreDamage);
        }
        return (int)coreDamage;
    }
    #endregion


}
