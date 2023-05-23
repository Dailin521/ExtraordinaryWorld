using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.P)){
            SavePlayerDate();
        }
        if(Input.GetKeyDown(KeyCode.L)){
            LoadPlayerDate();
        }
    }
    public void SavePlayerDate(){
        Save(GameManager.Instance.playerStates.characterDate,GameManager.Instance.playerStates.characterDate.name);
    }
    public void LoadPlayerDate(){
        Load(GameManager.Instance.playerStates.characterDate,GameManager.Instance.playerStates.characterDate.name);
    }
    public void Save(Object date,string key){
        var jsonDate=JsonUtility.ToJson(date);
        PlayerPrefs.SetString(key,jsonDate);
        PlayerPrefs.Save();
    }
    public void Load(Object date,string key){
        if(PlayerPrefs.HasKey(key)){
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key),date);
        }
    }
}
