using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldCharaGenerator : MonoBehaviour
{

    private WorldGameManager gameManager;

    [SerializeField]
    private List<CharaData> charaDatasList = new List<CharaData>();

    [SerializeField, Header("キャラセット")]
    private WorldPlacementCharaSelectPopUp worldPlacementCharaSelectPopUpPrefab;　//WorldPlacementCharaSelectPopUp ????????????????????



    [SerializeField, Header("雇用契約Prefab")]
    private WorldContractSet ContractSetPrefab;

    [SerializeField, Header("雇用契約")]
    private WorldContractSet worldContractSet;

    [SerializeField, Header("スタンプPrefab")]
    private GameObject StampPrefab;

    [SerializeField, Header("スタンプ")]
    private GameObject Stamp;


    [SerializeField]
    private Transform canvasTran;                      //　PlacementCharaSelectPopUp ゲームオブジェクトの生成位置の登録用

    private WorldPlacementCharaSelectPopUp worldPlacementCharaSelectPopUp;

    [SerializeField, Header("キャラのデータリスト")]
    private List<CharaData> worldCharaDatasList = new List<CharaData>();



    /// <summary>
    /// 設定
    /// </summary>
    /// <param name="gameManager"></param>
    /// <returns></returns>
    public IEnumerator SetUpCharaGenerator(WorldGameManager gameManager)
    {
        this.gameManager = gameManager;

        // TODO ステージのデータを取得

        // キャラのデータをリスト化
        WorldCreateHaveCharaDatasList();

        // キャラ配置用のポップアップの生成
        yield return StartCoroutine(CreatePlacementCharaSelectPopUp());

    }

    /// <summary>
    /// 配置キャラ選択用ポップアップ生成
    /// </summary>
    /// <returns></returns>
    public IEnumerator CreatePlacementCharaSelectPopUp()
    {
        Debug.Log("aaaaa");

        // worldPlacementCharaSelectPopUpのポップアップを生成
        worldPlacementCharaSelectPopUp = Instantiate(worldPlacementCharaSelectPopUpPrefab, canvasTran, false);

        // worldPlacementCharaSelectPopUpのポップアップの設定。あとでキャラ設定用の情報も渡す
        worldPlacementCharaSelectPopUp.WorldSetUpPlacementCharaSelectPopUp(this, charaDatasList);

        // worldPlacementCharaSelectPopUpのポップアップを非表示にする
        worldPlacementCharaSelectPopUp.gameObject.SetActive(false);

        // ContractSetのポップアップを生成
        worldContractSet = Instantiate(ContractSetPrefab, canvasTran, false);

        // ContractSetのポップアップを非表示にする
        worldContractSet.gameObject.SetActive(false);

        // スタンプを生成
        Stamp = Instantiate(StampPrefab, canvasTran, false);

        // スタンプを非表示にする
        Stamp.gameObject.SetActive(false);

        yield return null;
    }

    /// <summary>
    /// 配置キャラ選択用のポップアップの表示
    /// </summary>
    public void ActivatePlacementCharaSelectPopUp()
    {

        // TODO ゲームの進行状態をゲーム停止に変更


        // TODO すべての敵の移動を一時停止


        // 配置キャラ選択用のポップアップの表示
        worldPlacementCharaSelectPopUp.gameObject.SetActive(true);
        worldPlacementCharaSelectPopUp.ShowPopUp();
    }

    /// <summary>
    /// 雇用契約ボタンを押した時の処理
    /// </summary>
    /// <returns></returns>
    public IEnumerator PushKoyoKeiyaku(CharaData chooseCharaData)
    {
        //コントラクトセットに情報を届ける
        worldContractSet.SetSelectCharaDetail(chooseCharaData);

        //コントラクトセットを表示する
        worldContractSet.gameObject.SetActive(true);

        //2秒停止
        yield return new WaitForSeconds(2);

        //スタンプを表示
        //TODO ①表示する際にアニメーションの追加

        Stamp.gameObject.SetActive(true);

        //2秒停止
        yield return new WaitForSeconds(2);

        //コントラクトセット、スタンプを非表示にする
        worldContractSet.gameObject.SetActive(false);
        Stamp.gameObject.SetActive(false);

        //TODO ②選択したキャラクターを雇用契約完了の画像にして、ボタンを押せないようにする
        // このスクリプトが制御したいボタンを知っているか
        // 知っている　問題なし　。
        // 知らない→ボタンの情報をここにもらう
        // 知らない→ここより前の段階でボタンの情報を知っているか調べる（他のスクリプトを確認する方法を広げてみる）



    }

    /// <summary>
    /// 配置キャラ選択用ポップアップ生成
    /// </summary>
    /// <returns></returns>
    public IEnumerator CreatePlacementCharaSelectPopUp2()
    {
        // ポップアップを生成
        worldPlacementCharaSelectPopUp = Instantiate(worldPlacementCharaSelectPopUpPrefab, canvasTran, false);

        // ポップアップの設定。あとでキャラ設定用の情報も渡す
        worldPlacementCharaSelectPopUp.WorldSetUpPlacementCharaSelectPopUp(this, charaDatasList);

        // ポップアップを非表示にする
        worldPlacementCharaSelectPopUp.gameObject.SetActive(false);
        Debug.Log("aac");
        yield return null;
    }

    /// <summary>
    /// キャラのデータをリスト化
    /// </summary>
    private void WorldCreateHaveCharaDatasList()
    {
        // CharaDataSO スクリプタブル・オブジェクト内の CharaData を１つずつリストに追加
        // TODO スクリプタブル・オブジェクトではなく、実際にプレイヤーが所持しているキャラの番号を元にキャラのデータのリストを作成
        for (int i = 0; i < DataBaseManager.instance.charaDataSO.charaDatasList.Count; i++)
        {
            charaDatasList.Add(DataBaseManager.instance.charaDataSO.charaDatasList[i]);
        }
    }

    /// <summary>
    /// 配置キャラ選択用のポップアップの非表示
    /// </summary>
    public void InactivatePlacementCharaSelectPopUp()
    {

        // 配置キャラ選択用のポップアップの非表示
        worldPlacementCharaSelectPopUp.gameObject.SetActive(false);


        // TODO ゲームオーバーやゲームクリアではない場合


        // TODO ゲームの進行状態をプレイ中に変更して、ゲーム再開


        // TODO すべての敵の移動を再開


        // TODO カレンシーの加算処理を再開

    }

    /// <summary>
    /// キャラのデータをリスト化
    /// </summary>
    private void CreateHaveCharaDatasList()
    {

        // CharaDataSO スクリプタブル・オブジェクト内の CharaData を１つずつリストに追加
        // TODO スクリプタブル・オブジェクトではなく、実際にプレイヤーが所持しているキャラの番号を元にキャラのデータのリストを作成
        for (int i = 0; i < DataBaseManager.instance.charaDataSO.charaDatasList.Count; i++)
        {
            charaDatasList.Add(DataBaseManager.instance.charaDataSO.charaDatasList[i]);
        }
    }

    /// <summary>
    /// 配置キャラ選択用のポップアップの表示
    /// </summary>
    public void ActivateContractSet()
    {

        // TODO ゲームの進行状態をゲーム停止に変更


        // TODO すべての敵の移動を一時停止
        // 配置キャラ選択用のポップアップの表示
        worldContractSet.gameObject.SetActive(true);
        //placementCharaSelectPopUp.ShowPopUp();
    }

    /// <summary>
    /// 選択したキャラを生成して配置
    /// </summary>
    /// <param name="charaData"></param>
    public void WorldCreateChooseChara(CharaData charaData)
    {

        // コスト支払い
        //GameData.instance.currency -= charaData.cost;

        // キャラをタップした位置に生成
        //CharaController chara = Instantiate(charaControllerPrefab, gridPos, Quaternion.identity);

        // 位置が左下を 0,0 としているので、中央にくるように調整
        //chara.transform.position = new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);

        // キャラの設定
        //chara.SetUpChara(charaData, gameManager);    //  <=  ☆　CharaController 側に SetUpChara メソッドがまだないので、次の手順になってからコメントアウトを解除します。

        Debug.Log(charaData.charaName);   // 選択しているキャラのデータがとどいているかを確認するためのログ表示


        // キャラを List に追加
        //gameManager.AddCharasList(chara);

    }

}