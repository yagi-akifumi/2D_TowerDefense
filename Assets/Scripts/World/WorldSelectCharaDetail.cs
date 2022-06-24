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

    private WorldPlacementCharaSelectPopUp worldPlacementCharaSelectPop;

    private WorldContractSet worldContractSet;


    private CharaData worldCharaData;

    /// <summary>
    /// SelectCharaDetail の設定
    /// </summary>
    /// <param name="placementCharaSelectPop"></param>
    /// <param name="charaData"></param>
    public void SetUpSelectCharaDetail(WorldPlacementCharaSelectPopUp worldPlacementCharaSelectPop, CharaData worldCharaData)
    {
        //WorldCharaGeneratorをworldCharaGeneratorとし、ここで使う
        this.worldPlacementCharaSelectPop = worldPlacementCharaSelectPop;
        this.worldCharaData = worldCharaData;


        // ボタンを押せない状態に切り替える
        ChangeActivateButton(true);


        worldImgChara.sprite = this.worldCharaData.charaSprite;


        // ボタンにメソッドを登録
        worldBtnSelectCharaDetail.onClick.AddListener(OnClickSelectCharaDetail);


        // コストに応じてボタンを押せるかどうかを切り替える
        //ChangeActivateButton(JudgePermissionCost(GameData.instance.currency));
    }

    /// <summary>
    /// SelectCharaDetail を押したの処理
    /// </summary>
    private void OnClickSelectCharaDetail()
    {

        // TODO アニメ演出

        // タップした SelectCharaDetail の情報をポップアップに送る
        // TODO 次の手順で、PlacementCharaSelectPop スクリプト内に SetSelectCharaDetail メソッドを作成するため、それまでコメントアウトしておいてください
        worldPlacementCharaSelectPop.SetSelectCharaDetail(worldCharaData);
    }

    /// <summary>
    /// ボタンを押せる状態の切り替え
    /// </summary>
    public void ChangeActivateButton(bool isSwitch)
    {
        worldBtnSelectCharaDetail.interactable = isSwitch;
    }

    /// <summary>
    /// ボタンの状態の取得(今後のために実装)
    /// </summary>
    /// <returns></returns>
    public bool GetActivateButtonState()
    {
        return worldBtnSelectCharaDetail.interactable;
    }

    /// <summary>
    /// CharaData の取得(今後のために実装)
    /// </summary>
    /// <returns></returns>
    public CharaData GetCharaData()
    {
        return worldCharaData;
    }

    /// <summary>
    /// ボタンを非表示にする
    /// </summary>
    /// <returns></returns>
    public void InActive(CharaData charaData)
    {
        Debug.Log("ボタンを静止した");
        ChangeActivateButton(false);
    }


}