using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ReturnSelectCharaPopUp : MonoBehaviour
{
    [SerializeField]
    private Button btnSubmitReturnChara;

    [SerializeField]
    private Button btnClosePopUp;

    [SerializeField]
    private CanvasGroup canvasGroup;

    private CharaController charaController;
    private GameManager gameManager;

    /// <summary>
    /// 設定
    /// </summary>
    /// <param name="charaController"></param>
    /// <param name="gameManager"></param>
    public void SetUpReturnSelectCharaPopUp(CharaController charaController, GameManager gameManager)
    {
        //CharaControllerのcharaControllerをこのcharaControllerにする。
        this.charaController = charaController;
        this.gameManager = gameManager;

        //ボタンはfalseにする。
        btnSubmitReturnChara.interactable = false;
        btnClosePopUp.interactable = false;

        //クリックでイベント処理を実行する
        btnSubmitReturnChara.onClick.AddListener(OnClickSubmitReturnChara);
        btnClosePopUp.onClick.AddListener(OnClickClosePopUp);

        //ボタンはtrueにする。
        btnSubmitReturnChara.interactable = true;
        btnClosePopUp.interactable = true;
    }

    /// <summary>
    /// 配置解除する選択時
    /// </summary>
    private void OnClickSubmitReturnChara()
    {

        CloseReturnCharaPopUp(true);
    }

    /// <summary>
    /// 配置解除しない選択時
    /// </summary>
    private void OnClickClosePopUp()
    {
        CloseReturnCharaPopUp(false);

    }

    /// <summary>
    /// ポップアップを閉じて選択したボタンの結果を GameMangaer に送る
    /// </summary>
    private void CloseReturnCharaPopUp(bool isReturnChara)
    {
        canvasGroup.DOFade(0, 0.5f).SetEase(Ease.Linear)
            .OnComplete(() => {
                //gameManager.JudgeReturnChara(isReturnChara, charaController);
                DestroyReturnSelectCharaPopUp();
            });
    }


    /// <summary>
    /// ポップアップを破壊
    /// </summary>
    private void DestroyReturnSelectCharaPopUp()
    {
        Destroy(gameObject);
    }
}
