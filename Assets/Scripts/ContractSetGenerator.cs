using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractSetGenerator : MonoBehaviour
{

    [SerializeField]
    private GameObject ContractSet;

    [SerializeField]
    private Transform canvasTran;

    [SerializeField,Header("管理画面の表示")]
    private bool ContractSetPreview;

    [SerializeField,Header("スタンプの表示")]
    private bool StampPreview;

    void start()
    {
        ContractSetPreview = false;
        StampPreview = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ContractSetPreview == false)
            {
                ActivateContractSet();
            }

            else
            if(ContractSetPreview == true)
            {
                InactivateContractSet();
            }
        }
    }

    /// <summary>
    /// 表示
    /// </summary>
    public void ActivateContractSet()
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
    public void InactivateContractSet()
    {
        // ポップアップを生成
        Debug.Log("ゲーム非表示");
        // コントラクトセットを非表示にする
        ContractSet.gameObject.SetActive(false);
        // ContractSetPreviewをfalseにする
        ContractSetPreview = false;
    }
}
