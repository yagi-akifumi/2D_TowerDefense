using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private EnemyGenerator enemyGenerator;

    [SerializeField]
    private CharaGenerator charaGenerator;

    public bool isEnemyGenerate;

    public int generateIntervalTime;

    public int generateEnemyCount;

    public int maxEnemyCount;

    /// <summary>
    /// ゲームの状態
    /// </summary>
    public enum GameState
    {
        Preparate,       // ゲーム開始前の準備中
        Play,            // ゲームプレイ中
        Stop,            // ゲーム内の処理の一時停止中
        GameUp           // ゲーム終了(ゲームオーバー、クリア両方)
    }

    public GameState currentGameState;    // 現在の GameState の状態。上記の GameState の列挙子が１つだけ代入されるので、他の GameState と競合しない

    [SerializeField]
    private List<EnemyController> enemiesList = new List<EnemyController>();　　　//　敵の情報を一元化して管理するための変数。EnemyController 型で扱う

    [SerializeField]
    private int destroyEnemyCount;                //  敵を破壊した数のカウント用

    void Start()
    {

        // ゲームの進行状態を準備中に設定
        SetGameState(GameState.Preparate);

        // TODO ゲームデータを初期化

        // TODO ステージの設定 + ステージごとの PathData を設定

        // キャラ配置用ポップアップの生成と設定
        StartCoroutine(charaGenerator.SetUpCharaGenerator(this));

        // TODO 拠点の設定

        // TODO オープニング演出再生

        isEnemyGenerate = true;

        // ゲームの進行状態をプレイ中に変更
        SetGameState(GameState.Play);

        // 敵の生成準備開始
        StartCoroutine(enemyGenerator.PreparateEnemyGenerate(this));

        // TODO カレンシーの自動獲得処理の開始

    }

    /// <summary>
    /// 敵の情報を List に追加
    /// </summary>
    /// <param name="enemy"></param>
    public void AddEnemyList(EnemyController enemy)
    {　　　　//　☆①　敵の情報を List に追加する際に、引数を追加

        //　☆②　敵の情報を List に追加
        enemiesList.Add(enemy);

        // 敵の生成数をカウントアップ
        generateEnemyCount++;
    }

    /// <summary>
    /// 敵の生成を停止するか判定
    /// </summary>
    public void JudgeGenerateEnemysEnd()
    {
        if (generateEnemyCount >= maxEnemyCount)
        {
            isEnemyGenerate = false;
        }
    }

    /// <summary>
    /// GameState の変更
    /// </summary>
    /// <param name="nextGameState"></param>
    public void SetGameState(GameState nextGameState)
    {
        currentGameState = nextGameState;
    }

    /// <summary>
    /// すべての敵の移動を一時停止
    /// </summary>
    public void PauseEnemies()
    {
        for (int i = 0; i < enemiesList.Count; i++)
        {
            enemiesList[i].PauseMove();
        }
    }

    /// <summary>
    /// すべての敵の移動を再開
    /// </summary>
    public void ResumeEnemies()
    {
        for (int i = 0; i < enemiesList.Count; i++)
        {
            enemiesList[i].ResumeMove();
        }
    }

    /// <summary>
    /// 敵の情報を List から削除
    /// </summary>
    /// <param name="removeEnemy"></param>
    public void RemoveEnemyList(EnemyController removeEnemy)
    {
        enemiesList.Remove(removeEnemy);
    }

    /// <summary>
    /// 破壊した敵の数をカウント(このメソッドを外部のクラスから実行してもらう)
    /// </summary>
    public void CountUpDestoryEnemyCount(EnemyController enemyController)
    {

        // List から破壊された敵の情報を削除
        RemoveEnemyList(enemyController);

        // 敵を破壊した数を加算
        destroyEnemyCount++;

        Debug.Log("破壊した敵の数 : " + destroyEnemyCount);

        // ゲームクリア判定
        JudgeGameClear();
    }


    /// <summary>
    /// ゲームクリア判定
    /// </summary>
    public void JudgeGameClear()
    {
        // 生成数を超えているか
        if (destroyEnemyCount >= maxEnemyCount)
        {

            Debug.Log("ゲームクリア");

            // TODO ゲームクリアの処理を追加

        }
    }

}