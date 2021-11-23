using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MobStatus
{
    protected override void OnDie()
    {
        base.OnDie();
        //プレイヤーが倒れたときのゲームオーバー処理
        StartCoroutine(GoToGameOverCoroutine());
    }

    private IEnumerator GoToGameOverCoroutine()
    {
        //3秒待ってからゲームオーバーシーンへ遷移
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("GameOverScene");
    }
}
//public class PlayerStatus : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}
