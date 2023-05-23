using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//using UnityEngine.Events;
// [System.Serializable]
// public  class EventVector3 : UnityEvent<Vector3> { }   //这两个是拖拽事件的写法

public class MouseManager : Singleton<MouseManager>
{
    // public static MouseManager Instance;
    //public EventVector3 onMouseClicked;//鼠标点击事件
    public event Action<Vector3> OnMouseClicked;//鼠标点击事件
    public event Action<GameObject> OnEnemyClicked;//敌人相关事件
    RaycastHit hitInfo;
    public Texture2D point, doorway, attack, target, arrow;
    // private void Awake() {
    //     if(Instance!=null)//单例模式
    //         Destroy(gameObject);
    //     Instance=this;
    // }
    //public GameObject cursor;
    public GameObject settings_Canvas;
    //public int count = 1;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        //将光标锁定到游戏窗口的中心。
        Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = false;
    }
    void Update()
    {
        //当点击esc后
        Mouse();
        if (Input.GetKeyDown(KeyCode.Escape)&&Time.timeScale==1)
        {
            //如果隐藏状态
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;//鼠标显示并解锁
                    settings_Canvas.SetActive(true);
                    Time.timeScale=0;
        }
        // if (Input.GetButtonDown("Fire1"))
        // {
        //     //Cursor.visible = false;
        //     //Cursor.lockState = CursorLockMode.Locked;
        // }

        SetCursorTexture();
        //MouseControl();
    }
    void SetCursorTexture()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitInfo, 50))
        {
            //设置贴图
            switch (hitInfo.collider.tag)
            {
                // case "Ground":
                //     Cursor.SetCursor(arrow,new Vector2(16,16),CursorMode.Auto);
                //     //cursor.GetComponent<RawImage>().texture = target;
                //     break;
                case "Enemy":
                    Cursor.SetCursor(attack,new Vector2(16,16),CursorMode.Auto);
                    //cursor.GetComponent<RawImage>().texture = attack;
                    break;
                case "DoorWay":
                    Cursor.SetCursor(doorway,new Vector2(16,16),CursorMode.Auto);
                   // cursor.GetComponent<RawImage>().texture = doorway;
                    break;
            }
            if(hitInfo.collider.transform.parent!=null&&hitInfo.collider.transform.parent.CompareTag("Ground")){
                Cursor.SetCursor(arrow,new Vector2(16,16),CursorMode.Auto);
            }
        }
    }
    void MouseControl()
    {
        if (Input.GetMouseButton(2) && hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.CompareTag("Ground"))
            {
                OnMouseClicked?.Invoke(hitInfo.point);//?是判断是否为空，不为空，invoke调用事件
            }
            if (hitInfo.collider.gameObject.CompareTag("Enemy"))
            {
                OnEnemyClicked?.Invoke(hitInfo.collider.gameObject);//?是判断是否为空，不为空，invoke调用事件
            }
            if (hitInfo.collider.gameObject.CompareTag("Attackable"))
            {
                OnEnemyClicked?.Invoke(hitInfo.collider.gameObject);//?是判断是否为空，不为空，invoke调用事件
            }
        }
    }
    private void Mouse()
    {
        if (Input.GetKeyDown(KeyCode.Tab)||Input.GetKeyDown(KeyCode.Escape))//鼠标中键点击
        {
            switch (Cursor.lockState)
            {
                case CursorLockMode.None://如果显示状态
                    // Cursor.lockState = CursorLockMode.Locked;//鼠标隐藏并锁定
                    Mouse_Pause.Instance.MouseHade();
                    break;
                case CursorLockMode.Locked://如果隐藏状态
                    // Cursor.visible = true;
                    // Cursor.lockState = CursorLockMode.None;//鼠标显示并解锁
                    Mouse_Pause.Instance.MouseShow();
                    break;
                default:
                    break;
            }
        }
    }
}
