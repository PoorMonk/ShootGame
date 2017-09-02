using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskManage : MonoBehaviour {

    public GameObject m_PrefabDisk;

    private Transform m_Transform;

	// Use this for initialization
	void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 开始创建飞盘
    /// </summary>
    public void BeginCreateDisk()
    {
        InvokeRepeating("CreateDisk", 2.0f, 2.0f);
    }

    public void StopCreateDisk()
    {
        CancelInvoke("CreateDisk");
    }

    /// <summary>
    /// 每次创建三个飞盘
    /// </summary>
    void CreateDisk()
    {
        for(int i = 0; i < 3; ++i)
        {
            GameObject gDisk = GameObject.Instantiate(m_PrefabDisk, new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(1.0f, 3.0f), Random.Range(5.0f, 13.0f)), Quaternion.identity);
            gDisk.GetComponent<Transform>().SetParent(m_Transform);
        }
    }

    /// <summary>
    /// 游戏结束后销毁剩余的飞盘
    /// </summary>
    public void DestroyRestDisk()
    {
        Transform[] disk = m_Transform.GetComponentsInChildren<Transform>();
        for(int i = 1; i < disk.Length; ++i)
        {
            Destroy(disk[i].gameObject);
        }
    }
}
