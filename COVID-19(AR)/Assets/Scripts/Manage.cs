using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Manage : MonoBehaviour
{
    public Camera ARCamera;
    public GameObject Earth;
    public GameObject Prefab;
    public GameObject Compared_Earth;
    

    //public GameObject Compare_btn;
    public Button Compare_Button;
    public Button Close_Compare;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Set the Earth to the summoned Earth

    public void SetEarth(GameObject generated)
    {
        Earth = generated;
        Earth.transform.position = new Vector3(Earth.transform.position.x, Earth.transform.position.y + 0.7f, Earth.transform.position.z);
        Earth.GetComponent<EarthRotator>().SetCamera(ARCamera);
        Earth.GetComponent<EarthRotator>().SetManeger(gameObject);
    }

    //Handle the reset rotation action on the Earth, Sends the command to reset the rotation to the EarthRotator script

    public void ResetEarthRotation()
    {
        if(Earth != null)
        {
            //Earth.transform.eulerAngles = new Vector3(
            //    Mathf.LerpAngle(transform.eulerAngles.x, 0f, Time.deltaTime * 4f),
            //    Mathf.LerpAngle(transform.eulerAngles.y, 0f, Time.deltaTime * 4f),
            //    Mathf.LerpAngle(transform.eulerAngles.z, 0f, Time.deltaTime * 4f));
            Earth.gameObject.GetComponent<EarthRotator>().Reset();
        }
        else
        {
            return;
        }
    }

    // destroys the Earth, called on pressing the reset scan button

    public void DeleteEarth()
    {
        if(Earth != null)
        {
            Destroy(Earth);
        }
        Earth = null;
    }

    // Changes the type of Info that appear on the Planet Earth, sends the string of the type of the information to the EarthRotator script

    public void ChooseTypeOfInformation(string Type)
    {
        if(Earth != null)
        {
            Earth.GetComponent<EarthRotator>().SelectTypeOfInfo(Type);
        }
    }

    // enable and disable the compare button

    public void IsAble(bool par)
    {
        if (par == true)
        {
            if (Close_Compare.gameObject.activeInHierarchy == false)
            {
                Compare_Button.gameObject.SetActive(true);
            }
        }
        else
        {
            Compare_Button.gameObject.SetActive(false);
        }
    }

    // Generate the Compared Earth to the right of the original earth, infront of the camera

    public void Generate_Comp()
    {
        GameObject Camera = ARCamera.gameObject;

        float Xm = Camera.transform.position.x;
        float Zm = Camera.transform.position.z;

        float Xt = Earth.transform.position.x;
        float Zt = Earth.transform.position.z;

        float Xdiff = Xt - Xm;
        float Zdiff = Zt - Zm;

        float S = (float)System.Math.Sqrt((double)(Zdiff * Zdiff) + (double)(Xdiff * Xdiff));

        float slop = (float)System.Math.Atan(Zdiff / Xdiff);

        float slopangel = slop * 180 / (float)System.Math.PI;

        if (Xdiff < 0 && Zdiff >= 0)
        {
            slopangel = slopangel + 180;
        }
        else if (Zdiff < 0 && Xdiff >= 0)
        {
            slopangel = slopangel + 360;
        }
        else if (Zdiff < 0 && Xdiff < 0)
        {
            slopangel = slopangel + 180;
        }

        slop = slopangel * (float)System.Math.PI / 180;

        float angel = (float)System.Math.Atan(0.5 / (double)S);

        angel = angel * 180 / (float)System.Math.PI;

        float maindist = S * 1 / ((float)System.Math.Cos(angel * System.Math.PI / 180));

        float Xfinal = maindist * (float)System.Math.Cos(slop - angel * System.Math.PI / 180) + Xm;
        float Zfinal = maindist * (float)System.Math.Sin(slop - angel * System.Math.PI / 180) + Zm;

        Compared_Earth = Instantiate(Prefab, new Vector3(Xfinal, Earth.transform.position.y, Zfinal), Quaternion.identity);
        Compared_Earth.GetComponent<EarthRotator>().SetCamera(ARCamera);
    }

    //handle the action of pressing the cancel compare button.

    public void Disable_Compare_Button()
    {
        if (Compared_Earth != null)
        {
            Destroy(Compared_Earth);
            Compared_Earth = null;
        }

        if(Earth.GetComponent<EarthRotator>().Get_CountrySelected())
        {
            Compare_Button.gameObject.SetActive(true);
        }
        else
        {
            Compare_Button.gameObject.SetActive(false);
        }
    }

    //Changes the type of Info that appear on the Compared Earth, sends the string of the type of the information to the EarthRotator script

    public void ChooseTypeOfInformation_Compared(string Type)
    {
        if (Compared_Earth != null)
        {
            Compared_Earth.GetComponent<EarthRotator>().SelectTypeOfInfo(Type);
        }
    }
}
