using UnityEngine;

public class DropSpawnerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject drop;
    private float spawnRate;
    private float timer;

    private void Awake()
    {
        spawnRate = 2;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Instantiate(drop, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
            timer = 0;
        }
    }
}