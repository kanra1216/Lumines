using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 次に落ちてくるブロックの保存と表示をするクラス
/// </summary>
public class NextBlock : MonoBehaviour
{
    public int[] nextBlock = new int[4];
    private int[] secondBlock = new int[4];
    private int[] thardBlock = new int[4];

    private GameObject[] nexts = new GameObject[4];
    private GameObject[] seconds = new GameObject[4];
    private GameObject[] thards = new GameObject[4];

    #region sprite
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
    /// 次の落下ブロック
    /// </summary>
    [SerializeField]
    private GameObject next1 = null;
    [SerializeField]
    private GameObject next2 = null;
    [SerializeField]
    private GameObject next3 = null;
    [SerializeField]
    private GameObject next4 = null;

    /// <summary>
    /// 二個先の落下ブロック
    /// </summary>
    [SerializeField]
    private GameObject second1 = null;
    [SerializeField]
    private GameObject second2 = null;
    [SerializeField]
    private GameObject second3 = null;
    [SerializeField]
    private GameObject second4 = null;

    /// <summary>
    /// 三個先の落下ブロック
    /// </summary>
    [SerializeField]
    private GameObject thard1 = null;
    [SerializeField]
    private GameObject thard2 = null;
    [SerializeField]
    private GameObject thard3 = null;
    [SerializeField]
    private GameObject thard4 = null;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x <= 3; x++)
        {
            nextBlock[x] = Random.Range(1, 3);
            secondBlock[x] = Random.Range(1, 3);
            thardBlock[x] = Random.Range(1, 3);
        }

        for (int x = 0; x <= 3; x++)
        {
            if (x == 0)
            {
                nexts[x] = next1;
                seconds[x] = second1;
                thards[x] = thard1;
            }
            else if (x == 1)
            {
                nexts[x] = next2;
                seconds[x] = second2;
                thards[x] = thard2;
            }
            else if (x == 2)
            {
                nexts[x] = next3;
                seconds[x] = second3;
                thards[x] = thard3;
            }
            else if (x == 3)
            {
                nexts[x] = next4;
                seconds[x] = second4;
                thards[x] = thard4;
            }
        }

        for (int x = 0; x <= 3; x++)
        {
            if (nextBlock[x] == 1)
            {
                nexts[x].GetComponent<SpriteRenderer>().sprite = lightSprite;
            }
            else if (nextBlock[x] == 2)
            {
                nexts[x].GetComponent<SpriteRenderer>().sprite = blackSprite;
            }
        }

        for (int x = 0; x <= 3; x++)
        {
            if (secondBlock[x] == 1)
            {
                seconds[x].GetComponent<SpriteRenderer>().sprite = lightSprite;
            }
            else if (secondBlock[x] == 2)
            {
                seconds[x].GetComponent<SpriteRenderer>().sprite = blackSprite;
            }
        }

        for (int x = 0; x <= 3; x++)
        {
            if (thardBlock[x] == 1)
            {
                thards[x].GetComponent<SpriteRenderer>().sprite = lightSprite;
            }
            else if (nextBlock[x] == 2)
            {
                thards[x].GetComponent<SpriteRenderer>().sprite = blackSprite;
            }
        }
    }

    public int NextGet(int x)
    {
        return nextBlock[x];
    }

    public void NextReset()
    {
        for (int x = 0; x <= 3; x++)
        {
            nextBlock[x] = secondBlock[x];

            if (nextBlock[x] == 1)
            {
                nexts[x].GetComponent<SpriteRenderer>().sprite = lightSprite;
            }
            else if (nextBlock[x] == 2)
            {
                nexts[x].GetComponent<SpriteRenderer>().sprite = blackSprite;
            }

            if (x == 3)
            {
                SecondReset();
            }
        }
    }

    private void SecondReset()
    {
        for (int x = 0; x <= 3; x++)
        {
            secondBlock[x] = thardBlock[x];

            if (secondBlock[x] == 1)
            {
                seconds[x].GetComponent<SpriteRenderer>().sprite = lightSprite;
            }
            else if (secondBlock[x] == 2)
            {
                seconds[x].GetComponent<SpriteRenderer>().sprite = blackSprite;
            }

            if (x == 3)
            {
                ThardReset();
            }
        }
    }

    private void ThardReset()
    {
        for (int x = 0; x <= 3; x++)
        {
            thardBlock[x] = Random.Range(1, 3);

            if (thardBlock[x] == 1)
            {
                thards[x].GetComponent<SpriteRenderer>().sprite = lightSprite;
            }
            else if (thardBlock[x] == 2)
            {
                thards[x].GetComponent<SpriteRenderer>().sprite = blackSprite;
            }
        }
    }
}
