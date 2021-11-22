using System.Collections.Generic;
using UnityEngine;

//kinda ranning out of time for the test here,
//so it's a very stupid vfx system, but it works :)
public class ThunderControl : MonoBehaviour
{
    public List<GameObject> thunderPrefabs;
    private float intensity = 0f;
    private float spawnCooldown;
    public float distRange = 5f;
    public void SetThunder(float val)
    {
        intensity = val;
        spawnCooldown = 0f;
    }
    private void Update()
    {
        if (intensity < 0.1f) return;

        spawnCooldown += Time.deltaTime;
        if (spawnCooldown > (0.1f / intensity))
        {
            spawnCooldown = 0f;
            CreateThunder();
        }
    }
    private void CreateThunder()
    {
        var prefab = thunderPrefabs.GetRandomElement();
        var thunder = Instantiate(prefab, transform);
        thunder.transform.position = new Vector3(Random.Range(-1f, 1f) * distRange, 0f, Random.Range(-1f, 1f) *distRange);
        thunder.SetActive(true);
    }
}
