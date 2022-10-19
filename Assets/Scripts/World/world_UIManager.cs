using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class world_UIManager : MonoBehaviour
{

    private WorldGameManager gameManager;

    [SerializeField]
    private Button worldMain;

    [SerializeField]
    private Button KoyoKeiyaku;
    [SerializeField]
    private WorldCharaGenerator worldCharaGenerator;

    

    private WorldPlacementCharaSelectPopUp worldPlacementCharaSelectPopUp;

    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log(worldCharaGenerator);
        worldMain.onClick.AddListener(() => SceneStateManager.instance.PreparateNextScene(SceneType.Main));
        KoyoKeiyaku.onClick.AddListener(worldCharaGenerator.ActivatePlacementCharaSelectPopUp);
    }
}
