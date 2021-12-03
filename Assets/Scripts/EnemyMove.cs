﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyStatus))]
public class EnemyMove : MonoBehaviour
{
    //[SerializeField] private PlayerController playerController;
    [SerializeField] private LayerMask raycastLayerMask; //レイヤーマスク
    private NavMeshAgent _agent;
    private RaycastHit[] _raycastHits = new RaycastHit[10];
    private EnemyStatus _status;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>(); //NavMeshAgentを保持しておく
        _status = GetComponent<EnemyStatus>();
    }

    // Update is called once per frame
    //void Update()
    //{
        //クエリちゃんを目指して進む
        //_agent.destination = playerController.transform.position;
        //_agent.destination = GameObject.Find("Query-Chan-SD").transform.position;
        //Vector3 tmp = GameObject.Find("Slime_Blue").transform.position;
        //playerController.transform.position = new Vector3(tmp.x + 100, tmp.y, tmp.z);
    //}

    //CollisionDetectorのonTriggerStayにセットし、衝突判定を受け取るメソッド
    public void OnDetectObject(Collider collider)
    {
        if(!_status.IsMovable)
        {
            _agent.isStopped = true;
            return;
        }
        //検知したオブジェクトに「Player」のタグがついていれば、そのオブジェクトを追いかける
        if(collider.CompareTag("Player"))
        {
            var positionDiff = collider.transform.position - transform.position; //自身とプレイヤーの座標差分を計算
            var distance = positionDiff.magnitude; //プレイヤーとの距離を計算
            var direction = positionDiff.normalized; //プレイヤーへの方向

            //_raycastHitsに、ヒットしたColliderや座標情報などが格納される
            //RaycastAllとRaycastNonAllocは同等の機能だが、RaycastNonAllocだとメモリにゴミが残らないのでこちらを推奨
            var hitCount = Physics.RaycastNonAlloc(transform.position, direction, _raycastHits, distance, raycastLayerMask);
            //Debug.Log("hitCount: " + hitCount);
            if(hitCount == 0)
            {
                //本作のプレイヤーはCharacterControllerを使っていて、Colliderは使っていないのでRaycastはヒットしない
                //つまり、ヒット数が0であればプレイヤーとの間に障害物が無いということになる
                _agent.isStopped = false;
                _agent.destination = collider.transform.position;
            }else
            {
                //見失ったら停止する
                _agent.isStopped = true;
            }
        }
    }
}
