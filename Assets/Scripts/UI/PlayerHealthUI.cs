using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    Text levelText;
    Text goldText;
    Text red_MedicinText;
    Image healthSlider;
    Image expSlider;
    private void Awake() {
        levelText=transform.GetChild(2).GetComponent<Text>();
        goldText=transform.GetChild(3).GetChild(0).GetComponent<Text>();
        red_MedicinText=transform.GetChild(4).GetChild(0).GetComponent<Text>();
        healthSlider=transform.GetChild(0).GetChild(0).GetComponent<Image>();
        expSlider=transform.GetChild(1).GetChild(0).GetComponent<Image>();

    }
    private void Update() {
        levelText.text="Level  "+GameManager.Instance.playerStates.characterDate.currentLevel.ToString("00");
        goldText.text="x "+GameManager.Instance.playerStates.GoldPoint.ToString("00");
        red_MedicinText.text="x "+GameManager.Instance.playerStates.red_Medicine.ToString("00");
        UpdateExp();
        UpdateHealth();
    }
    void UpdateHealth(){
        float sliderPercent=(float)GameManager.Instance.playerStates.CurrentHealth/GameManager.Instance.playerStates.MaxHealth;
        healthSlider.fillAmount=sliderPercent;
    }
    void UpdateExp(){
        float sliderPercent=(float)GameManager.Instance.playerStates.characterDate.currentExp/GameManager.Instance.playerStates.characterDate.baseExp;
        expSlider.fillAmount=sliderPercent;
    }
    
}
