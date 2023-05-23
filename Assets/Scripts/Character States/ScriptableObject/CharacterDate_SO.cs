using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Date",menuName ="Character States/Date")]
public class CharacterDate_SO : ScriptableObject
{
    [Header("States Info")]
    public int maxHealth;
    public int currentHealth;
    public int baseDefence;
    public int currentDefence;
    public int killPoint;
    [Header("mission")]
    public int batPoint;
    public int slimePoint;
    public int goldPoint;
    [Header("Level")]
    public int currentLevel;
    public int maxLevel;
    public int baseExp;
    public int currentExp;
    public float levelBuff;
    public float levelMultiplier{
        get{return 1+(currentLevel-1)*levelBuff;}
    }
    public void UpdateExp(int point){
        currentExp+=point;
        if(currentExp>=baseExp){
            LevelUp();
        }
    }

    private void LevelUp()
    {
        //所有提升的数据方法
        currentExp=0;
        currentLevel=Mathf.Clamp(currentLevel+1,1,maxLevel);
        baseExp+=(int)(baseExp*levelMultiplier);
        //血量
        maxHealth=(int)(maxHealth*levelMultiplier);
        currentHealth=maxHealth;
        //Debug.Log("up"+currentLevel+"maxhelth:"+maxHealth);
    }
}
