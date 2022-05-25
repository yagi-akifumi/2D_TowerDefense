using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class world_UIManager : MonoBehaviour
{

    [SerializeField]
    private Button worldMain;

    // Start is called before the first frame update
    void Start()
    {
        worldMain.onClick.AddListener(() => SceneStateManager.instance.PreparateNextScene(SceneType.Main));
    }
}
