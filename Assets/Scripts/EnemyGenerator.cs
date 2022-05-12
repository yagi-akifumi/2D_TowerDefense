using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemyControllerPrefab;

    [SerializeField]
    private PathData[] pathDatas;

    [SerializeField]
    private DrawPathLine pathLinePrefab;

    private GameManager gameManager;


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

            if (this.gameManager.currentGameState == GameManager.GameState.Play)
            {　　　//　<=　☆①　GameState が Play 以外では生成を一時停止

                // タイマーを加算
                timer++;

                // タイマーの値が敵の生成待機時間を超えたら
                if (timer > gameManager.generateIntervalTime)
                {

                    // 次の生成のためにタイマーをリセット
                    timer = 0;

                    //GenerateEnemy();                              //  <=  ☆②　コメントアウトします

                    // 敵の生成し、敵の生成数のカウントアップと List への追加
                    gameManager.AddEnemyList(GenerateEnemy());   //　<=　☆③　引数に GenerateEnemy メソッドを設定します

                    // 最大生成数を超えたら生成停止
                    gameManager.JudgeGenerateEnemysEnd();
                }
            }                                                   //  <=  ☆④　if 文終了用のブロックを追加します



            // 1フレーム中断
            yield return null;
        }

        // TODO 生成終了後の処理を記述する

    }

    /// <summary>
    /// 敵の生成
    /// </summary>
    /// <param name="generateNo"></param>
    /// <returns></returns>
    public EnemyController GenerateEnemy(int generateNo = 0)
    {　　//　<=　☆①　戻り値を変更し、引数を追加します
        // ランダムな値を配列の最大要素数内で取得
        int randomValue = Random.Range(0, pathDatas.Length);　　　//　<=　☆①　処理を追加します

        // 指定した位置に敵を生成
        EnemyController enemyController = Instantiate(enemyControllerPrefab, pathDatas[randomValue].generateTran.position, Quaternion.identity);

        // 移動する地点を取得(<=　いままでEnemyController スクリプト内で行っていた処理をこちらに移動します)
        Vector3[] paths = pathDatas[randomValue].pathTranArray.Select(x => x.position).ToArray();

        // 敵キャラの初期設定を行い、移動を一時停止しておく
        enemyController.SetUpEnemyController(paths);

        // 敵の移動経路のライン表示を生成の準備
        StartCoroutine(PreparateCreatePathLine(paths, enemyController));

        return enemyController;　　　//　<=　☆②　戻り値の設定をします

    }

    /// <summary>
    /// ライン生成の準備
    /// </summary>
    /// <param name="paths"></param>
    /// <returns></returns>
    private IEnumerator PreparateCreatePathLine(Vector3[] paths, EnemyController enemyController)
    {
        // ラインの生成と削除。この処理が終了するまでは、この処理より下の処理は実行されない
        yield return StartCoroutine(CreatePathLine(paths));

        // ☆　ポイントです。この処理が何故必要なのかを考えてみましょう。
        yield return new WaitUntil(() => gameManager.currentGameState == GameManager.GameState.Play);

        // 敵の移動を再開
        enemyController.ResumeMove();
    }


    /// <summary>
    /// 移動経路用のラインの生成と破棄
    /// </summary>
    private IEnumerator CreatePathLine(Vector3[] paths)
    {

        // List の宣言と初期化
        List<DrawPathLine> drawPathLinesList = new List<DrawPathLine>();

        // １つの Path ごとに１つずつ順番にラインを生成
        for (int i = 0; i < paths.Length - 1; i++)
        {
            DrawPathLine drawPathLine = Instantiate(pathLinePrefab, transform.position, Quaternion.identity);

            Vector3[] drawPaths = new Vector3[2] { paths[i], paths[i + 1] };

            drawPathLine.CreatePathLine(drawPaths);

            drawPathLinesList.Add(drawPathLine);

            yield return new WaitForSeconds(0.1f);
        }

        // すべてのラインを生成して待機
        yield return new WaitForSeconds(0.5f);

        // １つのラインずつ順番に削除する
        for (int i = 0; i < drawPathLinesList.Count; i++)
        {
            Destroy(drawPathLinesList[i].gameObject);

            yield return new WaitForSeconds(0.1f);
        }
    }
}