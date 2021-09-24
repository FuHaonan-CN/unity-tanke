using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 属性值
    public float moveSpeed = 3;
    private Vector3 bulletEulerAngles;
    // 计时器
    private float timeVal;

    // 引用
    private SpriteRenderer sr;
    // 上 右 下 左
    public Sprite[] tankSprite;
    // 子弹的预设值
    public GameObject bulletPrefab;
    public GameObject explosion;

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
        // 设置发射子弹的间隔
        if (timeVal >= 0.4f)
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 子弹产生的角度：当前坦克的角度+子弹应该旋转的角度
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles+bulletEulerAngles));
            
            // 子弹计时器归零
            timeVal = 0;
        }
    }
    // 坦克的移动方法
    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        
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

        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);

        // 设置优先级,避免斜着走
        if (h != 0)
        {
            return;
        }

        float v = Input.GetAxisRaw("Vertical");
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

        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
    }
    // 坦克的死亡方法
    private void Die()
    {
        // 产生爆炸特效
        // 死亡
    }
}