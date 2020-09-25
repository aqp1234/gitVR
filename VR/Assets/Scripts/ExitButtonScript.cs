using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ExitButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canvas;
    
    public void OnClickExitButton(){
        try{
            GameObject[] playerbuttonarr = GameObject.FindGameObjectsWithTag("npcbutton");
            for(int a = 0; a < playerbuttonarr.Length; a++){
            Destroy(playerbuttonarr[a]);
            } // npcbutton 삭제
            GameObject.Find("ConversationText").GetComponent<Text>().text = ""; // text 초기화
            playerbuttonarr = GameObject.FindGameObjectsWithTag("playerbutton");
            for(int a = 0; a < playerbuttonarr.Length; a++){
            Destroy(playerbuttonarr[a]);
            }
        }
        catch(Exception e){
            Debug.Log(e);
        }
        /*Transform[] allChildTransform = canvas.GetComponentsInChildren<Transform>();
        foreach(Transform child in allChildTransform){
            Debug.Log(child.name);
            child.gameObject.SetActive(false);
        }*/
        /*foreach(Transform child in allChildTransform){
            if(child.name.Equals("LeftUIPanel") || child.name.Equals("Canvas")) continue;
            child.gameObject.SetActive(false);
        }
        for(int a = 0; a < GameObject.Find("LeftUIPanel").transform.childCount; a++){
            GameObject.Find("LeftUIPanel").transform.GetChild(a).gameObject.SetActive(true);
        }*/
        for(int a = 0; a < canvas.transform.childCount; a++){
            canvas.transform.GetChild(a).gameObject.SetActive(false);
        }
        for(int a = 5; a < 9; a++){
            canvas.transform.GetChild(a).gameObject.SetActive(true);
        }
        canvas.SetActive(false);
        GameObject.Find("Controller (left)").GetComponent<PlayerMoveInput>().minmove = 0.2f;
        GameObject.Find("Controller (right)").GetComponent<PlayerMoveInput>().minmove = 0.2f;
        GameObject.Find("Controller (left)").GetComponent<PlayerMoveInput>().iscanvasactive = false;
        GameObject.Find("Controller (right)").GetComponent<PlayerMoveInput>().iscanvasactive = false;
        
    }
}
