using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [Header("Bullet Properties")]
    public Queue<GameObject> bulletPool;
    public GameObject bulletPrefab;
    //[Range(10, 200)]
    public int bulletNumber = 50;
    public int bulletCount = 0;
    public int activeBullets = 0;

    public Transform bulletParent;
    // Start is called before the first frame update
    void Start()
    {
        bulletPool = new Queue<GameObject>();
        BuildBulletPool();
    }

    void BuildBulletPool()
    {
        for(int i = 0; i < bulletNumber; i++)
        {
            CreateBullets();
        }

        bulletCount = bulletPool.Count;
    }
    private void CreateBullets()
    {
        var bullet = Instantiate(bulletPrefab);
        bullet.SetActive(false);
        bullet.transform.SetParent(bulletParent);
        bulletPool.Enqueue(bullet);
    }
    public GameObject GetBullet(Vector2 position, BulletDirection direction)
    {
        if(bulletPool.Count < 1)
        {
            CreateBullets();
        }

        var bullet = bulletPool.Dequeue();
        bullet.SetActive(true);
        bullet.transform.position = position;
        bullet.GetComponent<BulletBehaviour>().SetDirection(direction);
        //bullet.GetComponent<BulletBehaviour>().SetDirection(direction);

        bulletCount = bulletPool.Count;
        activeBullets++;

        return bullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);

        bulletCount = bulletPool.Count;
        activeBullets--;
    }
}
