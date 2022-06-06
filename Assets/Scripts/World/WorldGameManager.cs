using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGameManager : MonoBehaviour
{
    [SerializeField]
    private WorldCharaGenerator worldCharaGenerator;

    // Start is called before the first frame update
    void Start()
    {
        //worldCharaGeneratorをスクリプトの設定処理を実行する
        // キャラ配置用ポップアップの生成と設定
        StartCoroutine(worldCharaGenerator.SetUpCharaGenerator(this));
        Debug.Log("aaa");
    }
}
