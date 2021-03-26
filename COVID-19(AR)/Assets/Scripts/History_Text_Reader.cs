using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class History_Text_Reader : MonoBehaviour
{
    public TextAsset ExternalFile;
    public Text txt;



    //Start is called before the first frame update
    void Start()
    {
        string[] texts = ExternalFile.text.Split("\n"[0]);
        foreach(string line in texts)
        {
            txt.text = txt.text + line + "\n";
        }
    }

    // Update is called once per frame
    //void Update()
    //{

    //}






}
