﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System;
using System.Data;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class player_scenario_class
{
    public player_scenario_class(string foreign_scenario, int? next_npc_scenario_number){
        this.foreign_scenario = foreign_scenario;
        this.next_npc_scenario_number = next_npc_scenario_number;
    }
    private string foreign_scenario;
    private int? next_npc_scenario_number;
    public string get_foreign_scenario(){
        return foreign_scenario;
    }
    public int? get_next_npc_scenario_number(){
        return next_npc_scenario_number;
    }
}
public class npc_scenario_class
{
    public npc_scenario_class(string foreign_scenario, int? next_player_scenario_number){
        this.foreign_scenario = foreign_scenario;
        this.next_player_scenario_number = next_player_scenario_number;
    }
    private string foreign_scenario;
    private int? next_player_scenario_number;
    public string get_foreign_scenario(){
        return foreign_scenario;
    }
    public int? get_next_player_scenario_number(){
        return next_player_scenario_number;
    }
}

public class testdbscript : MonoBehaviour
{
    public Transform playerbuttonprefab;
    public Transform npcbuttonprefab;
    private int MAX_PLAYER_SCENARIO_NUMBER;
    private int MAX_SUB_PLAYER_SCENARIO_NUMBER;
    private int MAX_NPC_SCENARIO_NUMBER;
    private int world_id = 1;
    private int scene_id = 1;
    private int situation_id = 3;
    private int? scenario_number = 1;
    private int? scenario_number_2;
    private int current_button_number;
    IDbConnection dbconn;
    string sqlQuery;
    player_scenario_class[][] player_foreign_scenario_arr;
    npc_scenario_class[] npc_foreign_scenario_arr;

    public void npcdb()
    {
        scenario_number_2 = scenario_number;
        int npc_scenario_count = 0;
        MAX_PLAYER_SCENARIO_NUMBER = 0;
        MAX_SUB_PLAYER_SCENARIO_NUMBER = 0;
        MAX_NPC_SCENARIO_NUMBER = 0;
        string conn = @"Data Source=" + Application.dataPath + "/Database/0607.db";
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();

        GameObject.Find("Canvas").transform.Find("ExitButton").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("ConversationPanel").gameObject.SetActive(true);
        GameObject.Find("Director").GetComponent<LeftUIOpenCloseScript>().PanelOpenorClose();

        IDbCommand dbcmd = dbconn.CreateCommand();
        sqlQuery = "select count(*) from NPCScenarioNumber where world_id = " + world_id + " and scene_id = " + scene_id + " and situation_id = " + situation_id;
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        reader.Read();
        MAX_NPC_SCENARIO_NUMBER = reader.GetInt32(0);
        reader.Close();
        sqlQuery = "select distinct scenario_number from PlayerScenarioNumber where world_id = " + world_id + " and scene_id = " + scene_id + " and situation_id = " + situation_id;
        MAX_PLAYER_SCENARIO_NUMBER = CountMAXScenarioNumber(dbcmd, sqlQuery);
        player_foreign_scenario_arr = new player_scenario_class[MAX_PLAYER_SCENARIO_NUMBER][];
        npc_foreign_scenario_arr = new npc_scenario_class[MAX_NPC_SCENARIO_NUMBER];
        
        for(int a = 0; a < MAX_PLAYER_SCENARIO_NUMBER; a++){
            sqlQuery = "select foreign_scenario, next_npc_scenario_number from PlayerScenarioNumber where world_id = " + world_id + " and scene_id = " + scene_id + " and situation_id = " + situation_id + " and scenario_number = " + scenario_number_2;
            player_foreign_scenario_arr[a] = set_foreign_scenario(dbcmd, sqlQuery);
            scenario_number_2++;
        }
        sqlQuery = "select foreign_scenario, next_player_scenario_number from NPCScenarioNumber where world_id = " + world_id + " and scene_id = " + scene_id + " and situation_id = " + situation_id;
        dbcmd.CommandText = sqlQuery;
        reader = dbcmd.ExecuteReader();
        while(reader.Read()){
            string scenario = reader.GetString(0);
            int? next_number;
            if(reader.IsDBNull(1) == true){
                next_number = null;
            }
            else{
                next_number = reader.GetInt32(1);
            }
            npc_foreign_scenario_arr[npc_scenario_count] = new npc_scenario_class(scenario, next_number);
            npc_scenario_count++;
        }
        reader.Close();
        
        // NPC먼저인지 Player먼저인지 판단해야 되는 스크립트 개발해야됨
        Transform npcbuttonins = Instantiate(npcbuttonprefab);
        npcbuttonins.SetParent(GameObject.Find("ConversationPanel").transform);
        npcbuttonins.GetComponent<Button>().onClick.AddListener(PlayPlayerScenario);
        npcbuttonins.name = "npcbutton";
        npcbuttonins.GetComponent<RectTransform>().anchorMax = new Vector2(0f,1f);
        npcbuttonins.GetComponent<RectTransform>().anchorMin = new Vector2(0f,1f);
        npcbuttonins.GetComponent<RectTransform>().anchoredPosition = new Vector3(50, -50, 0);
        string first_scenario = npc_foreign_scenario_arr[(int) scenario_number-1].get_foreign_scenario();
        GameObject.Find("UIText").GetComponent<Text>().text = first_scenario;
    }

