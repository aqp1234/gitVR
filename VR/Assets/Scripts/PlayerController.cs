using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float turnSpeed;
    GameObject player;
    List<GameObject> FoundObjects;
    GameObject enemy;
    string TagName = "NPC";
    float shortDis;
    private float lookSensitivity = 2;
    private float cameraRotatoinLimit = 45;
    private float currentCameraRotationX = 0f;
    private float currentCameraRotationY = 0f;
    [SerializeField]private Camera playerCamera;
    Rigidbody myrigid;

    void moveandturn()
    {
        float _DirX = Input.GetAxis("Horizontal");
        float _DirY = Input.GetAxis("Vertical");
        Vector3 _MoveHorizontal = transform.right * _DirX;
        Vector3 _MoveVertical = transform.forward * _DirY;
        Vector3 _velocity = (_MoveHorizontal + _MoveVertical).normalized * moveSpeed;
        //myrigid.MovePosition(transform.position + _velocity * Time.deltaTime);
        myrigid.velocity = _velocity;
        //Move
        //Vector3 vec3 = GameObject.Find("Main Camera").transform.localRotation*Vector3.forward;
        //myrigid.velocity = myrigid.transform.position + _velocity * Time.deltaTime;
        //transform.Translate(0f, 0f, v * moveSpeed * Time.deltaTime);
        //Turn
        //transform.Rotate(0f, h * turnSpeed * Time.deltaTime, 0f);
        
    }

    void interactive()
    {
        if (Input.GetKeyDown("space"))
        {
            FoundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag(TagName));
            shortDis = Vector3.Distance(gameObject.transform.position, FoundObjects[0].transform.position); // 첫번째를 기준으로 잡아주기 

            enemy = FoundObjects[0]; // 첫번째를 먼저 
            foreach (GameObject found in FoundObjects)
            {
                float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

                if (Distance < shortDis) // 위에서 잡은 기준으로 거리 재기
                {
                    enemy = found;
                    shortDis = Distance;
                }
            }
            if (shortDis < 10)
            {
                GameObject[] icons = GameObject.FindGameObjectsWithTag("Icon");
                for (int a = 0; a < icons.Length; a++)
                {
                    if (Vector3.Distance(gameObject.transform.position, icons[a].transform.position) < 10)
                    {
                        //GameObject.Find("DDirector").GetComponent<testdbscript>().npcdb();
                        //icons[a].GetComponent<ChangeScene>().NextScene();
                        //GameObject.Find("sqlitetest").GetComponent<dbscript>().playscenario();
                    }
                }

            }
        }
    }

    private void CameraRotation(){
        //상하카메라 회전
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        //float _yRotation = Input.GetAxisRaw("Mouse X");
        float _cameraRotationX = _xRotation * lookSensitivity;
        //float _cameraRatationY = _yRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        //currentCameraRotationY += _cameraRatationY;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotatoinLimit, cameraRotatoinLimit);

        playerCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    private void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myrigid.MoveRotation(myrigid.rotation * Quaternion.Euler(_characterRotationY));
    }
    
    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("Player");
        myrigid = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveandturn();
        interactive();
        CameraRotation();
        CharacterRotation();
    }
}
