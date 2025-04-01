using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject cube;
    public GameObject InGame;
    public ElephantGameManager elephantGameManager;

    void Start()
    {
        InGame.SetActive(false);
        StartCoroutine(CubeFall());
    }

    IEnumerator CubeFall()
    {
        yield return new WaitForSeconds(1.5f);
        cube.SetActive(false);
        InGame.SetActive(true);

        // 🕒 延迟 0.5 秒后再触发小象跳跃
        yield return new WaitForSeconds(0.5f);
        elephantGameManager.TriggerElephantJump();
    }
}

