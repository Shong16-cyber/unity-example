using System.Collections;
using UnityEngine;

public class GameController2 : MonoBehaviour
{
    public GameObject cube;
    public GameObject InGame;
    public GameObject elephant;  // 小象对象
    public float jumpDuration = 1f;

    void Start()
    {
        InGame.SetActive(false);  // 确保展开图最开始是关闭的
        StartCoroutine(CubeFall());
    }

    IEnumerator CubeFall()
    {
        // 等待5秒
        yield return new WaitForSeconds(3f);

        // 隐藏Cube并显示展开图
        cube.SetActive(false);
        InGame.SetActive(true);

        // 延迟0.5秒后开始Cube的跳跃
        yield return new WaitForSeconds(0.5f);

        // 触发Cube跳跃动画
        StartCoroutine(CubeJumpToElephant());
    }

    IEnumerator CubeJumpToElephant()
    {
        Vector3 start = cube.transform.position;  // Cube的起始位置
        Vector3 target = elephant.transform.position + new Vector3(0f, 1f, 0f);  // 小象的头顶位置

        float time = 0f;

        // 让Cube从起始位置到目标位置平滑过渡
        while (time < jumpDuration)
        {
            cube.transform.position = Vector3.Lerp(start, target, time / jumpDuration);
            time += Time.deltaTime;
            yield return null;
        }

        // 确保Cube停留在目标位置
        cube.transform.position = target;
    }
}

