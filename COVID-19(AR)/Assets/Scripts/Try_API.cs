using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Try_API : MonoBehaviour
{
    string url;
    string JSONData;

    void Start()
    {
        url = "https://api.covid19api.com/country/egypt/status/confirmed?from=2020-08-27T00:00:00Z&to=2020-08-28T00:00:00Z";
        JSONData = Get(url);
        Debug.Log(JSONData);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string Get(string url)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.AutomaticDecompression = DecompressionMethods.GZip;

        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        using (Stream stream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(stream))
        {
            return reader.ReadToEnd();
        }
    }
}
