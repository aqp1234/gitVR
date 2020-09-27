using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerMoveInput : MonoBehaviour
{
    private string[] interactiveButtons = {"EnglishMeaning", "KoreantoEnglish", "TTS", "Button4"};
    public float minmove = 0.2f;
    public Transform camTr;
    private float speed = 10.0f;
    private CharacterController controllertr;
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean TouchPad;
    public SteamVR_Action_Vector2 Joystick;
    public SteamVR_Action_Boolean Grip;
    public GameObject canvas;
    public bool iscanvasactive = false;

    void Start()
    {
        controllertr = GameObject.Find("Player").GetComponent<CharacterController>();
        controllertr.Move(camTr.TransformDirection(new Vector3(0.1f,0.0f,0.1f)));
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = new Vector3(getJoystick().x,0.0f,getJoystick().y);
        forward = camTr.TransformDirection(forward);
        forward.y = 0.0f;
        //Debug.Log("mag = "+forward.magnitude + "minmove = "+minmove);
        if(Mathf.Abs(forward.magnitude) < minmove)
        {
            //forward = new Vector3(0.0f,0.0f,0.0f);
            controllertr.Move(Vector3.zero * Time.deltaTime);
        }
        else{
            canvas.SetActive(false);
            Debug.Log("forward = "+forward + "" + handType);
            controllertr.Move(forward * speed * Time.deltaTime);
        }
        if(getGripGrab()){
            exitorgetui();
            canvas.SetActive(iscanvasactive);
        }
    }
    public bool getTouchPad(){
        return TouchPad.GetStateDown(handType);
    }
    public Vector2 getJoystick(){
        return Joystick.GetAxis(handType);
    }
    public bool getGripGrab(){
        return Grip.GetStateDown(handType);
    }
    public void exitorgetui(){
        iscanvasactive = !iscanvasactive;
        foreach(string buttonname in interactiveButtons){
            canvas.transform.Find(buttonname).gameObject.SetActive(iscanvasactive);
        }
        if(iscanvasactive){
            minmove = 1.0f;
        }
        else{
            minmove = 0.2f;
        }
    }
}
