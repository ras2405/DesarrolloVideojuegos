using UnityEngine;

public class Tree : MonoBehaviour
{
    public Transform playerTransform;
    private SpriteRenderer spriteRenderer;
    public Transform playerBase;
    public Transform treeBase;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (playerBase != null && treeBase != null)
        {
            int playerSortingOrder = playerTransform.GetComponent<SpriteRenderer>().sortingOrder;

            bool isPlayerAbove = playerBase.position.y < treeBase.position.y;

            if (isPlayerAbove)
            {
                spriteRenderer.sortingOrder = playerSortingOrder - 3;
            }
            else
            {
                spriteRenderer.sortingOrder = playerSortingOrder + 3;
            }
        }
    }
}