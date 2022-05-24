using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;

    [Header("コスト用の通貨")]
    public int currency;

    [Header("カレンシーの最大値")]
    public int maxCurrency;

    [Header("加算までの待機時間")]
    public int currencyIntervalTime;

    [Header("加算値")]
    public int addCurrencyPoint;

    [Header("配置できるキャラの上限値")]
    public int maxCharaPlacementCount;　　 // 配置できるキャラの上限数

    [Header("デバッグモードの切り替え")]
    public bool isDebug;　　　　　　　　　// true の場合、デバッグモードとする

    public int defenseBaseDurability;

    public int stageNo;

    public int totalClearPoint;　　　　　　　　　　　　　　　　　　// バトルクリア時にもらえるポイント。消費してキャラと契約できる

    [Header("契約して所持しているキャラの番号")]
    public List<int> engageCharaNosList = new List<int>();

    [Header("表示するステージの番号")]
    public List<int> clearedStageNosList = new List<int>();



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}