using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnClickExitButton(){
        Transform[] allChildTransform = GameObject.Find("Canvas").GetComponentsInChildren<Transform>();
        foreach(Transform child in allChildTransform){
            if(child.name.Equals("LeftUIPanel") || child.name.Equals("Canvas")) continue;
            child.gameObject.SetActive(false);
        }
        for(int a = 0; a < GameObject.Find("LeftUIPanel").transform.childCount; a++){
            GameObject.Find("LeftUIPanel").transform.GetChild(a).gameObject.SetActive(true);
        }
    }
}
