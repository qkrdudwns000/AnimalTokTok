using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// (박영준) 플레이시작시 board 생성 및 조정.
public class PlayManager : MonoBehaviour
{
    // 필요 컴퍼넌트
    public Transform tr_Tiles; // block 오브젝트 모아둘 부모 트랜스폼.
    public GameObject tile; // block 프리팹.
    public GameObject[,] tiles; // 모든 block들 list
    public List<Sprite> characters = new List<Sprite>();
    // 보드 크기 가로x세로 (8x8)
    [SerializeField] int width = 8;
    [SerializeField] int height = 8;
    [SerializeField] int increaseScore = 50;
    [SerializeField] float increaseTimer = 0.2f;

    [HideInInspector]
    public int emptyTileCount = 0;

    public static PlayManager inst = null;

    public bool IsSliding { get; set; }

    void Start()
    {
        inst = GetComponent<PlayManager>();
        MakeBoard();
    }

    void MakeBoard() // 보드 만들기 -> 예외 처리: 처음 만들어진 보드판에는 '빙고!'가 있어서는 안됨
    {
        tiles = new GameObject[width, height];

        //간격
        float Interval = 0.625f;

        Sprite[] previousLeft = new Sprite[height]; // 직전 line block ( 바로 왼쪽 블락 )
        Sprite previousBelow = null; // 직전 block ( 바로 밑의 블락 )

        for (int w = 0; w < width; w++)
        {
            for(int h  = 0; h < height; h++)
            {
                GameObject newTile = Instantiate(tile, tr_Tiles);
                tiles[w, h] = newTile;
                newTile.transform.position += new Vector3(w * Interval, h * Interval, 0);

                List<Sprite> possibleCharacters = new List<Sprite>();
                possibleCharacters.AddRange(characters);
                possibleCharacters.Remove(previousLeft[h]); // 첫 줄은 null, 두 번째 줄부터 check
                possibleCharacters.Remove(previousBelow); // 직전 block 확인.

                // 랜덤 sprite 입히기
                Sprite newSprite = possibleCharacters[Random.Range(0, possibleCharacters.Count)];
                newTile.GetComponent<SpriteRenderer>().sprite = newSprite;

                previousLeft[h] = newSprite;
                previousBelow = newSprite;
            }
        }
    }

    public IEnumerator FindEmptyTiles()
    {
        for(int w = 0; w < width; w++)
            for(int h = 0; h < height; h++)
                if (tiles[w, h].GetComponent<SpriteRenderer>().sprite == null)
                {
                    yield return StartCoroutine(SlideDownTiles(w, h));                 
                    break;
                }
        
        yield return new WaitForSeconds(0.4f);
        for (int w = 0; w < width; w++) // 타일들 내려오고나서 다시 match되는것 있나 확인.
            for (int h = 0; h < height; h++)
                tiles[w, h].GetComponent<Tile>().ClearAllMatches();
        if(emptyTileCount >= 3)
            CalcScoreAndTimer();
        emptyTileCount = 0;
    }
    private IEnumerator SlideDownTiles(int _w, int _hStart, float slideDelay = 0.05f)
    {
        IsSliding = true;
        List<SpriteRenderer> renders = new List<SpriteRenderer>();
        int emptyCount = 0;

        for(int h = _hStart; h < height; h++)
        {
            SpriteRenderer render = tiles[_w, h].GetComponent<SpriteRenderer>();
            if (render.sprite == null)
                emptyCount++;
            renders.Add(render);
        }
        EmptyTileCounting();
        for (int i = 0; i < emptyCount; i++)
        {
            yield return new WaitForSeconds(slideDelay);

            for(int j = 0; j < renders.Count - 1; j++)
            {
                renders[j].sprite = renders[j + 1].sprite;
                if (renders[j].sprite == null)
                    renders[j].sprite = GetNewSprite();
                renders[j + 1].sprite = GetNewSprite();
            }
            if (renders.Count == 1) // 맨 윗줄에서 타일 match 되었을 경우 예외처리.
                renders[0].sprite = GetNewSprite();
        }

        IsSliding = false;
    }

    private Sprite GetNewSprite()
    {
        List<Sprite> newSprites = new List<Sprite>();
        newSprites.AddRange(characters);

        return newSprites[Random.Range(0, newSprites.Count)];
    }
    public void EmptyTileCounting()
    {
        emptyTileCount = 0;
        for (int w = 0; w < width; w++)
            for (int h = 0; h < height; h++)
                if (tiles[w, h].GetComponent<SpriteRenderer>().sprite == null)
                    emptyTileCount++;
    }
    public void CalcScoreAndTimer()
    {
        float totalIncreaseTimer = increaseTimer * emptyTileCount;
        GUIManager.inst.Timer += totalIncreaseTimer; // 타임 증가.
        GUIManager.inst.AddTimer(totalIncreaseTimer);
        GUIManager.inst.Score += increaseScore * emptyTileCount; // 스코어 증가. emptyCount 갯수에 비례해서
    }
    public void RainbowBombItem(int _tileNum)
    {
        for (int w = 0; w < width; w++)
            for (int h = 0; h < height; h++)
                if (tiles[w, h].GetComponent<SpriteRenderer>().sprite == characters[_tileNum])
                    tiles[w, h].GetComponent<SpriteRenderer>().sprite = null;
        AudioManager.inst.PlaySFX("Matching");

        EmptyTileCounting();
        CalcScoreAndTimer();
        emptyTileCount = 0;

        StopCoroutine(FindEmptyTiles());
        StartCoroutine(FindEmptyTiles());
    }
}
