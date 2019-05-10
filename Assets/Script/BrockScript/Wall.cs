using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 壁の情報を配列に格納するクラス
/// </summary>
public class Wall : MonoBehaviour
{
    [SerializeField]
    private GameController gameController=null;

    void Start()
    {
        //配列に格納
        gameController.SetArrayStore(
            gameObject.transform.position, GameController.WALL_DATA, gameObject);
    }
}
