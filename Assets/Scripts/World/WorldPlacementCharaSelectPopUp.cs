using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
using System;

public class WorldPlacementCharaSelectPopUp : MonoBehaviour
{
    [SerializeField]
    private Button btnClosePopUp;

    [SerializeField]
    private Button btnKoyoKeiyaku;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private WorldCharaGenerator worldCharaGenerator;

    [SerializeField]
    private Image imgPickupChara;

    [SerializeField]
    private Text txtPickupCharaName;

    [SerializeField]
    private Text txtPickupCharaAttackPower;

    [SerializeField]
    private Text txtPickupCharaAttackRangeType;

    [SerializeField]
    private Text txtPickupCharaCost;

    [SerializeField]
    private Text txtPickupCharaMaxAttackCount;

    [SerializeField]
    private Text txtDiscription;

    [SerializeField]
    private WorldSelectCharaDetail selectCharaDetailPrefab;　　　　//　キャラのボタン用のプレファブをアサインする

    [SerializeField]
    private WorldSelectCharaDetail worldselectCharaDetail;　　　　//　キャラのボタン用のプレファブをアサインする

    [SerializeField]
    private Transform selectCharaDetailTran;　　　　　　　　　//　キャラのボタンを生成する位置をアサインする

    [SerializeField]
    private List<WorldSelectCharaDetail> selectCharaDetailsList = new List<WorldSelectCharaDetail>();  //　生成したキャラのボタンを管理する

    [SerializeField]
    private CharaData chooseCharaData;　　　　　　　　　　　　//　現在選択しているキャラの情報を管理する

    public CharaDataSO charaDataSO;

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

        // 徐々にポップアップを表示する
        ShowPopUp();

        // 各ボタンを押せる状態にする
        SwithcActivateButtons(false);

        // スクリプタブル・オブジェクトに登録されているキャラ分(引数で受け取った情報)を利用して
        for (int i = 0; i < haveCharaDataList.Count; i++)
        {

            // ボタンのゲームオブジェクトを生成
            WorldSelectCharaDetail selectCharaDetail = Instantiate(selectCharaDetailPrefab, selectCharaDetailTran, false);

            // ボタンのゲームオブジェクトの設定(CharaData を設定する)
            selectCharaDetail.SetUpSelectCharaDetail(this, haveCharaDataList[i]);

            // List に追加
            selectCharaDetailsList.Add(selectCharaDetail);

            // 最初に生成したボタンの場合
            if (i == 0)
            {
                // 選択しているキャラとして初期値に設定
                SetSelectCharaDetail(haveCharaDataList[i]);
            }



        }

        // 各ボタンにメソッドを登録
        btnKoyoKeiyaku.onClick.AddListener(OnClickSubmitKoyoKeiyaku);

        btnClosePopUp.onClick.AddListener(OnClickClosePopUp);

        // 各ボタンを押せる状態にする
        SwithcActivateButtons(true);
    }

    /// <summary>
    /// 各ボタンのアクティブ状態の切り替え
    /// </summary>
    /// <param name="isSwitch"></param>
    public void SwithcActivateButtons(bool isSwitch)
    {
        btnKoyoKeiyaku.interactable = isSwitch;
        btnClosePopUp.interactable = isSwitch;
    }

    /// <summary>
    /// ポップアップの表示
    /// </summary>
    public void ShowPopUp()
    {
        // TODO 各キャラのボタンの制御

        // ポップアップの表示
        canvasGroup.DOFade(1.0f, 0.5f);
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
        StartCoroutine(worldCharaGenerator.PushKoyoKeiyaku(chooseCharaData));
        WorldSelectCharaDetail chooseCharaBtn = selectCharaDetailsList.Find(x => x.GetCharaData() == chooseCharaData);
        // ボタンを非活性化して押せない状態にする
        chooseCharaBtn.ChangeActivateButton(false);

        // TODO コストの支払いが可能か最終確認


        // TODO 選択しているキャラの生成

    }

    /// <summary>
    /// 選択された SelectCharaDetail の情報をポップアップ内のピックアップに表示する
    /// </summary>
    /// <param name="charaData"></param>
    public void SetSelectCharaDetail(CharaData charaData)
    {
        chooseCharaData = charaData;

        // 各値の設定
        imgPickupChara.sprite = charaData.charaSprite;

        txtPickupCharaName.text = charaData.charaName;

        txtPickupCharaAttackPower.text = charaData.attackPower.ToString();

        txtPickupCharaAttackRangeType.text = charaData.attackRange.ToString();

        txtPickupCharaCost.text = charaData.cost.ToString();

        txtPickupCharaMaxAttackCount.text = charaData.maxAttackCount.ToString();

    }

    /// <summary>
    /// WorldCharaGeneratorの命令を受け取る
    /// </summary>
    /// <param name="charaData"></param>
    public void TestReceiveOrder(CharaData charaData)
    {
        //キャラの名前は取れている
        Debug.Log(txtPickupCharaName.text);

        //LinQでListからデータを取れると一番スマート
        //List の機能によりデータベースのようになって管理されているキャラのボタン群から、抽出処理を行う必要
        //ボタンを非表示にする。
        //TESTで閉じるボタンを押せなくしてみた。btnClosePopUpを選択したボタンにしたい。
        //btnClosePopUp.interactable = false;
        //LINQでチャレンジ
        //Debug.Log(CharaDataSO);
        //charaDataSO.charaDatasList.Find(x => x.charaName == txtPickupCharaName.text);
        //foreach (CharaData charaData in CharaData.instance.selectCharaDetailsList)
        //{

        //
    }
    /// 参考
    /// <param name="attackRangeType"></param>
    /// <returns></returns>
    //public Vector2 GetAttackRangeSize(AttackRangeType attackRangeType)
    //{
    //    return attackRangeSizeSO.attackRangeSizesList.Find(x => x.attackRangeType == attackRangeType).size;
    //}

}
