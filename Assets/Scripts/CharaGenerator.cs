using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharaGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject charaPrefab;

    [SerializeField]
    private Grid grid;         // Base 側の Grid を指定する 

    [SerializeField]
    private Tilemap tilemaps;   // Walk 側の Tilemap を指定する

    [SerializeField]
    private PlacementCharaSelectPopUp placementCharaSelectPopUpPrefab;　　　//　PlacementCharaSelectPopUp プレファブゲームオブジェクトをアサイン用

    [SerializeField]
    private Transform canvasTran;                      //　PlacementCharaSelectPopUp ゲームオブジェクトの生成位置の登録用

    [SerializeField, Header("キャラのデータリスト")]
    private List<CharaData> charaDatasList = new List<CharaData>();


    private PlacementCharaSelectPopUp placementCharaSelectPopUp;　　　　　　//　生成された PlacementCharaSelectPopUp ゲームオブジェクトを代入するための変数

    private GameManager gameManager;

    private Vector3Int gridPos;


    void Update()
    {
        // TODO 配置できる最大キャラ数に達している場合には配置できない

        // 画面をタップ(マウスクリック)し、かつ、配置キャラポップアップが非表示状態なら
        if (Input.GetMouseButtonDown(0) && !placementCharaSelectPopUp.gameObject.activeSelf)
        {

            // タップ(マウスクリック)の位置を取得してワールド座標に変換し、それをさらにタイルのセル座標に変換
            gridPos = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            // タップした位置のタイルのコライダーの情報を確認し、それが None であるなら
            if (tilemaps.GetColliderType(gridPos) == Tile.ColliderType.None)
            {

                // キャラ生成処理をメソッド化
                //CreateChara(gridPos);

                // 配置キャラ選択用ポップアップの表示
                ActivatePlacementCharaSelectPopUp();
            }
        }

    }

    /// <summary>
    /// キャラ生成
    /// </summary>
    /// <param name="gridPos"></param>
    private void CreateChara(Vector3Int gridPos)
    {

        // タップした位置にキャラを生成して配置
        GameObject chara = Instantiate(charaPrefab, gridPos, Quaternion.identity);

        // キャラの位置がタイルの左下を 0,0 として生成しているので、タイルの中央にくるように位置を調整
        chara.transform.position = new Vector2(chara.transform.position.x + 0.5f, chara.transform.position.y + 0.5f);
    }
    /// <summary>
    /// 設定
    /// </summary>
    /// <param name="gameManager"></param>
    /// <returns></returns>
    public IEnumerator SetUpCharaGenerator(GameManager gameManager)
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
        placementCharaSelectPopUp = Instantiate(placementCharaSelectPopUpPrefab, canvasTran, false);

        // ポップアップの設定。キャラ設定用の情報も渡す
        placementCharaSelectPopUp.SetUpPlacementCharaSelectPopUp(this, charaDatasList);　　//　<=　☆　第2引数を追加します


        // ポップアップを非表示にする
        placementCharaSelectPopUp.gameObject.SetActive(false);

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
        Debug.Log("true");
        placementCharaSelectPopUp.gameObject.SetActive(true);
        placementCharaSelectPopUp.ShowPopUp();
    }

    /// <summary>
    /// 配置キャラ選択用のポップアップの非表示
    /// </summary>
    public void InactivatePlacementCharaSelectPopUp()
    {

        // 配置キャラ選択用のポップアップの非表示
        Debug.Log("false");
        placementCharaSelectPopUp.gameObject.SetActive(false);


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
}