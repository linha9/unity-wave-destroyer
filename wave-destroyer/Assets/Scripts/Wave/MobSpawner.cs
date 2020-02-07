using UnityEngine;
using UnityEngine.Events;

public class MobSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject mobPrefab;
    [SerializeField]
    private Dir direction;

    public GameObject Spawn()
    {
        return EnemyController.Create(mobPrefab, transform.position, Direction.dirToFloat(direction));
    }
}
