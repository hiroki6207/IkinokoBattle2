using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Mob（動くオブジェクト、MovingObjectの略）の状態管理スクリプト
public class MobStatus : MonoBehaviour
{

    //状態の定義
    protected enum StateEnum
    {
        Normal, //通常
        Attack, //攻撃中
        Die //死亡
    }

    //移動可能かどうか
    public bool IsMovable => StateEnum.Normal == _state;

    //攻撃可能かどうか
    public bool IsAttackable => StateEnum.Normal == _state;

    //ライフ最大値を返します
    public float LifeMax => lifeMax;

    //ライフの値を返します
    public float Life => _life;

    [SerializeField] private float lifeMax = 10; //ライフ最大値
    protected Animator _animator;
    protected StateEnum _state = StateEnum.Normal; //Mob状態
    private float _life; //現在のライフ値(ヒットポイント)

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _life = lifeMax; //初期状態はライフ満タン
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    //キャラクターが倒れた時の処理を記述します
    protected virtual void OnDie()
    {

    }

    //指定値のダメージを受けます
    /// <param name="damage"></param>
    public void Damage(int damage)
    {
        if (_state == StateEnum.Die) return;

        _life -= damage;
        if (_life > 0) return;

        _state = StateEnum.Die;
        _animator.SetTrigger("Die");

        OnDie();
    }

    //可能であれば攻撃中の状態に遷移します
    public void GoToAttackStateIfPossible()
    {
        if (!IsAttackable) return;

        _state = StateEnum.Attack;
        _animator.SetTrigger("Attack");
    }

    //可能であればNormalの状態に遷移します
    public void GoToNormalStateIfPossible()
    {
        if (_state == StateEnum.Die) return;

        _state = StateEnum.Normal;
    }
}
