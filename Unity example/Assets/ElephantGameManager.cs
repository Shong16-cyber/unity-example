using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantGameManager : MonoBehaviour
{
    public GameObject elephant;
    public Tile[] tiles; // 顺序必须和立方体展开图一致！
    public float elephantYOffset = 0.2f;
    public float jumpHeight = 0.5f;
    public float jumpDuration = 0.6f;

    private Tile currentTile;
    private Tile lastCorrectTile;
    private int correctCount = 0;

    private bool isJumping = false;

    void Start()
    {
        StartNewStep();
    }

    public void StartNewStep()
    {
        List<Tile> candidates = new List<Tile>(tiles);

        // 排除上一次正确点击的 tile 及其对面
        if (lastCorrectTile != null)
        {
            candidates.Remove(lastCorrectTile);
            Tile opposite = GetOppositeTile(lastCorrectTile);
            if (opposite != null)
                candidates.Remove(opposite);
        }

        if (candidates.Count == 0)
        {
            Debug.Log("🎉 全部完成！");
            return;
        }

        // 选择一个新 tile
        currentTile = candidates[Random.Range(0, candidates.Count)];

        // 移动小象
        TriggerElephantJump();
    }

    public void TriggerElephantJump()
    {
        if (elephant != null && currentTile != null && !isJumping)
            StartCoroutine(JumpToTile(currentTile));
    }

    private IEnumerator JumpToTile(Tile targetTile)
    {
        isJumping = true;

        Vector3 start = elephant.transform.position;
        Vector3 end = targetTile.transform.position + Vector3.up * elephantYOffset;

        float elapsed = 0f;
        while (elapsed < jumpDuration)
        {
            float t = elapsed / jumpDuration;
            float height = Mathf.Sin(Mathf.PI * t) * jumpHeight;
            elephant.transform.position = Vector3.Lerp(start, end, t) + Vector3.up * height;
            elapsed += Time.deltaTime;
            yield return null;
        }

        elephant.transform.position = end;
        isJumping = false;
    }

    public void OnTileSelected(Tile selectedTile)
    {
        if (isJumping || selectedTile.IsDisabled()) return;

        Tile correctTile = GetOppositeTile(currentTile);
        if (correctTile == null) return;

        if (selectedTile == correctTile)
        {
            Debug.Log("✅ Correct!");
            selectedTile.DisableTile(); // 变灰 + 不可点击
            currentTile.DisableTile();  // 当前 tile 也不可再被跳过去
            lastCorrectTile = currentTile;
            correctCount++;

            if (correctCount >= 3)
                Debug.Log("🎉 成功通关！");
            else
                StartNewStep();
        }
        else
        {
            Debug.Log("❌ 错了，请再试一次！");
            selectedTile.FlashWrongColor();
        }
    }

    private Tile GetOppositeTile(Tile tile)
    {
        int index = System.Array.IndexOf(tiles, tile);

        // 新的对立面逻辑
        switch (index)
        {
            case 0: return tiles[3]; // 绿色对蓝色
            case 1: return tiles[4]; // 红色对黄色
            case 2: return tiles[5]; // 紫色对肉色
            case 3: return tiles[0]; // 蓝色对绿色
            case 4: return tiles[1]; // 黄色对红色
            case 5: return tiles[2]; // 肉色对紫色
            default: return null;
        }
    }
}

