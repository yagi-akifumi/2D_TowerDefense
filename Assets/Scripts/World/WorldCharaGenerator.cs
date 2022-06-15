using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCharaGenerator : MonoBehaviour
{

    private WorldGameManager gameManager;

    [SerializeField]
    private List<CharaData> charaDatasList = new List<CharaData>();

    [SerializeField, Header("キャラセット")]
    private WorldPlacementCharaSelectPopUp worldPlacementCharaSelectPopUpPrefab;　//WorldPlacementCharaSelectPopUp ????????????????????



    [SerializeField, Header("雇用契約")]
    private GameObject ContractSet;

    [SerializeField, Header("スタンプ")]
    private GameObject BtnSubmitContractStamp;

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

        // ポップアップを生成
        worldPlacementCharaSelectPopUp = Instantiate(worldPlacementCharaSelectPopUpPrefab, canvasTran, false);

        // ポップアップの設定。あとでキャラ設定用の情報も渡す
        worldPlacementCharaSelectPopUp.WorldSetUpPlacementCharaSelectPopUp(this, charaDatasList);

        // ポップアップを非表示にする
        worldPlacementCharaSelectPopUp.gameObject.SetActive(false);

        // ポップアップを生成
        ContractSet = Instantiate(ContractSet, canvasTran, false);

        // ポップアップを非表示にする
        ContractSet.gameObject.SetActive(false);

        // スタンプを生成
        BtnSubmitContractStamp = Instantiate(BtnSubmitContractStamp, canvasTran, false);

        // スタンプを非表示にする
        BtnSubmitContractStamp.gameObject.SetActive(false);

        yield return null;
    }

    /// <summary>
    /// 雇用契約ボタンを押した時の処理
    /// </summary>
    /// <returns></returns>
    public IEnumerator PushKoyoKeiyaku()
    {
        //コントラクトセットを表示する
        ContractSet.gameObject.SetActive(true);

        //3秒停止
        yield return new WaitForSeconds(3);

        //スタンプを表示
        BtnSubmitContractStamp.gameObject.SetActive(true);

        //3秒停止
        yield return new WaitForSeconds(3);

        //コントラクトセット、スタンプを非表示にする
        ContractSet.gameObject.SetActive(false);
        BtnSubmitContractStamp.gameObject.SetActive(false);

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
    /// 配置キャラ選択用のポップアップの表示
    /// </summary>
    public void ActivatePlacementCharaSelectPopUp()
    {

        // TODO ゲームの進行状態をゲーム停止に変更


        // TODO すべての敵の移動を一時停止
        // 配置キャラ選択用のポップアップの表示
        worldPlacementCharaSelectPopUp.gameObject.SetActive(true);
        //placementCharaSelectPopUp.ShowPopUp();
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
    /// 配置キャラ選択用のポップアップの表示
    /// </summary>
    public void ActivateContractSet()
    {

        // TODO ゲームの進行状態をゲーム停止に変更


        // TODO すべての敵の移動を一時停止
        // 配置キャラ選択用のポップアップの表示
        ContractSet.gameObject.SetActive(true);
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