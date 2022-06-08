using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    private SelectCharaDetail selectCharaDetailPrefab;　　　　//　キャラのボタン用のプレファブをアサインする

    [SerializeField]
    private Transform selectCharaDetailTran;　　　　　　　　　//　キャラのボタンを生成する位置をアサインする

    [SerializeField]
    private List<SelectCharaDetail> selectCharaDetailsList = new List<SelectCharaDetail>();　　//　生成したキャラのボタンを管理する

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

        // 各ボタンにメソッドを登録
        btnKoyoKeiyaku.onClick.AddListener(OnClickSubmitKoyoKeiyaku);

        btnClosePopUp.onClick.AddListener(OnClickClosePopUp);

        Debug.Log("botan");

        // 各ボタンを押せる状態にする
        SwithcActivateButtons(true);

        // スクリプタブル・オブジェクトに登録されているキャラ分(引数で受け取った情報)を利用して
        for (int i = 0; i < haveCharaDataList.Count; i++)
        {

            // ボタンのゲームオブジェクトを生成
            SelectCharaDetail worldselectCharaDetail = Instantiate(selectCharaDetailPrefab, selectCharaDetailTran, false);

            // ボタンのゲームオブジェクトの設定(CharaData を設定する)
            worldselectCharaDetail.SetUpSelectCharaDetail(this, haveCharaDataList[i]);

            // List に追加
            selectCharaDetailsList.Add(worldselectCharaDetail);

            // 最初に生成したボタンの場合
            if (i == 0)
            {

                // 選択しているキャラとして初期値に設定
                SetSelectCharaDetail(haveCharaDataList[i]);
            }
        }


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
    /// 配置を止めるボタンを押した際の処理
    /// </summary>
    private void OnClickClosePopUp()
    {
        Debug.Log("閉じる1");
        // ポップアップの非表示
        HidePopUp();
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
    /// ポップアップの非表示
    /// </summary>
    private void HidePopUp()
    {

        //Debug.Log("HidePopUp");
        // TODO 各キャラのボタンの制御


        // ポップアップの非表示
        //canvasGroup.DOFade(0, 0.5f).OnComplete(() => charaGenerator.InactivatePlacementCharaSelectPopUp());  // 次の手順でメソッドを追加するので、それまでコメントアウトしておいてください。
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
}

