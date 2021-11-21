﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private GameObject enemyPrefab;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(Spawner()); //Coroutineを開始
    }

    /// <summary>
    /// 敵出現のCoroutine
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnLoop()
    {
        while(true)
        {
            //距離10のベクトル
            var distanceVector = new Vector3(10, 0);
            //プレイヤーの位置をベースにした敵の出現位置。Y軸に対して上記ベクトルをランダムに0°～360°回転させている
            var spawnPositionFromPlayer = Quaternion.Euler(0, Random.Range(0, 360f), 0) * distanceVector;
            //敵を出現させたい位置を決定
            var spawnPosition = playerStatus.transform.position + spawnPositionFromPlayer;

            //指定座標から一番近いNavMeshを決定
            NavMeshHit navMeshHit;
            if(NavMesh.SamplePosition(spawnPosition, out navMeshHit, 10, NavMesh.AllAreas))
            {
                //enemyPrefabを複製、NavMeshAgentは必ずNavMesh上に配置する
                Instantiate(enemyPrefab, navMeshHit.position, Quaternion.identity);
            }

            //10秒待つ
            yield return new WaitForSeconds(10);

            if(playerStatus.Life <= 0)
            {
                //プレイヤーが倒れたらループを抜ける
                break;
            }
        }
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
