using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームシステムを制御するクラス
/// </summary>
public class GameController : MonoBehaviour
{
    public enum GameState
    {
        ///<summary>
        ///ブロックを生成
        ///</summary>
        CREATE,
        ///<summary>
        ///ブロックを操作
        ///</summary>
        PLAY,
        ///<summary>
        ///消去可能なブロックがあるかのチェック
        ///</summary>
        CHECK_DELETE,
        ///<summary>
        ///消えたブロックを反映する
        ///</summary>
        DROP,
        ///<summary>
        ///ゲームオーバー
        ///</summary>
        GAMEOVER,
    }

    public GameState gameState = GameState.CREATE;
    #region 配列
    ///<summary>
    ///フィールドの横幅
    ///</summary>
    const int FIELD_X = 14;
    ///<summary>
    ///フィールドの横幅
    ///</summary>
    public const int FIELD_Y = 14;

    ///<summary>
    ///フィールド
    ///</summary>
    public int[,] fields = new int[FIELD_X, FIELD_Y];
    ///<summary>
    ///削除予想されたブロック
    ///</summary>
    public int[,] delBlocks = new int[FIELD_X, FIELD_Y];
    ///<summary>
    ///削除予想されたブロックにかぶせるプレハブ
    ///</summary>
    public GameObject[,] delBlockPrefabs = new GameObject[FIELD_X, FIELD_Y];
    ///<summary>
    ///フィールドと同期したブロック
    ///</summary>
    public GameObject[,] blocks = new GameObject[FIELD_X, FIELD_Y];

    ///<summary>
    ///配列格納用の空白データ
    ///</summary>
    public const int NULL_DATA = 0;
    ///<summary>
    ///配列格納用の壁データ
    ///</summary>
    public const int WALL_DATA = 196;

    ///<summary>
    ///空いているブロックの数
    ///</summary>
    private int nullCount = 0;

    ///<summary>
    ///配列に格納した数
    ///</summary>
    private int storeCount = 0;
    #endregion

    #region ブロック用変数
    ///<summary>
    ///ブロック
    ///</summary>
    [SerializeField]
    private GameObject block=null;

    ///<summary>
    ///配列格納用の明ブロックデータ
    ///</summary>
    public const int LIGHT_BLOCK = 1;
    ///<summary>
    ///配列格納用の暗ブロックデータ
    ///</summary>
    public const int BRACK_BLOCK = 2;
    ///<summary>
    ///消去判定された明ブロックデータ
    ///</summary>
    public const int DELETE_LIGHT = 3;
    ///<summary>
    ///消去判定された暗ブロックデータ
    ///</summary>
    public const int DELETE_BRACK = 4;
    ///<summary>
    ///消去判定された明ブロックデータ
    ///</summary>
    public const int DESTROY_LIGHTBLOCK = 5;
    ///<summary>
    ///削除が確定した暗ブロックデータ
    ///</summary>
    public const int DESTROY_BRACKBLOCK = 6;

    /// <summary>
    /// ペア成立の明ブロックデータ
    /// </summary>
    public const int SQUARE_LIGHTBLOCK = 10;
    /// <summary>
    /// ペア成立の暗ブロックデータ
    /// </summary>
    public const int SQUARE_BRACKBLOCK = 20;

    ///<summary>
    ///消すブロックにかぶせるプレハブ
    ///</summary>
    [SerializeField]
    private GameObject deleteLightPrefab=null;
    [SerializeField]
    private GameObject deleteBrackPrefab = null;

    /// <summary>
    /// 消去エフェクト
    /// </summary>
    [SerializeField]
    private GameObject deleteEffect = null;

    /// <summary>
    /// 役成立時のエフェクト
    /// </summary>
    [SerializeField]
    private GameObject setEffect = null;

    ///<summary>
    ///ブロック生成位置
    ///</summary>
    readonly Vector3 BROCK_POINT = new Vector3(6.5f, 12.5f,-7f);

    #endregion

    /// <summary>
    /// エフェクトのセット位置
    /// </summary>
    readonly Vector3 SET = new Vector3(0.5f, 0.5f, 0);

    ///<summary>
    ///ゲームオーバー画面
    ///</summary>
    [SerializeField]
    private GameObject gameOver=null;

    ///<summary>
    ///ブロック落下の可否
    ///</summary>
    private bool downDecision;

    /// <summary>
    /// 消去ブロックのペアがあるか
    /// </summary>
    private bool checkDelete = false;

    /// <summary>
    /// ブロックが落ちた後
    /// </summary>
    private bool afteDrop = false;

    /// <summary>
    /// ドロップ中
    /// </summary>
    private bool dropNow = true;

    [SerializeField]
    private SoundController soundController = null;

    /// <summary>
    /// 現在のステージ
    /// </summary>
    public int stage = 1;

    private Vector3 efe;

    void Awake()
    {
        for (int y = 0; y < FIELD_Y; y++)
        {
            for (int x = 0; x < FIELD_X; x++)
            {
                fields[x, y] = NULL_DATA;
                delBlocks[x, y] = NULL_DATA;
                blocks[x, y] = null;
            }
        }
        efe = new Vector3(0, 0, -1f);
    }

