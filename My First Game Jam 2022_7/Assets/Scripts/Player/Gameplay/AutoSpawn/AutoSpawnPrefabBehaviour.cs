using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SanityController))]
public class AutoSpawnPrefabBehaviour : MonoBehaviour
{
    public enum Level : int
    {
        None = 1,
        Low = 2,
        Medium = 3,
        High = 4
    }
    public GameObject[] lowPrefab;
    public GameObject[] mediumPrefab;
    public GameObject[] highPrefab;
    public int spawnsPerTime;
    public float spawnIntervalInSeconds;
    public float maxRadius;
    public float minRadius;
    public Level level;
    [SerializeField] private bool displayGizmos;
    private SanityController _sanity;
    private void Awake()
    {
        _sanity = GetComponent<SanityController>();
    }
    private void Start()
    {
        level = Level.None;
        StartAutoSpawnCoroutine();
    }
    public void Update()
    {
        switch(_sanity.level)
        {
            case SanityController.Level.Max:
                level = Level.None;
                break;
            case SanityController.Level.High:
                level = Level.Low;
                break;
            case SanityController.Level.Medium:
                level = Level.Medium;
                break;
            case SanityController.Level.Low:
                level = Level.High;
                break;
            default:
                level = Level.None;
                break;
        }
    }
    public void StartAutoSpawnCoroutine() => StartCoroutine(AutoSpawnCoroutine());
    private IEnumerator AutoSpawnCoroutine()
    {
        while(true)
        {
            if (level == Level.Low)
            {
                for(int i = 0; i < spawnsPerTime * (int)level; i++)
                {
                    int rand = Random.Range(0, lowPrefab.Length);
                    var prefab = lowPrefab[rand];
                    var size = prefab.GetComponent<Collider>().bounds.size;
                    Ray ray = new Ray(transform.position, Vector3.down);
                    RaycastHit hit;
                    Physics.Raycast(ray, out hit, 10, LayerMask.GetMask(Utilities.groundLayer));
                    Vector3 ground = hit.point;
                    Vector3 center = ground + Vector3.up * size.y / 2;
                    LayerMask avoidMask = LayerMask.GetMask(Utilities.objectLayer, Utilities.characterLayer);
                    Vector3 spawnPosition = Utilities.GetRandomPointOnGround(minRadius, maxRadius, center, size, avoidMask);
                    if(spawnPosition != Vector3.positiveInfinity)
                    {
                        Debug.DrawLine(spawnPosition, spawnPosition + Vector3.up * 100, Color.green, 2);
                        Instantiate(prefab, spawnPosition, Quaternion.identity);
                    }
                    yield return new WaitForSeconds(0.5f / (int)level);
                }
            }
            else if(level == Level.Medium)
            {
                for (int i = 0; i < spawnsPerTime * (int)level; i++)
                {
                    int randLevel = Random.Range(0, 2);
                    int rand = 0;
                    GameObject prefab = null;
                    if (randLevel == 0)
                    {
                        rand = Random.Range(0, lowPrefab.Length);
                        prefab = lowPrefab[rand];
                    }
                    else if (randLevel == 1)
                    {
                        rand = Random.Range(0, mediumPrefab.Length);
                        prefab = mediumPrefab[rand];
                    }
                    var size = prefab.GetComponent<Collider>().bounds.size;
                    Ray ray = new Ray(transform.position, Vector3.down);
                    RaycastHit hit;
                    Physics.Raycast(ray, out hit, 10, LayerMask.GetMask(Utilities.groundLayer));
                    Vector3 ground = hit.point;
                    Vector3 center = ground + Vector3.up * size.y / 2;
                    LayerMask avoidMask = LayerMask.GetMask(Utilities.objectLayer, Utilities.characterLayer);
                    Vector3 spawnPosition = Utilities.GetRandomPointOnGround(minRadius, maxRadius, center, size, avoidMask);
                    if (spawnPosition != Vector3.positiveInfinity)
                    {
                        Debug.DrawLine(spawnPosition, spawnPosition + Vector3.up * 100, Color.green, 2);
                        Instantiate(prefab, spawnPosition, Quaternion.identity);
                    }
                    yield return new WaitForSeconds(0.5f / (int)level);
                }
            }
            else if(level==Level.High)
            {
                for (int i = 0; i < spawnsPerTime * (int)level; i++)
                {
                    int randLevel = Random.Range(0, 3);
                    int rand = 0;
                    GameObject prefab = null;
                    if (randLevel == 0)
                    {
                        rand = Random.Range(0, lowPrefab.Length);
                        prefab = lowPrefab[rand];
                    }
                    else if (randLevel == 1)
                    {
                        rand = Random.Range(0, mediumPrefab.Length);
                        prefab = mediumPrefab[rand];
                    }
                    else if (randLevel == 2)
                    {
                        rand = Random.Range(0, highPrefab.Length);
                        prefab = highPrefab[rand];
                    }
                    var size = prefab.GetComponent<Collider>().bounds.size;
                    Ray ray = new Ray(transform.position, Vector3.down);
                    RaycastHit hit;
                    Physics.Raycast(ray, out hit, 10, LayerMask.GetMask(Utilities.groundLayer));
                    Vector3 ground = hit.point;
                    Vector3 center = ground + Vector3.up * size.y / 2;
                    LayerMask avoidMask = LayerMask.GetMask(Utilities.objectLayer, Utilities.characterLayer);
                    Vector3 spawnPosition = Utilities.GetRandomPointOnGround(minRadius, maxRadius, center, size, avoidMask);
                    if (spawnPosition != Vector3.positiveInfinity)
                    {
                        Debug.DrawLine(spawnPosition, spawnPosition + Vector3.up * 100, Color.green, 2);
                        Instantiate(prefab, spawnPosition, Quaternion.identity);
                    }
                    yield return new WaitForSeconds(0.5f / (int)level);
                }
            }
            yield return null;
            var waitInSeconds = new WaitForSeconds(spawnIntervalInSeconds / (int)level);
            yield return waitInSeconds;
        }
    }

    private void OnDrawGizmos()
    {
        if (!displayGizmos) { return; }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, maxRadius);
    }
}