    public void PlayPlayerScenario()
    {
        scenario_number = npc_foreign_scenario_arr[(int) scenario_number-1].get_next_player_scenario_number();
        if(scenario_number == null){
            Debug.Log("finish");
            return;
        }
        GameObject[] playerbuttonarr = GameObject.FindGameObjectsWithTag("npcbutton");
        for(int a = 0; a < playerbuttonarr.Length; a++){
            Destroy(playerbuttonarr[a]);
        } // npcbutton 삭제
        GameObject.Find("UIText").GetComponent<Text>().text = ""; // text 초기화
        for(int a = 0; a < player_foreign_scenario_arr[(int) scenario_number-1].Length; a++) // player시나리오 개수만큼 반복
        {
            string scenario = player_foreign_scenario_arr[(int) scenario_number-1][a].get_foreign_scenario(); // scenario받아오기
            GameObject.Find("UIText").GetComponent<Text>().text +=  (a + 1) + " " + scenario + "\n"; // 1 scenario 식으로 변경
            Transform playerbuttonins = Instantiate(playerbuttonprefab); // player button 인스턴스화
            playerbuttonins.tag = "playerbutton";
            playerbuttonins.SetParent(GameObject.Find("ConversationPanel").transform);
            playerbuttonins.GetComponent<Button>().onClick.AddListener(PlayNPCScenario);
            playerbuttonins.name= "playerbutton"+a;
            playerbuttonins.GetComponent<RectTransform>().anchorMax = new Vector2(1f,1f);
            playerbuttonins.GetComponent<RectTransform>().anchorMin = new Vector2(1f,1f);
            playerbuttonins.GetComponent<RectTransform>().anchoredPosition = new Vector3(-50*(a+1), -50, 0);
        }
    }

    public void PlayNPCScenario()
    {
        string current_button_name = EventSystem.current.currentSelectedGameObject.name;
        current_button_number = GameObject.FindGameObjectsWithTag("playerbutton").Length-1 - Int32.Parse(current_button_name.Substring(current_button_name.Length - 1));
        scenario_number = player_foreign_scenario_arr[(int) scenario_number-1][current_button_number].get_next_npc_scenario_number();
        Debug.Log(scenario_number);
        if(scenario_number == null){
            Debug.Log("finish");
            return;
        }
        GameObject[] playerbuttonarr = GameObject.FindGameObjectsWithTag("playerbutton");
        for(int a = 0; a < playerbuttonarr.Length; a++){
            Destroy(playerbuttonarr[a]);
        }
        string scenario = npc_foreign_scenario_arr[(int) scenario_number-1].get_foreign_scenario();
        GameObject.Find("UIText").GetComponent<Text>().text = scenario;
        Transform npcbuttonins = Instantiate(npcbuttonprefab);
        npcbuttonins.tag = "npcbutton";
        npcbuttonins.SetParent(GameObject.Find("ConversationPanel").transform);
        npcbuttonins.GetComponent<Button>().onClick.AddListener(PlayPlayerScenario);
        npcbuttonins.name = "npcbutton";
        npcbuttonins.GetComponent<RectTransform>().anchorMax = new Vector2(0f,1f);
        npcbuttonins.GetComponent<RectTransform>().anchorMin = new Vector2(0f,1f);
        npcbuttonins.GetComponent<RectTransform>().anchoredPosition = new Vector3(50, -50, 0);
    }

    public int CountMAXScenarioNumber(IDbCommand dbcmd, string sqlQuery){
        int count = 0;
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while(reader.Read()){
            count++;
        }
        reader.Close();
        return count;
    }
    

    public player_scenario_class[] set_foreign_scenario(IDbCommand dbcmd, string sqlQuery){
        int count = 0;
        dbcmd.CommandText = "select count(*) from PlayerScenarioNumber where world_id = " + world_id + " and scene_id = " + scene_id + " and situation_id = " + situation_id + " and scenario_number = " + scenario_number_2;
        IDataReader reader = dbcmd.ExecuteReader();
        reader.Read();
        MAX_SUB_PLAYER_SCENARIO_NUMBER = reader.GetInt32(0);
        reader.Close();
        Debug.Log("max - " + MAX_SUB_PLAYER_SCENARIO_NUMBER);
        dbcmd.CommandText = sqlQuery;
        reader = dbcmd.ExecuteReader();
        player_scenario_class[] sub_arr = new player_scenario_class[MAX_SUB_PLAYER_SCENARIO_NUMBER];
        while(reader.Read()){
            int? next_number;
            string scenario = reader.GetString(0);
            if(reader.IsDBNull(1) == true){
                next_number = null;
            }
            else{
                next_number = reader.GetInt32(1);
            }
            sub_arr[count] = new player_scenario_class(scenario, next_number);
            count++;
        }
        reader.Close();
        return sub_arr;
    }

    public void Set_Scene_Id(int scene_id){
        this.scene_id = scene_id;
    }

    public void Set_Situation_Id(int situation_id){
        this.situation_id = situation_id;
    }

    public void Set_World_Id(int world_id){
        this.world_id = world_id;
    }
    
}
