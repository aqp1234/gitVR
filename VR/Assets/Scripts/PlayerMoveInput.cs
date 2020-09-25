using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerMoveInput : MonoBehaviour
{
    public float minmove = 0.2f;
    public Transform camTr;
    private float speed = 10.0f;
    private CharacterController controllertr;
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean TouchPad;
    public SteamVR_Action_Vector2 Joystick;
    public SteamVR_Action_Boolean Grip;
    public SteamVR_Action_Boolean Left;
    public SteamVR_Action_Boolean Right;
    public GameObject canvas;
    public bool iscanvasactive = false;
    public GameObject player;

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
        if (getLeft()){
            player.transform.Rotate(new Vector3(0.0f, -30.0f, 0.0f));
        }
        if (getRight())
        {
            player.transform.Rotate(new Vector3(0.0f, +30.0f, 0.0f));
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
    public bool getLeft(){
        return Left.GetStateDown(handType);
    }
    public bool getRight(){
        return Right.GetStateDown(handType);
    }
    public void exitorgetui(){
        iscanvasactive = !iscanvasactive;
        for(int a = 5; a < 9; a++){
            canvas.transform.GetChild(a).gameObject.SetActive(iscanvasactive);
        }
        if(iscanvasactive){
            minmove = 1.0f;
        }
        else{
            minmove = 0.2f;
        }
        
    }
    /*public void clickUIButton()
    {
        iscanvasactive = !iscanvasactive;
        for (int a = 5; a < 9; a++)
        {
            canvas.transform.GetChild(a).gameObject.SetActive(iscanvasactive);
        }
        if (iscanvasactive)
        {
            minmove = 1.0f;
        }
        else
        {
            minmove = 0.2f;
        }
    }*/
}
