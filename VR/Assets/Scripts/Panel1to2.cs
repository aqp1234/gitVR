using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Panel1to2 : MonoBehaviour
{
    public void clickbutton(){
        GameObject.Find("Canvas").transform.Find(this.name.Substring(0,this.name.Length - 1)+"2").gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
