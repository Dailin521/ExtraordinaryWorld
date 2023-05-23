using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionTask : MonoBehaviour
{
    public GameObject skill3;
    public int id;
    private Text des;
    private Text need;
    private Text reward;
    private Button get;
    public MissionDateBase missionDateBase;
    bool isFinish=false;
    private void Awake() {
        des=transform.Find("des").GetComponent<Text>();
        need=transform.Find("needs").GetComponent<Text>();
        reward=transform.Find("reward").GetComponent<Text>();
        get=transform.Find("get").GetComponent<Button>();
    }
    private void OnEnable() {
        if(isFinish){
            gameObject.SetActive(false);
        }
        if(GameManager.Instance.isJS_Mission){
            des.text=missionDateBase.missionTasks[id].des;
        reward.text="获得：金币"+missionDateBase.missionTasks[id].rewardGoods.goodsCount.ToString("00");
        if(missionDateBase.missionTasks[id].needGoods.goodsType==GoodsType.gold){
            int currentCoin=GameManager.Instance.playerStates.GoldPoint;
            need.text="条件："+currentCoin.ToString("00")+"/"+missionDateBase.missionTasks[id].needGoods.goodsCount.ToString("00");
            if(currentCoin>=missionDateBase.missionTasks[id].needGoods.goodsCount){
                get.gameObject.SetActive(true);
                get.onClick.AddListener(()=>{
                    GameManager.Instance.playerStates.GoldPoint+=missionDateBase.missionTasks[id].rewardGoods.goodsCount;
                    isFinish=true;
                    get.onClick.RemoveAllListeners();
                    get.transform.parent.gameObject.SetActive(false);
                });
            }
        }
        if(missionDateBase.missionTasks[id].needGoods.goodsType==GoodsType.enemyBat){
            int currentBat=GameManager.Instance.playerStates.BatPoint;
            need.text="条件："+currentBat.ToString("00")+"/"+missionDateBase.missionTasks[id].needGoods.goodsCount.ToString("00");
            if(currentBat>=missionDateBase.missionTasks[id].needGoods.goodsCount){
                get.gameObject.SetActive(true);
                get.onClick.AddListener(()=>{
                    GameManager.Instance.playerStates.GoldPoint+=missionDateBase.missionTasks[id].rewardGoods.goodsCount;
                    skill3.SetActive(false);
                    isFinish=true;
                    get.onClick.RemoveAllListeners();
                    get.transform.parent.gameObject.SetActive(false);
                });
            }
            reward.text="获得:蓄能波技能解锁";
        }
        if(missionDateBase.missionTasks[id].needGoods.goodsType==GoodsType.enemySlime){
            int currentSlime=GameManager.Instance.playerStates.SlimePoint;
            need.text="条件："+currentSlime.ToString("00")+"/"+missionDateBase.missionTasks[id].needGoods.goodsCount.ToString("00");
            if(currentSlime>=missionDateBase.missionTasks[id].needGoods.goodsCount){
                get.gameObject.SetActive(true);
                get.onClick.AddListener(()=>{
                    GameManager.Instance.playerStates.GoldPoint+=missionDateBase.missionTasks[id].rewardGoods.goodsCount;
                    isFinish=true;
                    get.onClick.RemoveAllListeners();
                    get.transform.parent.gameObject.SetActive(false);
                });
            }
        }
        
        }
        
    }
}
