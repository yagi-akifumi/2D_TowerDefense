using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 契約演出用のクラス
/// </summary>
public class ContractDetail : MonoBehaviour
{
    [SerializeField]
    private Image imgPickupChara;

    [SerializeField]
    private Text txtPickupCharaName;

    [SerializeField]
    private Button btnSubmitContractStamp;

    [SerializeField]
    private Button btnFilter;

    [SerializeField]
    private CanvasGroup canvasGrouContractSet;

    [SerializeField]
    private CanvasGroup canvasGroupSubmitContractStamp;

    [SerializeField]
    private Image imgContractStamp;

    [SerializeField]
    private CharaData chooseCharaData;

    private WorldPlacementCharaSelectPopUp worldPlacementCharaSelectPop;

    private CharaData worldCharaData;

    /// <summary>
    /// 契約演出の設定
    /// </summary>
    /// <param name="charaData"></param>
    void upload()
    {
        //WorldCharaGeneratorをworldCharaGeneratorとし、ここで使う
        this.worldPlacementCharaSelectPop = worldPlacementCharaSelectPop;
        this.worldCharaData = worldCharaData;

        imgPickupChara.sprite = this.worldCharaData.charaSprite;

    }

    /// <summary>
    /// 契約演出の設定
    /// </summary>
    /// <param name="charaData"></param>
    public void SetUpContractDetail(CharaData charaData)
    {

        // 契約したキャラの画像を設定
        imgPickupChara.sprite = charaData.charaSprite;

        // 契約したキャラの名前を決定
        txtPickupCharaName.text = charaData.charaName;

        // ContractSet にアタッチされている CanvasGroup のアルファを 0 にする
        canvasGrouContractSet.alpha = 0;

        // 順番にボタンを押せるように、あとから表示するスタンプの画像を見えないように設定し、タップ感知しないようにしておく
        canvasGroupSubmitContractStamp.alpha = 0;
        canvasGroupSubmitContractStamp.blocksRaycasts = false;
        imgContractStamp.enabled = false;

        // 各ボタンにメソッドを登録
        btnFilter.onClick.AddListener(OnClickFilter);
        btnSubmitContractStamp.onClick.AddListener(OnClickSubmitContract);

        // 0 にしておいた、最初のボタン用に CanvasGruop を表示
        canvasGrouContractSet.DOFade(1.0f, 0.5f).SetEase(Ease.Linear);
    }

    /// <summary>
    /// スタンプ前にタップした際の処理
    /// </summary>
    private void OnClickFilter()
    {

        // スタンプを動かす
        // スタンプのサイズを３倍にする(サイズの大きくなっているもの小さくする演出につかうため)
        imgContractStamp.transform.localScale = Vector3.one * 3;

        // スタンプの角度をランダムに設定する(捺印の位置が毎回変わるようにする演出)
        imgContractStamp.transform.eulerAngles = new Vector3(0, 0, Random.Range(-30.0f, 30.0f));

        // スタンプの画像を表示
        imgContractStamp.enabled = true;

        // CanvasGroup の透明度を制御
        canvasGroupSubmitContractStamp.alpha = 1.0f;

        // スタンプを元の大きさに戻す。
        // Easeの設定により、元のサイズに戻ってから少しだけ大きくして戻すことで、スタンプを捺しているように見せる
        imgContractStamp.transform.DOScale(Vector3.one, 0.75f)
            .SetEase(Ease.OutBack, 1.0f)
            .OnComplete(() =>
            {
                // ボタン(画面)を押せるようにする
                canvasGroupSubmitContractStamp.blocksRaycasts = true;
            }
        );
    }

    /// <summary>
    /// スタンプ後にタップした際の処理
    /// </summary>
    private void OnClickSubmitContract()
    {
        // 契約演出を終了して、ポップアップも閉じる
        canvasGrouContractSet.DOFade(0.0f, 0.5f).SetEase(Ease.Linear).OnComplete(() => { Destroy(gameObject); });
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