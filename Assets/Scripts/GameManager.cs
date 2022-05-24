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

    private int destroyEnemyCount;                //  敵を破壊した数のカウント用

    public UIManager uiManager;

    [SerializeField]
    private List<CharaController> charasList = new List<CharaController>();    // 配置したキャラの情報を一元化して管理するための変数。CharaController 型で扱う

    [SerializeField]
    private DefenseBase defenseBase;          // 生成した防衛拠点の情報を代入するための変数

    [SerializeField]　　　　　　　　　　　　　// デバッグのために SerializeField 属性を利用してインスペクターで確認できるようにしている
    private MapInfo currentMapInfo;　　　　　 // 生成したステージのゲームオブジェクト(MapInfo クラスのアタッチされている MainMap ゲームオブジェクト)を代入するための変数

    [SerializeField]
    private DefenseBase defenseBasePrefab;　　// 防衛拠点のプレファブをアサインするための変数

    [SerializeField]
    private StageData currentStageData;    // 今回のバトルで使用するステージのデータ情報

    IEnumerator Start()
    {

        // ゲームの進行状態を準備中に設定
        SetGameState(GameState.Preparate);

        // ゲームデータを初期化
        RefreshGameData()

        // ステージの設定 + ステージごとの PathData を設定
        SetUpStageData();

        // キャラ配置用ポップアップの生成と設定
        StartCoroutine(charaGenerator.SetUpCharaGenerator(this));

        // TODO 拠点の設定
        defenseBase.SetUpDefenseBase(this, currentStageData.defenseBaseDurability, uiManager);

        // オープニング演出再生(自分で用意しているメソッドがあればそちらを使いましょう)
        //yield return StartCoroutine(uiManager.Opening());

        isEnemyGenerate = true;

        // ゲームの進行状態をプレイ中に変更
        SetGameState(GameState.Play);

        // 敵の生成準備開始
        StartCoroutine(enemyGenerator.PreparateEnemyGenerate(this, currentStageData));　　//　<=　第２引数を追加

        // カレンシーの自動獲得処理の開始
        StartCoroutine(TimeToCurrency());

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

            // ゲームクリアの処理を追加
            StartCoroutine(GameClearAndResult());

        }
    }

    /// <summary>
    /// 時間の経過に応じてカレンシーを加算
    /// </summary>
    /// <returns></returns>
    public IEnumerator TimeToCurrency()
    {
        int timer = 0;

        // ゲームプレイ中のみ加算
        while (currentGameState == GameState.Play)
        {
            timer++;

            // 規定の時間が経過し、カレンシーが最大値でなければ
            if (timer > GameData.instance.currencyIntervalTime && GameData.instance.currency < GameData.instance.maxCurrency)
            {
                timer = 0;

                // 最大値以下になるようにカレンシーを加算
                GameData.instance.currency = Mathf.Clamp(GameData.instance.currency += GameData.instance.addCurrencyPoint, 0, GameData.instance.maxCurrency);

                // カレンシーの画面表示を更新
                uiManager.UpdateDisplayCurrency();
            }

            yield return null;
        }
    }

    /// <summary>
    /// 選択したキャラの情報を List に追加
    /// </summary>
    public void AddCharasList(CharaController chara)
    {
        charasList.Add(chara);
    }

    /// <summary>
    /// 選択したキャラを破棄し、情報を List から削除
    /// </summary>
    /// <param name="chara"></param>
    public void RemoveCharasList(CharaController chara)
    {
        Destroy(chara.gameObject);
        charasList.Remove(chara);
    }

    /// <summary>
    /// 現在の配置しているキャラの数の取得
    /// </summary>
    /// <returns></returns>
    public int GetPlacementCharaCount()
    {
        return charasList.Count;
    }

    /// <summary>
    /// 配置解除を選択するポップアップ作成の準備(CharaController から呼び出される)
    /// </summary>
    /// <param name="chara"></param>
    public void PreparateCreateReturnCharaPopUp(CharaController chara)
    {

        // ゲームの進行状態をゲーム停止に変更
        SetGameState(GameState.Stop);

        // すべての敵の移動を一時停止
        PauseEnemies();

        // 配置解除を選択するポップアップを作成
        uiManager.CreateReturnCharaPopUp(chara, this);
    }

    /// <summary>
    /// 選択したキャラの配置解除の確認(ReturnSelectCharaPopUp から呼び出される)
    /// </summary>
    /// <param name="isReturnChara"></param>
    /// <param name="chara"></param>
    public void JudgeReturnChara(bool isReturnChara, CharaController chara)
    {

        // キャラの配置を解除する場合
        if (isReturnChara)
        {

            // 選択したキャラを破棄し、情報を List から削除
            RemoveCharasList(chara);
        }

        //  ゲームの進行状態をプレイ中に変更して、ゲーム再開
        SetGameState(GameState.Play);

        // すべての敵の移動を再開
        ResumeEnemies();

        // カレンシーの加算処理を再開
        StartCoroutine(TimeToCurrency());
    }

    /// <summary>
    /// ステージデータの設定
    /// </summary>
    private void SetUpStageData()
    {

        // GameData の stageNo から StageData を取得
        currentStageData = DataBaseManager.instance.stageDataSO.stageDatasList[GameData.instance.stageNo];

        // 各情報を StageData クラスを参照して設定
        generateIntervalTime = currentStageData.generateIntervalTime;
        maxEnemyCount = currentStageData.mapInfo.appearEnemyInfos.Length;

        // ステージ用のマップと防衛拠点の生成
        currentMapInfo = Instantiate(currentStageData.mapInfo);
        defenseBase = Instantiate(defenseBasePrefab, currentMapInfo.GetDefenseBaseTran());

        // PathDatas の移動経路情報を StageData クラスを参照して設定
        PathData[] pathDatas = new PathData[currentStageData.mapInfo.appearEnemyInfos.Length];
        for (int i = 0; i < currentStageData.mapInfo.appearEnemyInfos.Length; i++)
        {
            pathDatas[i] = currentStageData.mapInfo.appearEnemyInfos[i].enemyPathData;
        }

        // 移動経路の情報を引数で渡して、EnemyGenerator クラスの設定を行う
        enemyGenerator.SetUpPathDatas(pathDatas);

        // TODO 他にもあれば追加

    }

    /// <summary>
    /// ゲームクリアと報酬処理
    /// </summary>
    private IEnumerator GameClearAndResult()
    {

        // ゲーム終了
        GameUpToCommon();

        // TODO ゲームクリア演出(文字)
        //yield return StartCoroutine(uiManager.CreateGameClearSet());

        // TODO ロゴで演出
        //yield return StartCoroutine(uiManager.GameClear());

        // クリアボーナスの獲得
        GameData.instance.totalClearPoint += currentStageData.clearPoint;

        // 次のステージの番号を設定
        GameData.instance.stageNo++;   //  <=　この部分は、currentStageData.stageNo ++ でも構いません。その場合、下の条件式を修正してください。

        // 次のステージが未クリアである場合
        if (!GameData.instance.clearedStageNosList.Contains(GameData.instance.stageNo))
        {

            // 次のステージを登録してステージシーンで表示できるようにする(=> すでに List に登録してある場合には重複して登録しない)
            GameData.instance.clearedStageNosList.Add(GameData.instance.stageNo);
        }

        // シーン遷移
        SceneStateManager.instance.PreparateNextScene(SceneType.Main);
    }

    /// <summary>
    /// ゲーム終了時の共通処理
    /// </summary>
    private void GameUpToCommon()
    {

        // ゲームの進行状態をゲーム終了に変更
        SetGameState(GameState.GameUp);

        // キャラ配置用のポップアップが開いている場合には破棄
        charaGenerator.InactivatePlacementCharaSelectPopUp();

        // TODO ゲーム終了時に、ゲームクリアとゲームオーバーの共通する処理を追加

    }

    /// <summary>
    /// ゲームオーバー処理
    /// </summary>
    public IEnumerator GameOver()
    {

        // ゲーム終了処理
        GameUpToCommon();

        // TODO ゲームオーバー表示
        //uiManager.CreateGameOverSet();

        // TODO ゲームオーバー時の処理を追加

        yield return new WaitForSeconds(3.0f);

        // シーン遷移
        SceneStateManager.instance.PreparateNextScene(SceneType.Main);
    }

    /// <summary>
    /// ゲームデータを初期化
    /// </summary>
    private void RefreshGameData()
    {
        // TODO デバッグ用の処理やバトル開始時に初期化したい処理を記述する

    }

}