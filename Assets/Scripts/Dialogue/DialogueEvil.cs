using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueEvil : MonoBehaviour
{
    public GameObject R_T;
    public GameObject js_Mission;
    [Header("UI组件")]
    private GameObject panel1;
    public Image faceImage1;
    public Text textLable1;
    [Header("文本文件")]
    public TextAsset textFile;
    private int index2;
    public float textSpeed;
    [Header("头像")]
    public Sprite face01, face02;
    bool textFinished;//是否完成打字
    bool cancelTyping;
    [Header("技能")]
    public GameObject skill2;
    R_Tri r_Tri;
    List<string> textList2 = new List<string>();
    private void Awake()
    {
        panel1 = this.transform.GetChild(0).gameObject;
        r_Tri=R_T.GetComponent<R_Tri>();
        GetTextFromFile(textFile);
    }
    private void OnEnable()
    {
        // textLable1.text=textList[index];
        // index++;
        textFinished = true;
        index2=0;
        StartCoroutine(SetTextUI());
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            panel1.SetActive(false);
            index2 = 0;
            return;
        }
        if (Input.GetKeyUp(KeyCode.R) && index2 == textList2.Count)
        {
            panel1.SetActive(false);
            js_Mission.SetActive(true);
            index2 = 0;
            return;
        }
        if(index2==16){
            skill2.SetActive(false);
        }
        // if (Input.GetKeyDown(KeyCode.R) && textFinished)
        // {
        //     // textLable1.text=textList[index];
        //     // index++;
        //     StartCoroutine(SetTextUI());
        // }
        if (Input.GetKeyDown(KeyCode.R)&&r_Tri.isShow)
        {
            if(textFinished&&!cancelTyping){
                StartCoroutine(SetTextUI());
            }else if(!textFinished){
                cancelTyping=!cancelTyping;
            }
        }

    }
    void GetTextFromFile(TextAsset file)
    {
        textList2.Clear();
        index2 = 0;
        var lineDate = file.text.Split('\n');
        foreach (var line in lineDate)
        {
            textList2.Add(line);
        }
    }
    IEnumerator SetTextUI()
    {
        textFinished = false;
        textLable1.text = "";
        switch (textList2[index2].Trim())
        {
            case "A":
                faceImage1.sprite = face01;
                index2++;
                break;
            case "B":
                faceImage1.sprite = face02;
                index2++;
                break;
        }
        // for (int i = 0; i < textList[index].Length; i++)
        // {
        //     textLable1.text += textList[index][i];
        //     yield return new WaitForSeconds(textSpeed);
        // }
        int letter = 0;
        while (!cancelTyping && letter < textList2[index2].Length - 1)
        {
            textLable1.text += textList2[index2][letter];
            letter++;
            yield return new WaitForSeconds(textSpeed);
        }
        textLable1.text = textList2[index2];
        cancelTyping = false;
        textFinished = true;
        index2++;
    }
}
