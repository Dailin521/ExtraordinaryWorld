using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JS_Mission : MonoBehaviour
{
    // Start is called before the first frame update
    public Button bt_js;
    public Button bt_jj;
    private void OnEnable() {
        Mouse_Pause.Instance.MouseShow();
        bt_js.onClick.AddListener(()=>{
                    GameManager.Instance.isJS_Mission=true;
                    bt_js.onClick.RemoveAllListeners();
                    bt_js.transform.parent.gameObject.SetActive(false);
                    Mouse_Pause.Instance.MouseHade();
                });
        bt_jj.onClick.AddListener(()=>{
                    bt_jj.onClick.RemoveAllListeners();
                    bt_jj.transform.parent.gameObject.SetActive(false);
                    Mouse_Pause.Instance.MouseHade();
                });
    }
}
