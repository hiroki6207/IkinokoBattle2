using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        var button = GetComponent<Button>();
        //ボタンを押下した時のリスナーを設定
        button.onClick.AddListener(() =>
        {
            //シーン遷移の際にはSceneManagerを使用する
            SceneManager.LoadScene("MainScene");
        });
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