    void Update()
    {
        switch (gameState)
        {
            #region ブロックを生成
            case GameState.CREATE:
                Vector3 point = Vector3.zero;

                //生成位置を設定
                point = BROCK_POINT;

                Instantiate(block, point, transform.rotation);

                //状態をブロック操作中に
                gameState = GameState.PLAY;
                break;
            #endregion

            #region ブロックを移動
            case GameState.PLAY:
                break;
            #endregion

            #region ブロックの消去判定
            case GameState.CHECK_DELETE:
                gameState = GameState.CREATE;
                break;
            #endregion

            #region 消したブロックより上にあるブロックをすべて下にずらす
            case GameState.DROP:
                afteDrop = true;
                Drop();
                soundController.DownSound();
                gameState = GameState.CHECK_DELETE;
                break;
            #endregion

            #region ゲームオーバー
            case GameState.GAMEOVER:
                gameOver.SetActive(true);
            break;
            #endregion

        }


    }

    ///<summary>
    ///判定してから再度ブロックを生成
    ///</summary>
    public void Create()
    {
        gameState = GameState.DROP;
    }

    ///<summary>
    ///配列の中身を返す
    ///</summary>
    public int GetFiledCheck(Vector3 pos)
    {
        int x = Mathf.RoundToInt(pos.x);
        int y = Mathf.RoundToInt(pos.y);

        //配列の範囲内だったら
        if (x >= 0 && x < FIELD_X && y >= 0 && y < FIELD_Y)
        {
            //配列の中身を返す
            return fields[x, y];
        }
        else
        {
            //壁データを返す
            return WALL_DATA;
        }
    }

    ///<summary>
    ///配列に格納
    ///</summary>
    public void SetArrayStore(Vector3 pos, int num, GameObject gameObject)
    {
        int x = Mathf.RoundToInt(pos.x);
        int y = Mathf.RoundToInt(pos.y);

        //配列の範囲内だったら
        if (x >= 0 && x > FIELD_X && y >= 0 && y < FIELD_Y)
        {
            //配列に格納
            fields[x, y] = num;
            blocks[x, y] = gameObject;
        }
    }

    ///<summary>
    ///ブロックを配列に格納
    ///</summary> 
    public void SetBlockArrayStore(Vector3 pos, int num, GameObject gameObject)
    {
        int x = Mathf.RoundToInt(pos.x);
        int y = Mathf.RoundToInt(pos.y);

        //配列の範囲内だったら
        if (x >= 0 && x < FIELD_X && y >= 0 && y < FIELD_Y)
        {
            fields[x, y] = num;
            blocks[x, y] = gameObject;

            //配列に格納した数をカウント
            storeCount++;


            // 4ピース格納したら
            if (storeCount >= 4)
            {
                storeCount = 0;
                gameState = GameState.CREATE;
            }
        }
    }

    ///<summary>
    ///ブロックを落とす
    ///</summary>
    public void Drop()
    {
           dropNow = true;
            downDecision = false;
            for (int y = 0; y < FIELD_Y-1; y++)
            {
                for (int x = 0; x < FIELD_X; x++)
                {
                    if (fields[x, y] == NULL_DATA && fields[x, y + 1] >= 1)
                    {
                        nullCount++;
                        fields[x, y] = fields[x, y + 1];
                        fields[x, y + 1] = NULL_DATA;

                        delBlocks[x, y] = delBlocks[x, y + 1];
                        delBlocks[x, y + 1] = NULL_DATA;

                        blocks[x, y] = blocks[x, y + 1];
                        blocks[x, y + 1] = null;

                        delBlocks[x, y] = delBlocks[x, y + 1];
                        delBlocks[x, y + 1] = NULL_DATA;
                    // オブジェクトの位置を配列と同期
                    blocks[x, y].transform.position = new Vector3(x, y, -7f);

                        downDecision = true;
                    }
                }
            }
        dropNow = false;
        if (downDecision == true)
        {
            Drop();
        } else if (downDecision == false )
        {
            CheckDelete();
        }
    }

    ///<summary>
    ///ブロックを完全に消去
    ///</summary>
    public void BlockDelete()
    {
        for (int y = 0; y < FIELD_Y; y++)
        {
            for(int x = 0; x < FIELD_X ; x++)
            {
                if (fields[x, y] == DESTROY_BRACKBLOCK
                    || fields[x, y] == DESTROY_LIGHTBLOCK)
                {
                    //配列の中身を削除
                    fields[x, y] = NULL_DATA;
                    delBlocks[x, y] = NULL_DATA;
                    if (blocks[x, y] != null)
                    {
                        soundController.DeleteSound();
                        Instantiate(deleteEffect, blocks[x, y].transform.position, blocks[x, y].transform.rotation);
                        //ブロックを削除
                        Destroy(blocks[x, y].gameObject);
                        Destroy(delBlockPrefabs[x, y].gameObject);
                    }
                }
            }
        }
        ResetDelete();
        checkDelete = false;
        Drop();
    }

