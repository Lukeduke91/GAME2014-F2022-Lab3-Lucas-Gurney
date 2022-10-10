using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
public enum BulletDirection
{
    UP,
    RIGHT,
    DOWN,
    LEFT
}

[System.Serializable]
public struct ScreenBounds
{
    public Boundary horizontal;
    public Boundary vertical;
}
public class BulletBehaviour : MonoBehaviour
{
    [Header("Bullet Properties")]
    public BulletDirection direction;
    public float speed;
    public ScreenBounds bounds;
    private Vector3 velocity;
    public BulletManager bulletManager;
    // Start is called before the first frame update
    void Start()
    {
        SetDirection(direction);
        bulletManager = FindObjectOfType<BulletManager>();
    }
    public void SetDirection(BulletDirection direction)
    {
        switch (direction)
        {
            case BulletDirection.UP:
                velocity = Vector3.up * speed * Time.deltaTime;
                break;
            case BulletDirection.RIGHT:
                velocity = Vector3.right * speed * Time.deltaTime;
                break;
            case BulletDirection.DOWN:
                velocity = Vector3.down * speed * Time.deltaTime;
                break;
            case BulletDirection.LEFT:
                velocity = Vector3.left * speed * Time.deltaTime;
                break;

        }
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBounds();
    }
    private void Move()
    {
        transform.position += velocity * Time.deltaTime;
    }
    void CheckBounds()
    {
        if((transform.position.x > bounds.horizontal.max) || 
            (transform.position.x > bounds.horizontal.min) ||
            (transform.position.y > bounds.vertical.max) ||
            (transform.position.y > bounds.vertical.min))
        {
            bulletManager.ReturnBullet(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        bulletManager.ReturnBullet(this.gameObject);
    }
}
