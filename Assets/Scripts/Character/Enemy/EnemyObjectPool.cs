using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    public GameObject ParentEnemy;//所有克隆出来的enemy都放置在此对象下
    public GameObject[] Enemy;//存储敌人预制体的数组
    public Transform[] BornPoint;//存储出生点位置的数组
    public int EnemyMax = 2;//enemy的上限 小于此阈值才会刷新新的enemy
    private int EnemyNum;//当前游戏中enemy的数量
    void Start()
    {
        InvokeRepeating("EnemyIncrease", 1, 3);//延时重复执行
    }
    void EnemyIncrease()
    {
        EnemyNum = ParentEnemy.transform.childCount;//检查父对象下enemy数量
        int EnemyType = Random.Range(0, 2);
        //Debug.Log(EnemyType);
        int EnemyBorn = Random.Range(0, 4);
        //克隆enemy 类型随机 位置随机
        if (EnemyNum < EnemyMax)
        {
            GameObject EnemyObj = GameObject.Instantiate(Enemy[EnemyType], BornPoint[EnemyBorn].position, BornPoint[EnemyBorn].rotation);
            EnemyObj.transform.parent = ParentEnemy.transform;
        }
    }
}
