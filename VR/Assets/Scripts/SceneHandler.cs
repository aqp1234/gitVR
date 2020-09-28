using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;
using System;

public class SceneHandler : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;

    void Awake()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        IPointerClickHandler clickHandler = e.target.GetComponent<Collider>().gameObject.GetComponent<IPointerClickHandler>();
        
        if (e.target.transform.GetComponent<Button>() != null){
            if (e.target.tag.Equals("playerbutton"))
            {
                int current_number = Int32.Parse(e.target.name.Substring(e.target.name.Length - 1));
                GameObject.Find("Director").GetComponent<testdbscript>().PlayNPCScenario(current_number);
                Debug.Log(e.target.name.Substring(e.target.name.Length - 1));
            }
            else
            {
                PointerEventData e_Data = new PointerEventData(EventSystem.current);
                clickHandler.OnPointerClick(e_Data);
            }
            
        }
        
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        if (e.target.name == "Cube")
        {
            Debug.Log("Cube was entered");
        }
        else if (e.target.name == "Button")
        {
            Debug.Log("Button was entered");
        }
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        if (e.target.name == "Cube")
        {
            Debug.Log("Cube was exited");
        }
        else if (e.target.name == "Button")
        {
            Debug.Log("Button was exited");
        }
    }
}