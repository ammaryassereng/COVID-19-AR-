using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class EarthRotator : MonoBehaviour
{
    bool rest;
    bool FreezeRotation;


    public List<Canvas> UIs;
    public List<Text> Txts;
    public List<Text> HistoryTxts;
    public List<Image> Graph_Images;
    public List<Text> Graph_txt;
    public List<Text> InfoTxt;

    public List<TextAsset> HistoryFiles;
    //public List<TextAsset> InfoFiles;

    public int SelectedIndex;
    public bool CountrySelected;

    public GameObject maneger;

    // Start is called before the first frame update
    void Start()
    {
        rest = false;
        CountrySelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (FreezeRotation == false)
        {
            if (rest == false)
            {
                gameObject.transform.Rotate(Vector3.up * (float)Time.deltaTime * 10f);
            }
            else
            {
                transform.eulerAngles = new Vector3(
                    Mathf.LerpAngle(transform.eulerAngles.x, 0f, Time.deltaTime * 10f),
                    Mathf.LerpAngle(transform.eulerAngles.y, 0f, Time.deltaTime * 10f),
                    Mathf.LerpAngle(transform.eulerAngles.z, 0f, Time.deltaTime * 10f));

                if (transform.eulerAngles.x <= 0.1f && transform.eulerAngles.y <= 0.1f && transform.eulerAngles.z <= 0.1f)
                {
                    rest = false;
                }
            }
        }
    }

    // changes the value of the reset

    public void Reset()
    {
        rest = true;
    }

    // make the buttons interact with the AR camera

    public void SetCamera(Camera ARcam)
    {
        for(int i = 0; i<UIs.Count;i++)
        {
            UIs[i].GetComponent<Canvas>().worldCamera = ARcam;
        }
    }

    // Choose a country

    public void selectCountry(int C)
    {
        SelectedIndex = C;
        //HistoryTxts[SelectedIndex].gameObject.SetActive(true);
        //HistoryTxts[SelectedIndex].text = HistoryFiles[SelectedIndex].text;
    }

    // makes sure that there is a specific country selected

    public void IsSelected(bool result)
    {
        CountrySelected = result;
        FreezeRotation = result;
        maneger.gameObject.GetComponent<Manage>().IsAble(result);
    }

    // choosing the type of info depending on the recived string

    public void SelectTypeOfInfo(string Info)
    {
        if(CountrySelected == true)
        {
            Txts[SelectedIndex].text = Info;
            if (Info == "History")
            {
                HistoryTxts[SelectedIndex].gameObject.SetActive(true);
                Graph_Images[SelectedIndex].gameObject.SetActive(false);
                Graph_txt[SelectedIndex].gameObject.SetActive(false);
                InfoTxt[SelectedIndex].gameObject.SetActive(false);

                HistoryTxts[SelectedIndex].text = HistoryFiles[SelectedIndex].text;

            }
            else
            {
                HistoryTxts[SelectedIndex].gameObject.SetActive(false);
                Graph_Images[SelectedIndex].gameObject.SetActive(true);
                Graph_txt[SelectedIndex].gameObject.SetActive(true);
                InfoTxt[SelectedIndex].gameObject.SetActive(true);

                //string[] txts = InfoFiles[SelectedIndex].text.Split("\n"[0]);
                //InfoTxt[SelectedIndex].text = "";

                //foreach (string Line in txts)
                //{
                //    InfoTxt[SelectedIndex].text = InfoTxt[SelectedIndex].text + "NO." + Line + "\n";
                //}

            }
        }
        else
        {
            return;
        }
    }

    // sets the manager

    public void SetManeger(GameObject Mng)
    {
        maneger = Mng;
    }

    // return if a country is selected or not

    public bool Get_CountrySelected()
    {
        return CountrySelected;
    }
}
