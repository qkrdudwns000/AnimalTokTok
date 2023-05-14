using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// (박영준) Tile 클릭 시 이벤트 발생 스크립트.
public class Tile : MonoBehaviour
{
    private static Color selectColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);
    private static Tile previousTile = null;

    SpriteRenderer render;
    Collider2D myColider;
    bool isSelect = false;

    private Vector2[] adjacentDir = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    private bool matchFound = false;


    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        myColider = GetComponent<Collider2D>();
    }

    private void OnMouseDown()
    {
        if (render.sprite == null || PlayManager.inst.IsSliding)
            return;

        if (isSelect)
            Deselect();
        else
        {
            if (previousTile == null) // 첫 클릭
                Select();
            else
            {
                Debug.Log(previousTile.gameObject);
                // 4방향 인접 오브젝트 중 지금 클릭중인 오브젝트가 있을 경우 실행.
                if (GetAllAdjcentTiles().Contains(previousTile.gameObject))
                {
                    SwapSprite(previousTile.render);
                    previousTile.ClearAllMatches(); // 내가 클릭했던 tile match확인
                    previousTile.Deselect();
                    ClearAllMatches(); // 수동적으로 내가 클릭했던 tile과 교환된 현재 tile match 확인.
                    if(PlayManager.inst.emptyTileCount >= 3)
                        PlayManager.inst.CalcScoreAndTimer();
                    PlayManager.inst.emptyTileCount = 0;
                }
                else // 아닐 경우
                {
                    previousTile.GetComponent<Tile>().Deselect(); // 전 클릭 취소
                    Select(); // 현재 클릭 활성화.
                }

            }
        }
    }

    private void Select()
    {
        isSelect = true;
        render.color = selectColor;
        previousTile = gameObject.GetComponent<Tile>();
    }
    void Deselect()
    {
        isSelect = false;
        render.color = Color.white;
        previousTile = null;
    }

    public void SwapSprite(SpriteRenderer _render)
    {
        if (render.sprite == _render.sprite) // 고른 sprite와 옮긴 sprite가 같을 경우 return
            return;

        Sprite tempSprite = _render.sprite;
        _render.sprite = render.sprite;
        render.sprite = tempSprite;
    }

    private GameObject GetAdjcent(Vector2 _dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _dir);
        if (hit.collider != null)
        {
            return hit.collider.gameObject; // 탐색 오브젝트가 있을 경우 해당 gameobject return
        }

        return null; // 아무것도 탐색 못할 시에는 null return
    }
    private List<GameObject> GetAllAdjcentTiles()
    {
        List<GameObject> adjcentTiles = new List<GameObject>();
        myColider.enabled = false;
        for (int i = 0; i < adjacentDir.Length; i++)
            adjcentTiles.Add(GetAdjcent(adjacentDir[i])); // U,D,L,R 4방향 인접 오브젝트 체크

        myColider.enabled = true;
        return adjcentTiles;
    }

    private List<GameObject> FindMatch(Vector2 _dir) // 같은 Sprite 찾는 메소드
    {
        List<GameObject> matchingTiles = new List<GameObject>();
        myColider.enabled = false;
        Collider2D co_Temp = null; // 직전 hit.colider를 담을 임시변수
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _dir);

        // 같은 sprite 아니거나 빈 공간일때까지 
        while(hit.collider != null && hit.collider.GetComponent<SpriteRenderer>().sprite == render.sprite)
        {
            if (co_Temp != null)
                co_Temp.enabled = true;

            matchingTiles.Add(hit.collider.gameObject);
            co_Temp = hit.collider;
            co_Temp.enabled = false;
            hit = Physics2D.Raycast(hit.transform.position, _dir);
        }
        myColider.enabled = true;
        if(co_Temp != null)
            co_Temp.enabled = true;

        return matchingTiles;
    }
    private void ClearMatch(Vector2[] _straigtDir)
    {
        List<GameObject> matchingTiles = new List<GameObject>();
        for (int i = 0; i < _straigtDir.Length; i++)
            matchingTiles.AddRange(FindMatch(_straigtDir[i]));
        if(matchingTiles.Count >= 2) // 자기자신 제외하고 2개 더
        {
            for (int i = 0; i < matchingTiles.Count; i++)
                matchingTiles[i].GetComponent<SpriteRenderer>().sprite = null; // 그림 지워주기.

            matchFound = true;
        }    
    }
    public void ClearAllMatches()
    {
        if (render.sprite == null)
            return;    

        //수직 and 수평 3match 순차대로 찾기.
        ClearMatch(new Vector2[2] { Vector2.left, Vector2.right });
        ClearMatch(new Vector2[2] { Vector2.up, Vector2.down });

        if (matchFound)
        {
            render.sprite = null;
            matchFound = false;

            StopCoroutine(PlayManager.inst.FindEmptyTiles());
            StartCoroutine(PlayManager.inst.FindEmptyTiles());
        }
    }
}
