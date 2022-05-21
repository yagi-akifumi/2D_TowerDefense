using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using System;

public class EnemyController : MonoBehaviour
{
    [SerializeField, Header("移動経路の情報")]
    private PathData pathData;

    [SerializeField, Header("移動速度")]
    private float moveSpeed;

    [SerializeField, Header("最大HP")]
    private int maxHp;

    [SerializeField]
    private int hp;

    private Tween tween;

    private Vector3[] paths;

    private Animator anim;　　　　　　 // Animator コンポーネントの取得用

    private GameManager gameManager;

    public int attackPower;

    public EnemyDataSO.EnemyData enemyData;

    /// <summary>
    /// 敵の設定
    /// </summary>
    public void SetUpEnemyController(Vector3[] pathsData, GameManager gameManager, EnemyDataSO.EnemyData enemyData)
    {
        //    引数で届いた EnemyData の情報を代入して利用できる状態にする
        this.enemyData = enemyData;

        // 各数値を EnemyData の情報の値に書き換える
        moveSpeed = this.enemyData.moveSpeed;

        attackPower = this.enemyData.attackPower;

        maxHp = this.enemyData.hp;

        this.gameManager = gameManager;    //　<=　☆②　処理を追加。これで GameManager クラスが扱えるようになります。　
        hp = maxHp;

        // Animator コンポーネントを取得して anim 変数に代入
        if(TryGetComponent(out anim))
        {
            SetUpAnimation();
        }

        // 移動する地点を取得
        paths = pathsData;

        // 各地点に向けて移動。今後この処理を制御するため、Tween 型の変数に DOPath メソッドの処理を代入しておく
        tween = transform.DOPath(paths, 1000 / moveSpeed).SetEase(Ease.Linear).OnWaypointChange(ChangeAnimeDirection);    //  <=  DOPath の処理を tween 変数に代入します

        // 移動を一時停止
        PauseMove();

    }

    /// <summary>
    /// 敵の進行方向を取得して、移動アニメと同期
    /// </summary>
    private void ChangeAnimeDirection(int index)
    {

        // 次の移動先の地点がない場合には、ここで処理を終了する
        if (index >= paths.Length)
        {
            return;
        }

        // 目標の位置と現在の位置との距離と方向を取得し、正規化処理を行い、単位ベクトルとする(方向の情報は持ちつつ、距離による速度差をなくして一定値にする)
        Vector3 direction = (transform.position - paths[index]).normalized;

        //Debug.Log(direction);　　　　　　　　//　<=　☆　処理が正常に動いていることが分かっていればコメントアウトするか削除して構いません。

        // アニメーションの Palameter の値を更新し、移動アニメの BlendTree を制御して移動の方向と移動アニメを同期
        anim.SetFloat("X", direction.x);
        anim.SetFloat("Y", direction.y);
    }

    internal void SetUpEnemyController(Vector3[] paths, GameManager gameManager)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// ダメージ計算
    /// </summary>
    /// <param name="amount"></param>
    public void CulcDamage(int amount)
    {

        // Hp の値を減算した結果値を、最低値と最大値の範囲内に収まるようにして更新
        hp = Mathf.Clamp(hp -= amount, 0, maxHp);

        Debug.Log("残りHP : " + hp);

        // Hp が 0 以下になった場合
        if (hp <= 0)
        {

            // 破壊処理を実行するメソッドを呼び出す
            DestroyEnemy();
        }

        else
        {
            // 演出用のエフェクト生成
            CreateHitEffect();

            // ヒットストップ演出
            StartCoroutine(WaitMove());
        }
    }

    /// <summary>
    /// 敵破壊処理
    /// </summary>
    public void DestroyEnemy()
    {

        // Kill メソッドを実行し、tween 変数に代入されている処理(DOPath の処理)を終了する
        tween.Kill();

        // TODO SEの処理


        // 破壊時のエフェクトの生成や関連する処理
        GameObject effect = Instantiate(BattleEffectManager.instance.GetEffect(EffectType.Destroy_Enemy), transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);

        // 敵を破壊した数をカウントアップする
        // さらにこのメソッド内で、敵の情報を管理している List からこの敵の情報を削除もしてもらうために、EnemyController の情報を引数で渡している
        gameManager.CountUpDestoryEnemyCount(this);


        // 敵キャラの破壊
        Destroy(gameObject);
    }

    /// <summary>
    /// 移動を一時停止
    /// </summary>
    public void PauseMove()
    {
        tween.Pause();
    }

    /// <summary>
    /// 移動を開始
    /// </summary>
    public void ResumeMove()
    {
        tween.Play();
    }

    /// <summary>
    /// ヒットストップ演出
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitMove()
    {

        tween.timeScale = 0.05f;

        yield return new WaitForSeconds(0.5f);

        tween.timeScale = 1.0f;
    }

    /// <summary>
    /// AnimatorController を AnimatorOverrideController を利用して変更
    /// </summary>
    private void SetUpAnimation()
    {

        // このエネミーの EnemyData 内にアニメーション用のデータがあるか確認する
        if (enemyData.enemyOverrideController != null)
        {

            // アニメーションのデータがある場合には、アニメーションを上書きする
            anim.runtimeAnimatorController = enemyData.enemyOverrideController;
        }
    }

    /// <summary>
    /// ヒットエフェクト生成
    /// </summary>
    private void CreateHitEffect()
    {
        // TODO SE

        // 被ダメージ時のヒットエフェクト生成
        GameObject effect = Instantiate(BattleEffectManager.instance.GetEffect(EffectType.Hit_Enemy), transform.position, Quaternion.identity);
        // エフェクトを破壊
        Destroy(effect, 1.5f);
    }

}