    ///<summary>
    ///ブロックが消せるかチェック
    ///<summary>
    public void CheckDelete()
    {
        if (dropNow == true) return;
        //この中で消去できそうな左右で同色のペアを探す
        for (int y = 0; y < FIELD_Y; y++)
        {
            for (int x = 0; x < FIELD_X -1 ; x++)
            {
                if (fields[x, y] == LIGHT_BLOCK
                    || fields[x, y] == DELETE_LIGHT)
                {
                    if (fields[x + 1, y] == LIGHT_BLOCK 
                        || fields[x + 1, y] == DELETE_LIGHT)
                    {
                        delBlocks[x, y] = LIGHT_BLOCK;
                    }
                }
                else if (fields[x, y] == BRACK_BLOCK
                    || fields[x, y] == DELETE_BRACK)
                {
                    if (fields[x + 1, y] == BRACK_BLOCK 
                        || fields[x + 1, y] == DELETE_BRACK)
                    {
                        delBlocks[x, y] = BRACK_BLOCK;
                    }
                }
                else
                {
                    delBlocks[x, y] = NULL_DATA;
                }
            }
        }

        //同色で上下に同じペアがあるなら優先度を変更する
        for (int y = 0; y < FIELD_Y-1 ; y++)
        {
            for (int x = 0; x < FIELD_X ; x++)
            {
                if (delBlocks[x, y] == delBlocks[x, y + 1]
                    && delBlocks[x, y] != NULL_DATA)
                {
                    if (fields[x, y] == DELETE_BRACK
                       && fields[x + 1, y + 1] == DELETE_BRACK)
                    {
                        
                    }
                    else if (fields[x, y] == DELETE_LIGHT
                       && fields[x + 1, y + 1] == DELETE_LIGHT)
                    {
                        
                    }
                    else
                    {
                        Instantiate(setEffect, blocks[x, y].transform.position + SET, blocks[x, y].transform.rotation);
                    }

                    if (delBlocks[x, y] == LIGHT_BLOCK)
                    {
                        delBlocks[x, y] = SQUARE_LIGHTBLOCK;
                        fields[x, y] = DELETE_LIGHT;
                        fields[x + 1, y] = DELETE_LIGHT;
                        fields[x, y + 1] = DELETE_LIGHT;
                        fields[x + 1, y + 1] = DELETE_LIGHT;
                        checkDelete = true;
                    }
                    else if (delBlocks[x, y] == BRACK_BLOCK )
                    {
                        delBlocks[x, y] = SQUARE_BRACKBLOCK; 
                        fields[x, y] = DELETE_BRACK;
                        fields[x + 1, y] = DELETE_BRACK;
                        fields[x, y + 1] = DELETE_BRACK;
                        fields[x + 1, y + 1] = DELETE_BRACK;
                        checkDelete = true;
                    }
                }
            }
        }

        //消去が確定したブロックにプレハブを貼る
        for (int y = 0; y < FIELD_Y ; y++)
        {
            for (int x = 0; x < FIELD_X ; x++)
            {
                if (fields[x, y] == DELETE_BRACK && delBlockPrefabs[x, y] == null)
                {
                    delBlockPrefabs[x, y] = Instantiate(deleteBrackPrefab, new Vector3(x, y, -7.2f), transform.rotation);
                }
                else if (fields[x, y] == DELETE_LIGHT && delBlockPrefabs[x, y] == null)
                {
                    delBlockPrefabs[x, y] = Instantiate(deleteLightPrefab, new Vector3(x, y, -7.2f), transform.rotation);
                }
            }
        }

        if (checkDelete == true && afteDrop == true)
        {
            soundController.DeleteCheckSound(); 
        }
        afteDrop = false;
        checkDelete = false;
    }

    ///<summary>
    ///消去判定を一度リセット
    ///<summary>
    public void ResetDelete()
    {
        for (int y = 0; y < FIELD_Y; y++)
        {
            for (int x = 0; x < FIELD_X; x++)
            {
                if (delBlocks[x, y] != NULL_DATA && fields[x + 1, y] == NULL_DATA)
                {
                    if (fields[x, y] == DELETE_BRACK)
                    {
                        //配列の中身を戻す
                        fields[x, y] = BRACK_BLOCK;

                        //プレハブを消す
                        delBlocks[x, y] = NULL_DATA;
                        if (delBlockPrefabs[x, y] == null) continue;
                        Destroy(delBlockPrefabs[x, y].gameObject);
                    } else if (fields[x, y] == DELETE_LIGHT)
                    {
                        //配列の中身を戻す
                        fields[x, y] = LIGHT_BLOCK;

                        //プレハブを消す
                        if (delBlockPrefabs[x, y] == null) continue;
                        Destroy(delBlockPrefabs[x, y].gameObject);
                    }
                    delBlocks[x, y] = NULL_DATA;
                }

                if (fields[x, y] == NULL_DATA&&delBlockPrefabs[x,y] != null)
                {
                    Destroy(delBlockPrefabs[x, y].gameObject);
                }
            }
        }
    }
}
