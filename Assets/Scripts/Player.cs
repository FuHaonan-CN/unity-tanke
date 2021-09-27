using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /* 属性值 */
    public float moveSpeed = 3;
    private Vector3 bulletEulerAngles;
    private float timeVal;  // 计时器
    private float defendTimeVal=3;  // 无敌计时器
    private bool isDefended=true;

    /* 引用 */
    private SpriteRenderer sr;
    // 上 右 下 左
    public Sprite[] tankSprite;
    // 子弹的预设值
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject defendEffectPrefab;
    public AudioSource moveAudio;
    public AudioClip[] tankAudio;
    
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
        // 判断是否处于无敌状态
        if (isDefended)
        {
            defendEffectPrefab.SetActive(true);
            defendTimeVal -= Time.deltaTime;
            if (defendTimeVal<=0)
            {
                isDefended = false;
                defendEffectPrefab.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (PlayerManager.Instance.isDefeat)
        {
            // 游戏失败
            return;
        }
        
        // 设置发射子弹的间隔
        if (timeVal >= 0.4f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.fixedDeltaTime;
        }
        
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
        float v = Input.GetAxisRaw("Vertical");
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

        if (Mathf.Abs(v)>0.05f)
        {
            moveAudio.clip = tankAudio[1];
            // 不判断会一直播放 刺耳
            if (moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
        
        // 设置优先级,避免斜着走
        if (v != 0)
        {
            return;
        }
        
        float h = Input.GetAxisRaw("Horizontal");
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
        if (Mathf.Abs(h)>0.05f)
        {
            moveAudio.clip = tankAudio[1];
            // 不判断会一直播放 刺耳
            if (moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
        else
        {
            moveAudio.clip = tankAudio[0];
            // 不判断会一直播放 刺耳
            if (moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
    }
    // 坦克的死亡方法
    private void Die()
    {
        // 判断是否无敌
        if (isDefended)
        {
            return;
        }

        PlayerManager.Instance.isDead = true;
        
        // 产生爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        // 死亡
        Destroy(gameObject);
    }
}