using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractSetGenerator : MonoBehaviour
{

    [SerializeField, Header("コントラクトセット")]
    private GameObject ContractSet;

    [SerializeField, Header("キャラセット")]
    private GameObject CharaSet;

    [SerializeField, Header("スタンプセット")]
    private GameObject StampSet;

    [SerializeField]
    private Transform canvasTran;

    [SerializeField, Header("キャラセットの表示")]
    private bool CharaSetPopUp;

    [SerializeField, Header("管理画面の表示")]
    private bool ContractSetPreview;

    [SerializeField, Header("スタンプの表示")]
    private bool StampPreview;

    void start()
    {
        CharaSetPopUp = false;
        ContractSetPreview = false;
        StampPreview = false;
    }

    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            if (CharaSetPopUp == false)
            {
                ActivateCharaSetPopUp();
                CharaSetPopUp = true;
            }
            else
            {
                InactivateCharaSetPopUp();
                CharaSetPopUp = false;
            }
        }

        if (CharaSetPopUp == true)
        {
            if (Input.GetKey("up"))
            {
                if (ContractSetPreview == false)
                {
                    ActivateContractSet();
                    ContractSetPreview = true;
                }
                else
                {
                    InactivateContractSet();
                    ContractSetPreview = false;
                }
            }

            //if (Input.GetMouseButtonDown(0) && ContractSetPreview == false && CharaSetPopUp == true)
            //{
            //if (ContractSetPreview == false)
            //{
            //ActivateContractSet();
            //}
            //else
            //{
            // InactivateContractSet();
            //}
        }

    }
    /// <summary>
    /// 表示
    /// </summary>
    private void ActivateCharaSetPopUp()
    {
        // キャラセットのポップアップを生成
        Debug.Log("キャラセットの表示");
        // キャラセットをキャンバスにインスタンス化する
        CharaSet = Instantiate(CharaSet, canvasTran, false);
        // コントラクトセットを表示する
        CharaSet.gameObject.SetActive(true);
        // ContractSetPreviewをtrueにする
        CharaSetPopUp = true;
    }

    /// <summary>
    /// 非表示
    /// </summary>
    private void InactivateCharaSetPopUp()
    {
        // ポップアップを生成
        Debug.Log("キャラセットの非表示");
        // コントラクトセットを非表示にする
        CharaSet.gameObject.SetActive(false);
        // ContractSetPreviewをfalseにする
        CharaSetPopUp = false;
    }

    /// <summary>
    /// 表示
    /// </summary>
    private void ActivateContractSet()
    {
            // ポップアップを生成
            Debug.Log("ゲーム表示");
            // コントラクトセットをキャンバスにインスタンス化する
            ContractSet = Instantiate(ContractSet, canvasTran, false);
            // コントラクトセットを表示する
            ContractSet.gameObject.SetActive(true);
            // ContractSetPreviewをtrueにする
            ContractSetPreview = true;
    }

    /// <summary>
    /// 非表示
    /// </summary>
    private void InactivateContractSet()
    {
        // ポップアップを生成
        Debug.Log("ゲーム非表示");
        // コントラクトセットを非表示にする
        ContractSet.gameObject.SetActive(false);
        // ContractSetPreviewをfalseにする
        ContractSetPreview = false;
    }
}
