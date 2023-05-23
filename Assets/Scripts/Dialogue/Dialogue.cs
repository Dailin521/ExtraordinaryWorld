using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject R_T;//
    [Header("UI组件")]
    private GameObject panel1;
    public Image faceImage1;
    public Text textLable1;
    [Header("文本文件")]
    public TextAsset textFile;
    private int index;
    public float textSpeed;
    [Header("头像")]
    public Sprite face01, face02;
    bool textFinished;//是否完成打字
    bool cancelTyping;
    [Header("技能")]
    public GameObject skill1;
    public GameObject skillUI;
    R_Tri r_Tri;
    List<string> textList = new List<string>();
    private void Awake()
    {
        panel1 = this.transform.GetChild(0).gameObject;
        r_Tri = R_T.GetComponent<R_Tri>();
        GetTextFromFile(textFile);
    }
    private void OnEnable()
    {
        // textLable1.text=textList[index];
        // index++;
        textFinished = true;
        StartCoroutine(SetTextUI());
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            panel1.SetActive(false);
            index = 0;
            return;
        }
        if (Input.GetKeyUp(KeyCode.R) && index == textList.Count)
        {

            panel1.SetActive(false);
            index = 0;
            return;
        }
        if(index==21){
            skillUI.SetActive(true);
        }if(index==23){
            skill1.SetActive(false);
        }
        // if (Input.GetKeyDown(KeyCode.R) && textFinished)
        // {
        //     // textLable1.text=textList[index];
        //     // index++;
        //     StartCoroutine(SetTextUI());
        // }
        if (Input.GetKeyDown(KeyCode.R) && r_Tri.isShow)
        {
            if (textFinished && !cancelTyping)
            {
                StartCoroutine(SetTextUI());
            }
            else if (!textFinished)
            {
                cancelTyping = !cancelTyping;
            }
        }

    }
    void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;
        var lineDate = file.text.Split('\n');
        foreach (var line in lineDate)
        {
            textList.Add(line);
        }
    }
    IEnumerator SetTextUI()
    {
        textFinished = false;//文本显示未结束
        textLable1.text = "";//首行赋空
        switch (textList[index].Trim())
        {
            case "A":
                faceImage1.sprite = face01;//检测到A则替换相应头像
                index++;//行数加1
                break;
            case "B":
                faceImage1.sprite = face02;
                index++;
                break;
        }
        // for (int i = 0; i < textList[index].Length; i++)
        // {
        //     textLable1.text += textList[index][i];
        //     yield return new WaitForSeconds(textSpeed);
        // }
        int letter = 0;
        while (!cancelTyping && letter < textList[index].Length - 1)
        {
            textLable1.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(textSpeed);
        }
        textLable1.text = textList[index];
        cancelTyping = false;
        textFinished = true;
        index++;
    }
}
