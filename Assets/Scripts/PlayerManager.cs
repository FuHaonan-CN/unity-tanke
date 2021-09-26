using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    // 属性值
    public int lifeValue = 3;
    public int playerScore = 0;
    public bool isDead;
    public bool isDefeat;

    // 引用
    public GameObject born;
    public Text playerScoreTex;
    public Text playerLifeValueTex;
    public GameObject isDefeatUI;
    
    
    // 单例
    private static PlayerManager instance;

    public static PlayerManager Instance
    {
        get => instance;
        set => instance = value;
    }

    private void Awake()
    {
        Instance = this;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDefeat)
        {
            isDefeatUI.SetActive(true);
            return;
        }
        
        // 监听程序，如果死亡，调用复活方法
        if (isDead)
        {
            Recover();
        }

        playerLifeValueTex.text = lifeValue.ToString();
        playerScoreTex.text = playerScore.ToString();
        
    }

    private void Recover()
    {
        if (lifeValue<=0)
        {
            // 游戏失败 返回主界面
            isDefeat = true;

        }
        else
        {
            // 复活
            lifeValue--;
            GameObject go = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayer = true;
            isDead = false;
            
        }
    }
}
