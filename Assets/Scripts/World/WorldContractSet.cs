using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WorldContractSet : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private WorldCharaGenerator worldCharaGenerator;

    [SerializeField]
    private Image imgPickupChara;

    [SerializeField]
    private Text txtPickupCharaName;

    [SerializeField]
    private List<WorldSelectCharaDetail> selectCharaDetailsList = new List<WorldSelectCharaDetail>();  //　生成したキャラのボタンを管理する

    [SerializeField]
    private CharaData chooseCharaData;　　　　　　　　　　　　//　現在選択しているキャラの情報を管理する

    /// <summary>
    /// ポップアップの設定
    /// </summary>
    /// <param name="worldCharaGenerator"></param>
    /// <param name="haveCharaDataList"></param>
    public void WorldSetUpPlacementCharaSelectPopUp(WorldCharaGenerator worldCharaGenerator, List<CharaData> haveCharaDataList)
    {
        //WorldCharaGeneratorをworldCharaGeneratorとし、ここで使う
        this.worldCharaGenerator = worldCharaGenerator;

        // ポップアップを一度見えない状態にする
        canvasGroup.alpha = 0;

    }

    /// <summary>
    /// 選択しているキャラを配置するボタンを押した際の処理
    /// </summary>
    private void OnClickSubmitChooseChara()
    {

        // TODO コストの支払いが可能か最終確認

        // TODO 選択しているキャラの生成

        // ポップアップの非表示
        HidePopUp();
    }

    /// <summary>
    /// 配置を止めるボタンを押した際の処理
    /// </summary>
    private void OnClickClosePopUp()
    {
        Debug.Log("閉じる1");
        // ポップアップの非表示
        HidePopUp();
    }

    /// <summary>
    /// ポップアップの非表示
    /// </summary>
    private void HidePopUp()
    {

        Debug.Log("HidePopUp");
        // TODO 各キャラのボタンの制御


        // ポップアップの非表示
        canvasGroup.DOFade(0, 0.5f).OnComplete(() => worldCharaGenerator.InactivatePlacementCharaSelectPopUp());  // 次の手順でメソッドを追加するので、それまでコメントアウトしておいてください。
    }

    /// <summary>
    /// 選択しているキャラを配置するボタンを押した際の処理
    /// </summary>
    private void OnClickSubmitKoyoKeiyaku()
    {
        Debug.Log("雇用契約");
        StartCoroutine(worldCharaGenerator.PushKoyoKeiyaku());

        // TODO コストの支払いが可能か最終確認


        // TODO 選択しているキャラの生成

    }

    /// <summary>
    /// 選択された SelectCharaDetail の情報をポップアップ内のピックアップに表示する
    /// </summary>
    /// <param name="charaData"></param>
    public void SetSelectCharaDetail2(CharaData charaData)
    {
        chooseCharaData = charaData;

        // 各値の設定
        imgPickupChara.sprite = charaData.charaSprite;

        txtPickupCharaName.text = charaData.charaName;

    }
}

