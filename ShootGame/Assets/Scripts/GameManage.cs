using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour {

    /// <summary>
    /// 定义游戏状态枚举
    /// </summary>
    public enum GameStatus
    {
        GAMESTART,
        GAMEBEING,
        GAMEEND
    }

    private GameObject m_StartUI;
    private GameObject m_GameUI;
    private GameObject m_EndUI;

    private GameStatus m_status;

    private AudioSource m_bgAudioSource;
    private DiskManage m_DiskManage;

    private int m_score = 0;
    private float m_time = 20.0f;

    private GUIText m_textScore;
    private GUIText m_textTotalScore;
    private GUIText m_textTime;


    // Use this for initialization
    void Start () {
        m_StartUI = GameObject.Find("StartUI");
        m_GameUI  = GameObject.Find("GameUI");
        m_EndUI   = GameObject.Find("EndUI");

        m_bgAudioSource = GameObject.Find("Main Camera").gameObject.GetComponent<AudioSource>();
        m_DiskManage = GameObject.Find("DiskParent").gameObject.GetComponent<DiskManage>();
        m_textScore = GameObject.Find("GameScore").GetComponent<GUIText>();
        m_textTime = GameObject.Find("GameTime").GetComponent<GUIText>();
        m_textTotalScore = GameObject.Find("GameTotalScore").GetComponent<GUIText>();
        //m_status = GameStatus.GAMESTART;
        ChangeGameStatus(GameStatus.GAMESTART);
	}
	
	// Update is called once per frame
	void Update () {
        if(m_status == GameStatus.GAMEBEING)
        {
            Countdown();
        }
    }

    /// <summary>
    /// 切换游戏状态
    /// </summary>
    /// <param name="status"></param>
    public void ChangeGameStatus(GameStatus status)
    {
        m_status = status;

        if (m_status == GameStatus.GAMESTART)
        {
            m_StartUI.SetActive(true);
            m_GameUI.SetActive(false);
            m_EndUI.SetActive(false);

            m_bgAudioSource.Stop();
            m_DiskManage.StopCreateDisk();

            m_score = 0;
            m_time = 20.0f;
        }
        else if (m_status == GameStatus.GAMEBEING)
        {
            m_StartUI.SetActive(false);
            m_GameUI.SetActive(true);
            m_EndUI.SetActive(false);

            m_bgAudioSource.Play();
            m_DiskManage.BeginCreateDisk();

            ShowScore();
        }
        else if (m_status == GameStatus.GAMEEND)
        {
            m_StartUI.SetActive(false);
            m_GameUI.SetActive(false);
            m_EndUI.SetActive(true);

            m_bgAudioSource.Stop();
            m_DiskManage.StopCreateDisk();

            m_textTotalScore.text = "总分数：" + m_score;

            m_DiskManage.DestroyRestDisk();
        }
    }

    /// <summary>
    /// 获取当前游戏状态
    /// </summary>
    /// <returns></returns>
    public GameStatus GetGameStatus()
    {
        return m_status;
    }

    /// <summary>
    /// 增加分数
    /// </summary>
    public void AddScore()
    {
        m_score++;
        ShowScore();
    }

    /// <summary>
    /// 界面显示当前分数
    /// </summary>
    public void ShowScore()
    {
        m_textScore.text = "分数：" + m_score;
    }

    /// <summary>
    /// 开始倒计时,时间到了之后切换到结束画面
    /// </summary>
    public void Countdown()
    {
        m_time -= Time.deltaTime;
        m_textTime.text = "时间：" + m_time + "S";

        if (m_time <= 0.0f)
        {
            ChangeGameStatus(GameStatus.GAMEEND);
        }

    }
}
