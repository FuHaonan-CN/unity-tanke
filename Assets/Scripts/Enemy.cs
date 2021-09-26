using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    /* 属性值 */
    public float moveSpeed = 3;
    private Vector3 bulletEulerAngles;
    private float v=-1; // 敌人移动的竖直方向
    private float h; // 敌人移动的水平方向
    
    private float timeVal; // 计时器
    private float timeValChangeDirection; // 敌人移动的计时器

    /* 引用 */
    private SpriteRenderer sr;
    // 上 右 下 左
    public Sprite[] tankSprite;
    // 子弹的预设值
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // 自动发射子弹的间隔
        if (timeVal >= 3f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    // 坦克的攻击方法
    private void Attack()
    {
        // 子弹产生的角度：当前坦克的角度+子弹应该旋转的角度
        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));

        // 子弹计时器归零
        timeVal = 0;
    }

    // 坦克的移动方法
    private void Move()
    {
        if (timeValChangeDirection >= 4)
        {
            int num = Random.Range(0, 8);
            if (num > 5)
            {
                // 向下
                v = -1;
                h = 0;
            }
            else if (num == 0)
            {
                // 向上
                v = 1;
                h = 0;
            }
            else if (num > 0 && num <= 2)
            {
                // 向左
                h = -1;
                v = 0;
            }
            else if (num > 2 && num <= 4)
            {
                // 向右
                h = 1;
                v = 0;
            }
            // 重置时间，不然敌人会一直旋转
            timeValChangeDirection = 0;
        }
        else
        {
            timeValChangeDirection += Time.fixedDeltaTime;
        }

        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0)
        {
            // 下
            sr.sprite = tankSprite[2];
            bulletEulerAngles = new Vector3(0, 0, -180);
        }
        else if (v > 0)
        {
            // 上
            sr.sprite = tankSprite[0];
            bulletEulerAngles = new Vector3(0, 0, 0);
        }
        
        // 设置优先级,避免斜着走
        if (v != 0)
        {
            return;
        }
        
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0)
        {
            // 左
            sr.sprite = tankSprite[3];
            bulletEulerAngles = new Vector3(0, 0, 90);
        }
        else if (h > 0)
        {
            // 右
            sr.sprite = tankSprite[1];
            bulletEulerAngles = new Vector3(0, 0, -90);
        }

    }

    // 坦克的死亡方法
    private void Die()
    {
        PlayerManager.Instance.playerScore++;
        
        // 产生爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        // 死亡
        Destroy(gameObject);
    }

    // 如果有碰撞 则旋转
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Enemy")
        {
            timeValChangeDirection = 4;
        }
    }
}