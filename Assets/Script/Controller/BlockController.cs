using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ブロックの生成と判定を行うクラス
/// </summary>
public class BlockController : MonoBehaviour
{
    private GameController gameController;
    private NextBlock nextBlock;

    /// <summary>
    /// ブロックプレハブ
    /// </summary>
    [SerializeField]
    private GameObject block=null;

    /// <summary>
    /// 落下ブロックの配列
    /// </summary>
    private GameObject[] blocks = new GameObject[4];

    /// <summary>
    /// ブロック落下先のガイドライン
    /// </summary>
    [SerializeField]
    private GameObject guidLineL = null;
    [SerializeField]
    private GameObject guidLineR = null;

    public GameObject guidL;
    public GameObject guidR;

    //オフセット用
    readonly Vector3 ROW = new Vector3(1f, 0f);
    readonly Vector3 HALF_ROW = new Vector3(0.5f, 0f);
    readonly Vector3 COL = new Vector3(0f, 1f);
    readonly Vector3 HALF_COL = new Vector3(0f, 0.5f);

    /// <summary>
    ///ブロックに貼るスプライト
    /// </summary>
    private Sprite blockSprite;

    /// <summary>
    /// 明暗ブロックのスプライト
    /// </summary>
    [SerializeField]
    private Sprite lightSprite = null;
    [SerializeField]
    private Sprite blackSprite = null;

    /// <summary>
    /// ブロック優先度
    /// </summary>
    private int[] blockPriority = new int[4];

    private GameObject stopBlock;

    void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        stopBlock = GameObject.Find("StopBrock");
        nextBlock = GameObject.Find("NextBlock").GetComponent<NextBlock>();
    }

    void Start()
    {
        //2×2のブロックを生成
        for(int x = 0; x <= 3; x++)
        {
            blockPriority[x] = nextBlock.NextGet(x);
            if (x == 0)
            {
                blocks[x] = Instantiate(block, transform.position - HALF_ROW + HALF_COL, transform.rotation);
            }
            else if (x == 1)
            {
                blocks[x] = Instantiate(block, transform.position + HALF_ROW + HALF_COL, transform.rotation);
            }
            else if (x == 2)
            {
                blocks[x] = Instantiate(block, transform.position - HALF_ROW - HALF_COL, transform.rotation);
            }
            else if (x == 3)
            {
                blocks[x] = Instantiate(block, transform.position + HALF_ROW - HALF_COL, transform.rotation);
            }

            //取得した変数の中身でブロックの色を塗り替え
            blocks[x].GetComponent<BlockColor>().blockType = blockPriority[x];

            if (blockPriority[x] == 1)
            {
                blockSprite = lightSprite;
                blocks[x].GetComponent<SpriteRenderer>().sprite = blockSprite;
            }
            else if (blockPriority[x] == 2)
            {
                blockSprite = blackSprite;
                blocks[x].GetComponent<SpriteRenderer>().sprite = blockSprite;
            }

            if (x == 3)
            {
                nextBlock.NextReset();
            }
        }

        guidL = Instantiate(guidLineL, transform.position - ROW, transform.rotation);
        guidR = Instantiate(guidLineR, transform.position + ROW, transform.rotation);

        foreach (GameObject block in blocks)
        {
            //生成したブロックを子として登録
            block.transform.parent = transform;
        }

        // 生成位置でブロックと重なったら
        if (!LandingFieldCheck())
        {
            gameController.gameState = GameController.GameState.GAMEOVER;
        }
    }

    ///<summary>
    /// ブロックのセルの中身が空か確認
    /// </summary>
    public bool BlockFieldCheck()
    {
        foreach (GameObject block in blocks)
        {
            int data;
            data = gameController.GetFiledCheck(block.transform.position);

            if (data != GameController.NULL_DATA) return false;
        }
        return true;
    }

    ///<summary>
    ///ブロックの下のセルの中身が空か確認
    ///</summary>
    public bool LandingFieldCheck()
    {
        foreach(GameObject block in blocks)
        {
            int underData;
            underData = gameController.GetFiledCheck(block.transform.position - COL);

            if (underData != GameController.NULL_DATA) return false;
        }

        return true;
    }
    ///<summary>
    ///空白の数をカウント
    ///</summary>
    int NullCount(Vector3 pos)
    {
        int data;
        data = gameController.GetFiledCheck(transform.position + pos);

        if (data == GameController.NULL_DATA)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    ///<summary>
    ///配列に値を格納
    ///</summary>
    public void ArrayStore()
    {
        foreach (GameObject block in blocks)
        {
            //ブロックを配列に格納
            gameController.SetBlockArrayStore(
                block.transform.position, block.GetComponent<BlockColor>().blockType, block);
        }

        foreach(GameObject block in blocks)
        {
            block.transform.parent = null;
            //格納したブロックをStopBlockにまとめる
            block.transform.parent = stopBlock.transform;
        }
        guidL.transform.parent = this.transform;
        guidR.transform.parent = this.transform;
        //オブジェクトを削除
        Destroy(gameObject);
    }
}
