using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueryChanSD_Scripts : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Shift+Spaceを押しました！");
        }
        //Debug.Log("マウス座標：" + Input.mousePosition);
        //Debug.Log("マウスホイールのスクロール量：" + Input.mouseScrollDelta);
    }
}
