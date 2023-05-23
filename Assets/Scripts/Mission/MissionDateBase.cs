using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GoodsType{
    gold,
    enemyBat,
    enemySlime
}
[System.Serializable]
public struct MissionGoods{
    public GoodsType goodsType;
    public int goodsCount;
}
[System.Serializable]
public struct MissionDate{
    [Header("任务的描述")]
    public string des;

    [Header("任务的ID")]
    public int id;
    [Header("任务需要达成的条件")]
    public MissionGoods needGoods;

    [Header("任务完成的奖励")]
    public MissionGoods rewardGoods;
}
[CreateAssetMenu(fileName ="NewMissionDateBase",menuName ="CreateNewMissionDateBase/NewMissionDateBase")]
public class MissionDateBase : ScriptableObject
{
    public List<MissionDate> missionTasks;
}
