using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Attack",menuName ="Attack/Attack Date")]
public class AttackDate_SO : ScriptableObject
{
    public float attackRange;
    public float skillRange;
    public float coolDown;
    public float minDamage;
    public float maxDamage;
    public float criticalMultiplier;
    public float criticalChance;
    public int killPoint;
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
        minDamage=(int)(minDamage*levelMultiplier);
        maxDamage=(int)(maxDamage*levelMultiplier);
    }
}
