using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseBase : MonoBehaviour
{
    [SerializeField, Header("耐久値")]
    private int maxDefenseBaseDurability;

    private int defenseBaseDurability;      // 耐久力の現在値

    private GameManager gameManager;

    private UIManager uiManager;

    /// <summary>
    /// 設定
    /// </summary>
    /// <param name="gameManager"></param>
    public void SetUpDefenseBase(GameManager gameManager, int defenseBaseDurability, UIManager uiManager)
    {

        this.gameManager = gameManager;
        this.uiManager = uiManager;

        // 耐久力の最大値を決定する
        // デバッグモードを適用している場合
        if (GameData.instance.isDebug)
        {
            // GameData に設定している defenseBaseDurability を利用する
            maxDefenseBaseDurability = GameData.instance.defenseBaseDurability;
        }
        else
        {
            // デバッグモードを適用していない場合には、このクラス内での設定値を利用する
            maxDefenseBaseDurability = defenseBaseDurability;
        }
        // 耐久力の初期値の設定
        this.defenseBaseDurability = maxDefenseBaseDurability;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 侵入してきたゲームオブジェクトの確認と敵キャラの情報の取得
        if (collision.gameObject.TryGetComponent(out EnemyController enemyController))
        {
            // 敵キャラの攻撃力分だけ耐久力を減算し、耐久力の値の下限と上限内に収まるように制御した上で更新
            defenseBaseDurability = Mathf.Clamp(defenseBaseDurability - enemyController.attackPower, 0, maxDefenseBaseDurability);

            // TODO ダメージ演出生成

            // TODO ゲーム画面に耐久力の表示がある場合、その表示を更新

            // 耐久力の残りを確認
            if (defenseBaseDurability <= 0)
            {
                Debug.Log("Game Over");
                // TODO ゲームオーバー処理
            }
            // 敵の破壊
            enemyController.DestroyEnemy();
        }
    }

    // TODOダメージ演出生成用のメソッドの作成

}
