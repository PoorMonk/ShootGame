using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    private Ray m_Ray;
    private RaycastHit m_RaycastHit;

    private Transform m_Transform;
    private Transform m_point;

    private LineRenderer m_LineRenderer;
    private GameManage m_GameManage;

    private GameObject m_DiskRepeat = null;

    public AudioSource m_AudioSource;
	// Use this for initialization
	void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
        m_point = m_Transform.Find("point");
        m_LineRenderer = m_point.gameObject.GetComponent<LineRenderer>();
        m_AudioSource = gameObject.GetComponent<AudioSource>();
        m_GameManage = GameObject.Find("UI").gameObject.GetComponent<GameManage>();
	}
	
	// Update is called once per frame
	void Update () {
        if(m_GameManage.GetGameStatus() == GameManage.GameStatus.GAMEBEING)
        {
            m_Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(m_Ray, out m_RaycastHit))
            {
                //Debug.DrawLine(m_point.position, m_RaycastHit.point, Color.red);
                m_Transform.LookAt(m_RaycastHit.point);
                m_LineRenderer.SetPosition(0, m_point.position);
                m_LineRenderer.SetPosition(1, m_RaycastHit.point);
            }

            if (m_RaycastHit.collider.tag == "Disk" && Input.GetMouseButtonDown(0))
            {
                m_AudioSource.Play();

                Transform parent = m_RaycastHit.transform.parent;
                Transform[] TDisk = parent.GetComponentsInChildren<Transform>();
                for (int i = 0; i < TDisk.Length; ++i)
                {
                    TDisk[i].gameObject.AddComponent<Rigidbody>();
                }

                GameObject.Destroy(parent.gameObject, 2.0f);

                if(m_DiskRepeat == null)
                {
                    m_GameManage.AddScore();
                    
                }
                else if(m_DiskRepeat != parent.gameObject)
                {
                    m_GameManage.AddScore();
                }

                m_DiskRepeat = parent.gameObject;
            }
        }
        
	}
}
