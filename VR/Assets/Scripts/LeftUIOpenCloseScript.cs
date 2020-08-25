using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftUIOpenCloseScript : MonoBehaviour
{
    public void PanelOpenorClose(){
        GameObject LeftUI = GameObject.Find("LeftUIPanel");
        GameObject LeftUIButton = GameObject.Find("LeftUIOpenButton");
        Animator anim = LeftUI.GetComponent<Animator>();
        anim.SetBool("OpenClick", false);
        anim.SetBool("CloseClick", false);
        if(LeftUI.GetComponent<RectTransform>().position.x < 0){
            anim.SetBool("OpenClick", true);
            LeftUIButton.transform.rotation = Quaternion.Euler(new Vector3(0,0,180));
        }
        else if(LeftUI.GetComponent<RectTransform>().position.x > 0){
            anim.SetBool("CloseClick", true);
            LeftUIButton.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        }
    }
}
