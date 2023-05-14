using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// (�ڿ���) Tile Ŭ�� �� �̺�Ʈ �߻� ��ũ��Ʈ.
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
            if (previousTile == null) // ù Ŭ��
                Select();
            else
            {
                Debug.Log(previousTile.gameObject);
                // 4���� ���� ������Ʈ �� ���� Ŭ������ ������Ʈ�� ���� ��� ����.
                if (GetAllAdjcentTiles().Contains(previousTile.gameObject))
                {
                    SwapSprite(previousTile.render);
                    previousTile.ClearAllMatches(); // ���� Ŭ���ߴ� tile matchȮ��
                    previousTile.Deselect();
                    ClearAllMatches(); // ���������� ���� Ŭ���ߴ� tile�� ��ȯ�� ���� tile match Ȯ��.
                    if(PlayManager.inst.emptyTileCount >= 3)
                        PlayManager.inst.CalcScoreAndTimer();
                    PlayManager.inst.emptyTileCount = 0;
                }
                else // �ƴ� ���
                {
                    previousTile.GetComponent<Tile>().Deselect(); // �� Ŭ�� ���
                    Select(); // ���� Ŭ�� Ȱ��ȭ.
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
        if (render.sprite == _render.sprite) // �� sprite�� �ű� sprite�� ���� ��� return
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
            return hit.collider.gameObject; // Ž�� ������Ʈ�� ���� ��� �ش� gameobject return
        }

        return null; // �ƹ��͵� Ž�� ���� �ÿ��� null return
    }
    private List<GameObject> GetAllAdjcentTiles()
    {
        List<GameObject> adjcentTiles = new List<GameObject>();
        myColider.enabled = false;
        for (int i = 0; i < adjacentDir.Length; i++)
            adjcentTiles.Add(GetAdjcent(adjacentDir[i])); // U,D,L,R 4���� ���� ������Ʈ üũ

        myColider.enabled = true;
        return adjcentTiles;
    }

    private List<GameObject> FindMatch(Vector2 _dir) // ���� Sprite ã�� �޼ҵ�
    {
        List<GameObject> matchingTiles = new List<GameObject>();
        myColider.enabled = false;
        Collider2D co_Temp = null; // ���� hit.colider�� ���� �ӽú���
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _dir);

        // ���� sprite �ƴϰų� �� �����϶����� 
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
        if(matchingTiles.Count >= 2) // �ڱ��ڽ� �����ϰ� 2�� ��
        {
            for (int i = 0; i < matchingTiles.Count; i++)
                matchingTiles[i].GetComponent<SpriteRenderer>().sprite = null; // �׸� �����ֱ�.

            matchFound = true;
        }    
    }
    public void ClearAllMatches()
    {
        if (render.sprite == null)
            return;    

        //���� and ���� 3match ������� ã��.
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
