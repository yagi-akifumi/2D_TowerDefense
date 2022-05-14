using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemyControllerPrefab;

    [SerializeField]
    private PathData pathData;

    private GameManager gameManager;   //　☆　追加します

    /// <summary>
    /// 敵の生成準備
    /// </summary>
    /// <returns></returns>
    public IEnumerator PreparateEnemyGenerate(GameManager gameManager)
    {
        this.gameManager = gameManager; 

        // 生成用のタイマー用意
        int timer = 0;

        // isEnemyGenetate が true の間はループする
        while (gameManager.isEnemyGenerate)
        { 
            // タイマーを加算
            timer++;

            // タイマーの値が敵の生成待機時間を超えたら
            if (timer > gameManager.generateIntervalTime)
            {    //  ☆④　条件の右辺を変更します

                // 次の生成のためにタイマーをリセット
                timer = 0;

                // 敵の生成
                GenerateEnemy();

                // 敵の生成数のカウントアップと List への追加
                gameManager.AddEnemyList();　　　　　　　//　☆追加します

                // 最大生成数を超えたら生成停止
                gameManager.JudgeGenerateEnemysEnd();  //　☆追加します

            }

            // 1フレーム中断
            yield return null;
        }

        // TODO 生成終了後の処理を記述する
    }

    /// <summary>
    /// 敵の生成
    /// </summary>
    public void GenerateEnemy()
    {
        // 指定した位置に敵を生成
        EnemyController enemyController = Instantiate(enemyControllerPrefab, pathData.generateTran.position, Quaternion.identity);

        // TODO 移動する地点を取得

        // TODO 敵キャラの初期設定を行い、移動を一時停止しておく

        // TODO 敵の移動経路のライン表示を生成の準備
    }
}