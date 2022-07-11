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
                for(int i = 0; i < spawnsPerTime; i++)
                {
                    int rand = Random.Range(0, lowPrefab.Length);
                    var prefab = lowPrefab[rand];
                    var size = prefab.GetComponent<Collider>().bounds.size;
                    Ray ray = new Ray(transform.position, Vector3.down);
                    RaycastHit hit;
                    Physics.Raycast(ray, out hit, 10, LayerMask.GetMask(Utilities.groundLayer));
                    Vector3 ground = hit.point;
                    Debug.DrawLine(ground, ground + Vector3.up * 100, Color.yellow);
                    float y = ground.y + size.y / 2;
                    int iteration = 0;
                    while(iteration < 10)
                    {
                        float randRadius = Random.Range(minRadius, maxRadius);
                        Vector2 randPosition2D = Random.insideUnitCircle;
                        float x = ground.x + randPosition2D.x * randRadius;
                        float z = ground.z + randPosition2D.y * randRadius;
                        Vector3 spawnPosition = new Vector3(x, y, z);
                        if (Physics.CheckBox(spawnPosition, size / 2, Quaternion.identity, LayerMask.GetMask(Utilities.objectLayer, Utilities.characterLayer)))
                        {
                            iteration++;
                            continue;
                        }
                        if (!Physics.CheckBox(spawnPosition, size, Quaternion.identity, LayerMask.GetMask(Utilities.groundLayer)))
                        {
                            iteration++;
                            continue;
                        }
                        Instantiate(prefab, spawnPosition, Quaternion.identity);
                        break;
                    }
                    
                }
            }
            else if(level == Level.Medium)
            {
                for (int i = 0; i < spawnsPerTime; i++)
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
                    Debug.DrawLine(ground, ground + Vector3.up * 100, Color.yellow);

                    float y = ground.y + size.y / 2;
                    int iteration = 0;
                    while (iteration < 10)
                    {
                        float randRadius = Random.Range(minRadius, maxRadius);
                        Vector2 randPosition2D = Random.insideUnitCircle;
                        float x = ground.x + randPosition2D.x * randRadius;
                        float z = ground.z + randPosition2D.y * randRadius;
                        Vector3 spawnPosition = new Vector3(x, y, z);
                        if (Physics.CheckBox(spawnPosition, size / 2, Quaternion.identity, LayerMask.GetMask(Utilities.objectLayer, Utilities.characterLayer)))
                        {
                            iteration++;
                            continue;
                        }
                        if (!Physics.CheckBox(spawnPosition, size, Quaternion.identity, LayerMask.GetMask(Utilities.groundLayer)))
                        {
                            iteration++;
                            continue;
                        }
                        Instantiate(prefab, spawnPosition, Quaternion.identity);
                        break;
                    }
                }
            }
            else if(level==Level.High)
            {
                for (int i = 0; i < spawnsPerTime; i++)
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
                    Debug.DrawLine(ground, ground + Vector3.up * 100, Color.yellow);

                    float y = ground.y + size.y / 2;
                    int iteration = 0;
                    while (iteration < 10)
                    {
                        float randRadius = Random.Range(minRadius, maxRadius);
                        Vector2 randPosition2D = Random.insideUnitCircle;
                        float x = ground.x + randPosition2D.x * randRadius;
                        float z = ground.z + randPosition2D.y * randRadius;
                        Vector3 spawnPosition = new Vector3(x, y, z);
                        if (Physics.CheckBox(spawnPosition, size / 2, Quaternion.identity, LayerMask.GetMask(Utilities.objectLayer, Utilities.characterLayer)))
                        {
                            iteration++;
                            continue;
                        }
                        if (!Physics.CheckBox(spawnPosition, size, Quaternion.identity, LayerMask.GetMask(Utilities.groundLayer)))
                        {
                            iteration++;
                            continue;
                        }
                        Instantiate(prefab, spawnPosition, Quaternion.identity);
                        break;
                    }
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
