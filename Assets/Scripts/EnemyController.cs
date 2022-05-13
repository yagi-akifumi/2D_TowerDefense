using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;


public class EnemyController : MonoBehaviour
{
    [SerializeField, Header("移動経路の情報")]
    private PathData pathData;

    [SerializeField, Header("移動速度")]
    private float moveSpeed;

    private Vector3[] paths;

    private Animator anim;　　　　　　 // Animator コンポーネントの取得用


    void Start()
    {
        // Animator コンポーネントを取得して anim 変数に代入
        TryGetComponent(out anim);

        // 移動する地点を取得
        paths = pathData.pathTranArray.Select(x => x.position).ToArray();

        // 各地点に向けて移動
        transform.DOPath(paths, 1000 / moveSpeed).SetEase(Ease.Linear).OnWaypointChange(ChangeAnimeDirection);
    }

    /// <summary>
    /// 敵の進行方向を取得して、移動アニメと同期
    /// </summary>
    private void ChangeAnimeDirection(int index)
    {

        // 次の移動先の地点がない場合には、ここで処理を終了する
        if (index >= paths.Length)
        {
            return;
        }

        // 目標の位置と現在の位置との距離と方向を取得し、正規化処理を行い、単位ベクトルとする(方向の情報は持ちつつ、距離による速度差をなくして一定値にする)
        Vector3 direction = (transform.position - paths[index]).normalized;
        Debug.Log(direction);

        // アニメーションの Palameter の値を更新し、移動アニメの BlendTree を制御して移動の方向と移動アニメを同期
        anim.SetFloat("X", direction.x);
        anim.SetFloat("Y", direction.y);

    }
}