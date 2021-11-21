using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerStatus))]
[RequireComponent(typeof(MobAttack))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed = 3; //移動速度
    [SerializeField] private float jumpPower = 3; //ジャンプ力
    private CharacterController _characterController; //CharacterControllerのキャッシュ
    private Transform _transform; //Transformのキャッシュ
    private Vector3 _moveVelocity; //キャラクターの移動速度情報
    private PlayerStatus _status;
    private MobAttack _attack;

    //接地判定処理
    private bool IsGrounded
    {
        get
        {
            var ray = new Ray(_transform.position + new Vector3(0, 0.1f), Vector3.down);
            var raycastHits = new RaycastHit[1];
            var hitCount = Physics.RaycastNonAlloc(ray, raycastHits, 0.2f);
            return hitCount >= 1;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        //毎フレームアクセスするので、負荷を下げるためにキャッシュしておく
        _characterController = GetComponent<CharacterController>();
        _transform = transform; //Transformもキャッシュすると少しだけ負荷が下がる
        _status = GetComponent<PlayerStatus>();
        _attack = GetComponent<MobAttack>();
    }

    // Update is called once per frame
    private void Update()
    {
        //移動スピードをanimatorに反映
        animator.SetFloat("MoveSpeed", new Vector3(_moveVelocity.x, 0, _moveVelocity.z).magnitude);

        //Debug.Log(_characterController.isGrounded ? "地上にいます" : "空中です");
        Debug.Log(IsGrounded ? "地上にいます" : "空中です");

        if(CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            //Fire1ボタン(デフォルトだとマウス左クリック)で攻撃
            _attack.AttackIfPossible();
        }

        if(_status.IsMovable)
        {
            //入力軸による移動処理（慣性を無視しているので、キビキビ動く）
            _moveVelocity.x = CrossPlatformInputManager.GetAxis("Horizontal") * moveSpeed;
            _moveVelocity.z = CrossPlatformInputManager.GetAxis("Vertical") * moveSpeed;
            //移動方向に向く
            _transform.LookAt(_transform.position + new Vector3(_moveVelocity.x, 0, _moveVelocity.z));
        }
        else
        {
            _moveVelocity.x = 0;
            _moveVelocity.z = 0;
        }

        //if(_characterController.isGrounded)
        if (IsGrounded)
        {
            if(Input.GetButtonDown("Jump"))
            {
                //ジャンプ処理
                Debug.Log("ジャンプ！");
                _moveVelocity.y = jumpPower; //ジャンプの際は上方向に移動させる
            }
        }else
        {
            //重力による加速
            _moveVelocity.y += Physics.gravity.y * Time.deltaTime;
        }

        //オブジェクトを動かす
        _characterController.Move(_moveVelocity * Time.deltaTime);
    }
}
