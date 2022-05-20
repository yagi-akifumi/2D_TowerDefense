using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
    [SerializeField, Header("攻撃力")]
    private int attackPower = 1;

    [SerializeField, Header("攻撃するまでの待機時間")]
    private float intervalAttackTime = 60.0f;

    [SerializeField]
    private bool isAttack;

    [SerializeField]
    private EnemyController enemy;

    [SerializeField]
    private int attackCount = 3;     // TODO 現在の攻撃回数の残り。あとで CharaData クラスの値を反映させる

    [SerializeField]
    private UnityEngine.UI.Text txtAttackCount;

    [SerializeField]
    private BoxCollider2D attackRangeArea;

    [SerializeField]
    private CharaData charaData;

    private GameManager gameManager;

    private Animator anim;

    private string overrideClipName = "Chara_4"; //   　　<=  ☆　代入する文字列は自分のプロジェクトの Animationビュー の Motion に登録されている AnimationClip の名前を登録する

    private AnimatorOverrideController overrideController;

    private void OnTriggerStay2D(Collider2D collision)
    {

        // 攻撃中ではない場合で、かつ、敵の情報を未取得である場合
        if (!isAttack && !enemy)
        {

            Debug.Log("敵発見");

            // 敵の情報(EnemyController)を取得する
            if (collision.gameObject.TryGetComponent(out enemy))
            {

                // 情報を取得できたら、攻撃状態にする
                isAttack = true;

                // 攻撃の準備に入る
                StartCoroutine(PrepareteAttack());
            }
        }
    }

    /// <summary>
    /// 攻撃準備
    /// </summary>
    /// <returns></returns>
    public IEnumerator PrepareteAttack()
    {

        Debug.Log("攻撃準備開始");

        int timer = 0;

        // 攻撃中の間だけループ処理を繰り返す
        while (isAttack)
        {


            // ゲームプレイ中のみ攻撃する
            if (gameManager.currentGameState == GameManager.GameState.Play)
            {
                timer++;

                // 攻撃のための待機時間が経過したら    
                if (timer > intervalAttackTime)
                {

                    // 次の攻撃に備えて、待機時間のタイマーをリセット
                    timer = 0;

                    // 攻撃
                    Attack();

                    // 攻撃回数関連の処理をここに記述する            
                    attackCount--;
                    UpdateDisplayAttackCount();
                    Debug.Log(attackCount);

                    // 攻撃回数がなくなったら
                    if (attackCount <= 0)
                    {

                        // キャラ破壊
                        Destroy(gameObject);

                        // キャラのリストから情報を削除
                        gameManager.RemoveCharasList(this);
                    }
                }
            }

            // １フレーム処理を中断する(この処理を書き忘れると無限ループになり、Unity エディターが動かなくなって再起動することになります。注意！)
            yield return null;
        }
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    private void Attack()
    {

        Debug.Log("攻撃");

        // TODO キャラの上に攻撃エフェクトを生成

        // 敵キャラ側に用意したダメージ計算用のメソッドを呼び出して、敵にダメージを与える
        enemy.CulcDamage(attackPower);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {

            Debug.Log("敵なし");

            isAttack = false;
            enemy = null;
        }
    }

    /// <summary>
    /// 残り攻撃回数の表示更新
    /// </summary>
    private void UpdateDisplayAttackCount()
    {
        txtAttackCount.text = attackCount.ToString();
    }

    /// <summary>
    /// キャラの設定
    /// </summary>
    /// <param name="charaData"></param>
    /// <param name="gameManager"></param>
    public void SetUpChara(CharaData charaData, GameManager gameManager)
    {
        this.charaData = charaData;
        Debug.Log(this.charaData.charaNo);

        this.gameManager = gameManager;

        // 各値を CharaData から取得して設定
        attackPower = this.charaData.attackPower;

        intervalAttackTime = this.charaData.intervalAttackTime;

        // DataBaseManager に登録されている AttackRangeSizeSO スクリプタブル・オブジェクトのデータと照合を行い、CharaData の AttackRangeType の情報を元に Size を設定
        attackRangeArea.size = DataBaseManager.instance.GetAttackRangeSize(this.charaData.attackRange);
        attackCount = this.charaData.maxAttackCount;

        // 残りの攻撃回数の表示更新
        UpdateDisplayAttackCount();

        // キャラごとの AnimationClip を設定
        SetUpAnimation();

    }

    /// <summary>
    /// Motion に登録されている AnimationClip を変更
    /// </summary>
    private void SetUpAnimation()
    {
        if (TryGetComponent(out anim))
        {
            overrideController = new AnimatorOverrideController();

            overrideController.runtimeAnimatorController = anim.runtimeAnimatorController;
            anim.runtimeAnimatorController = overrideController;

            AnimatorStateInfo[] layerInfo = new AnimatorStateInfo[anim.layerCount];

            for (int i = 0; i < anim.layerCount; i++)
            {
                layerInfo[i] = anim.GetCurrentAnimatorStateInfo(i);
            }

            overrideController[overrideClipName] = this.charaData.charaAnim;

            anim.runtimeAnimatorController = overrideController;

            anim.Update(0.0f);

            for (int i = 0; i < anim.layerCount; i++)
            {
                anim.Play(layerInfo[i].fullPathHash, i, layerInfo[i].normalizedTime);
            }
        }
    }

    /// <summary>
    /// キャラをタップした際の処理(EventTrigger に登録するメソッド)
    /// </summary>
    public void OnClickChara()
    {

        // GameManager を経由して、ゲームの進行状態を切り替えつつ、UIManager へ処理をつなげてもらうためのメソッド
        gameManager.PreparateCreateReturnCharaPopUp(this);
    }

}