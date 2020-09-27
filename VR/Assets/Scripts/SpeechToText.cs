using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class SpeechToText : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnClickSpeechToTextButton(){
        GameObject.Find("Canvas").transform.Find("ExitButton").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("SpeechToTextInputField").gameObject.SetActive(true);
        //GameObject.Find("Director").GetComponent<LeftUIOpenCloseScript>().PanelOpenorClose();
    }

    public void Option1_ExecProcess()
    {
        System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();

        psi.FileName = @"C:\Users\KMS\AppData\Local\Programs\Python\Python38-32\python.exe";         //python.exe 경로

        var script = Application.dataPath + "/pythoncode/audio.py";        //파이썬 실행파일 경로


        psi.Arguments = $"\"{script}\"";            //파일경로와 인자 psi객체에 전달

        psi.UseShellExecute = false;
        psi.CreateNoWindow = true;
        psi.RedirectStandardOutput = true;
        psi.RedirectStandardError = true;

        var errors = "";
        var results = "";

        using (var process = System.Diagnostics.Process.Start(psi))         //파이썬 파일 실행하고 에러와 결과 읽어옴
        {
            errors = process.StandardError.ReadToEnd();
            results = process.StandardOutput.ReadToEnd();
        }

        Debug.Log("ERRORS:");
        Debug.Log(errors);
        Debug.Log("Results:");
        Debug.Log(results);
    }
}
