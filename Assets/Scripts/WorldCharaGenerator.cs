using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCharaGenerator : MonoBehaviour
{

    private GameManager gameManager;

    [SerializeField]
    private List<CharaData> charaDatasList = new List<CharaData>();

    [SerializeField]
    private WorldPlacementCharaSelectPopUp worldPlacementCharaSelectPopUpPrefab;//?WorldPlacementCharaSelectPopUp ????????????????????

    [SerializeField]
    private Transform canvasTran;                      //　PlacementCharaSelectPopUp ゲームオブジェクトの生成位置の登録用


    /// <summary>
    /// 設定
    /// </summary>
    /// <param name="gameManager"></param>
    /// <returns></returns>
    public IEnumerator WorldSetUpCharaGenerator(GameManager gameManager)
    {
        this.gameManager = gameManager;

        // TODO ステージのデータを取得

        // キャラのデータをリスト化
        CreateHaveCharaDatasList();

        // キャラ配置用のポップアップの生成
        yield return StartCoroutine(CreatePlacementCharaSelectPopUp());
    
    }

    /// <summary>
    /// 配置キャラ選択用ポップアップ生成
    /// </summary>
    /// <returns></returns>
    private IEnumerator CreatePlacementCharaSelectPopUp()
    {
        // ポップアップを生成
        worldPlacementCharaSelectPopUpPrefab = Instantiate(worldPlacementCharaSelectPopUpPrefab, canvasTran, false);

        // ポップアップの設定。あとでキャラ設定用の情報も渡す
        worldPlacementCharaSelectPopUpPrefab.WorldSetUpPlacementCharaSelectPopUp(CharaGenerator, charaDatasList);

        // ポップアップを非表示にする
        worldPlacementCharaSelectPopUpPrefab.gameObject.SetActive(false);

        yield return null;
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
}
