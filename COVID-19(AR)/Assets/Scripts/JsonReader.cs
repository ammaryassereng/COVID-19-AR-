using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class JsonReader : MonoBehaviour
{

    public Text txt;
    public string Country_name;
    string url;
    string JSONData;

    void Start()
    {

        DateTime date = DateTime.Today.AddDays(-1);
        string D = date.ToString("yyyy-MM-dd");

        DateTime date2 = DateTime.Today.AddDays(-3);
        string D2 = date2.ToString("yyyy-MM-dd");

        url = "https://api.covid19api.com/total/country/" + Country_name + "/status/confirmed?from=" + D2 + "T00:00:00Z&to=" + D + "T00:00:00Z";

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            txt.text = "Error. Check internet connection!";
        }
        else
        {

            JSONData = Get(url);
            Debug.Log(JSONData);

            JSONData = "{ \"countries\":" + JSONData + "}";


            //Debug.Log(JSONData);

            Nations nations = JsonUtility.FromJson<Nations>(JSONData);
            if (nations.countries.Count >= 2)
            {
                txt.text = "New cases: " + (nations.Countries[nations.Countries.Count-1].Cases - nations.Countries[nations.Countries.Count - 2].Cases).ToString();
            }
            else
            {
                txt.text = "No data updated today yet";
            }

        }
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


// genrating the necessary class for json string

[System.Serializable]
public class Nation
{

    public string Country;
    public string CountryCode;
    public string Province;
    public string City;
    public string CityCode;
    public string Lat;
    public string Lon;
    public int Cases;
    public string Status;
    public string Date;
}

[Serializable]
public class Nations
{
    public List<Nation> countries;

    public List<Nation> Countries { get => countries; set => countries = value; }
}


