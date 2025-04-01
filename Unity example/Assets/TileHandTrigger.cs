using UnityEngine;

public class TileHandTrigger : MonoBehaviour
{
    private Tile tile;

    void Start()
    {
        tile = GetComponent<Tile>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finger"))
        {
            tile.Highlight();
        }
    }
}
