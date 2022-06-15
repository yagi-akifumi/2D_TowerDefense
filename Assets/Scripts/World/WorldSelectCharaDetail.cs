using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class WorldSelectCharaDetail : MonoBehaviour
{
    [SerializeField]
    private Button worldBtnSelectCharaDetail;

    [SerializeField]
    private Image worldImgChara;

    private PlacementCharaSelectPopUp worldPlacementCharaSelectPop;

    private CharaData worldCharaData;

    /// <summary>
    /// SelectCharaDetail の設定
    /// </summary>
    /// <param name="placementCharaSelectPop"></param>
    /// <param name="charaData"></param>
    public void SetUpSelectCharaDetail2(PlacementCharaSelectPopUp worldPlacementCharaSelectPop, CharaData worldCharaData)
    {
        //WorldCharaGeneratorをworldCharaGeneratorとし、ここで使う
        this.worldPlacementCharaSelectPop = worldPlacementCharaSelectPop;
        this.worldCharaData = worldCharaData;


        // TODO ボタンを押せない状態に切り替える


        worldImgChara.sprite = this.worldCharaData.charaSprite;


        // ボタンにメソッドを登録
        worldBtnSelectCharaDetail.onClick.AddListener(OnClickSelectCharaDetail);


        // TODO コストに応じてボタンを押せるかどうかを切り替える

    }

    /// <summary>
    /// SelectCharaDetail を押したの処理
    /// </summary>
    private void OnClickSelectCharaDetail()
    {

        // TODO アニメ演出

        // タップした SelectCharaDetail の情報をポップアップに送る
        // TODO 次の手順で、PlacementCharaSelectPop スクリプト内に SetSelectCharaDetail メソッドを作成するため、それまでコメントアウトしておいてください
        //placementCharaSelectPop.SetSelectCharaDetail(charaData);
    }
}