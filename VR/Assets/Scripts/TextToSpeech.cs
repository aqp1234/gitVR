using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextToSpeech : MonoBehaviour
{
    // Start is called before the first frame update
    public static void OnClickTextToSpeechButton(){
        GameObject.Find("Canvas").transform.Find("ExitButton").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("TextToSpeechInputField").gameObject.SetActive(true);
        GameObject.Find("Director").GetComponent<LeftUIOpenCloseScript>().PanelOpenorClose();
    }

    public void Option1_ExecProcess()
    {
        System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();

        string text = GameObject.Find("TextToSpeechText").GetComponent<Text>().text;

        psi.FileName = @"C:\Users\KMS\AppData\Local\Programs\Python\Python38-32\python.exe";

        var script = Application.dataPath + "/pythoncode/ttsTest.py";
        //var text = "gtts test success";

        psi.Arguments = $"\"{script}\" \"{text}\"";

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

    }
}
