using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

public class EnglishMean : MonoBehaviour
{
    //public Text meantext;
    // Start is called before the first frame update
    public void OnClickEnglishMeanButton(){
        GameObject.Find("Canvas").transform.Find("ExitButton").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("EnglishMeaningPanel1").gameObject.SetActive(true);
        //GameObject.Find("Director").GetComponent<LeftUIOpenCloseScript>().PanelOpenorClose();
    }

    public void Option1_ExecProcess()
    {
        string text = GameObject.Find("EnglishMeanText").GetComponent<Text>().text;
        System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();

        psi.FileName = @"C:\Users\KMS\AppData\Local\Programs\Python\Python38-32\python.exe";         //python.exe 경로

        var script = Application.dataPath + "/pythoncode/englishMean.py";        //파이썬 실행파일 경로
        //var text = "work";      //전달할 인자(영어 뜻이라서 영단어work)
        //Debug.Log(Application.dataPath);

        psi.Arguments = $"\"{script}\" \"{text}\"";            //파일경로와 인자 psi객체에 전달

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

            byte[] bytesForEncoding = System.Text.Encoding.UTF8.GetBytes(results);
            results = System.Text.Encoding.UTF8.GetString(bytesForEncoding);

        }

        Debug.Log("ERRORS:");
        Debug.Log(errors);
        Debug.Log("Results:");
        Debug.Log(results);
        //meantext.text = results;
    }
}
