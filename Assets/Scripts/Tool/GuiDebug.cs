using UnityEngine;
using UnityEngine.UI;

public class GuiDebug : MonoBehaviour
{
    private bool bShow = false;
    public Rect windowRect0 = new Rect(80, 100, 420, 366);


    public string mSzLog = "";
    // Use this for initialization
    void Start()
    {

    }


    void OnEnable()
    {
        Application.RegisterLogCallback(HandleLog);
    }
    void OnDisable()
    {
        Application.RegisterLogCallback(null);
    }


    void HandleLog(string logString, string stackTrace, LogType type)
    {
        //output = logString;  
        //stack = stackTrace;  
        mSzLog += logString + "\n";
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            bShow = !bShow;
            if (bShow)
            {
                OnEnable();
            }
            else
            {
                OnDisable();
            }
        }
    }


    void DoMyWindow(int windowID)
    {
        if (GUI.Button(new Rect(20, 20, 80, 20), "Add"))
        {
            mSzLog += "Hello!!\n";
        }
        if (GUI.Button(new Rect(120, 20, 80, 20), "Clear"))
        {
            mSzLog = "";
        }
        GUI.TextArea(new Rect(20, 50, 380, 300), mSzLog);
        //GUI.DragWindow（）;//


        GUI.DragWindow(new Rect(0, 0, 600, 480));


        //  
    }




    void OnGUI()
    {
        if (bShow)
        {
            windowRect0 = GUI.Window(0, windowRect0, DoMyWindow, "Draggable Window");
        }
    }
}
