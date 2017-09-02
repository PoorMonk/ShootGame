using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReStart : MonoBehaviour {

    private GameManage m_GameManage;

	// Use this for initialization
	void Start () {
        m_GameManage = GameObject.Find("UI").GetComponent<GameManage>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        m_GameManage.ChangeGameStatus(GameManage.GameStatus.GAMESTART);
    }
}
