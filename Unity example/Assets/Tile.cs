using UnityEngine;

public class Tile : MonoBehaviour
{
    public Renderer tileRenderer;
    public Color highlightColor = new Color(0.3f, 0.6f, 1f); // 天蓝色
    private Color originalColor;
    private bool isHighlighted = false;

    private ElephantGameManager gameManager;

    void Start()
    {
        if (tileRenderer == null)
            tileRenderer = GetComponent<Renderer>();

        originalColor = tileRenderer.material.color;

        // 自动找到场景中的 ElephantGameManager
        gameManager = FindObjectOfType<ElephantGameManager>();
    }

    public void Highlight()
    {
        if (isHighlighted) return;
        isHighlighted = true;
        tileRenderer.material.color = highlightColor;
    }

    public void ResetColor()
    {
        tileRenderer.material.color = originalColor;
        isHighlighted = false;
    }

    // 闪红特效
    public void FlashWrongColor()
    {
        tileRenderer.material.color = Color.red;
        Invoke("ResetColor", 0.3f); // 闪烁 0.3 秒
    }

    public bool IsDisabled()
    {
        return tileRenderer.material.color == Color.gray;
    }

    void OnMouseDown()
    {
        Highlight();

        // 通知 ElephantGameManager：我被选中了
        if (gameManager != null)
        {
            gameManager.OnTileSelected(this);
        }
    }

    // 禁用 tile
    public void DisableTile()
    {
        tileRenderer.material.color = Color.gray;
    }
}

