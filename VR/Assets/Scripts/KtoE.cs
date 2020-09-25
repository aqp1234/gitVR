using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class KtoE: MonoBehaviour
{
    // Start is called before the first frame update
    public void OnClickKtoEButton(){
        GameObject.Find("Canvas").transform.Find("ExitButton").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("KtoEInputField").gameObject.SetActive(true);
        //GameObject.Find("Director").GetComponent<LeftUIOpenCloseScript>().PanelOpenorClose();
    }

    public void Option1_ExecProcess()
    {
        System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();


        psi.FileName = @"C:\Users\KMS\AppData\Local\Programs\Python\Python38-32\python.exe";

        var script = Application.dataPath + "/pythoncode/papago.py";
        var text = GameObject.Find("KtoEText").GetComponent<Text>().text;

        psi.Arguments = $"\"{script}\" {text}";

        psi.UseShellExecute = false;
        psi.CreateNoWindow = true;
        psi.RedirectStandardOutput = true;
        psi.RedirectStandardError = true;

        var errors = "";
        var results = "";

        using (var process = System.Diagnostics.Process.Start(psi))
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
