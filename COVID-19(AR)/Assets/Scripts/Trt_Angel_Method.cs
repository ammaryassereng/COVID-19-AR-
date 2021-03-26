using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trt_Angel_Method : MonoBehaviour
{
    public GameObject Targeted;
    public GameObject Prefab;
    float angel;
    GameObject made;

    // Start is called before the first frame update
    void Start()
    {
        float Xm = gameObject.transform.position.x;
        float Zm = gameObject.transform.position.z;

        float Xt = Targeted.transform.position.x;
        float Zt = Targeted.transform.position.z;

        float Xdiff = Xt - Xm;
        float Zdiff = Zt - Zm;

        float S = (float)Math.Sqrt((double)(Zdiff * Zdiff) + (double)(Xdiff * Xdiff));

        float slop = (float)Math.Atan(Zdiff / Xdiff);

        float slopangel = slop * 180 / (float)Math.PI;

        angel = (float)Math.Atan(0.5 / (double)S);

        angel = angel * 180 / (float)Math.PI;

        float maindist = S * 1/((float)Math.Cos(angel * Math.PI/180));

        float Xfinal = maindist * (float)Math.Cos(slop - angel * Math.PI / 180) + Xm;
        float Zfinal = maindist * (float)Math.Sin(slop - angel * Math.PI / 180) + Zm;

        made = Instantiate(Prefab, new Vector3(Xfinal, Targeted.transform.position.y, Zfinal), Quaternion.identity);
    }

    //Update is called once per frame
    void Update()
    {
        float Xm = gameObject.transform.position.x;
        float Zm = gameObject.transform.position.z;

        float Xt = Targeted.transform.position.x;
        float Zt = Targeted.transform.position.z;

        float Xdiff = Xt - Xm;
        float Zdiff = Zt - Zm;

        float S = (float)Math.Sqrt((double)(Zdiff * Zdiff) + (double)(Xdiff * Xdiff));

        float slop = (float)Math.Atan(Zdiff / Xdiff);

        float slopangel = slop * 180 / (float)Math.PI;

        if(Xdiff <0 && Zdiff >=0)
        {
            slopangel = slopangel + 180;
        }
        else if(Zdiff <0 && Xdiff>=0)
        {
            slopangel = slopangel + 360;
        }
        else if(Zdiff < 0 && Xdiff < 0)
        {
            slopangel = slopangel + 180;
        }

        slop = slopangel * (float)Math.PI / 180;

        angel = (float)Math.Atan(0.5 / (double)S);

        angel = angel * 180 / (float)Math.PI;

        float maindist = S * 1 / ((float)Math.Cos(angel * Math.PI / 180));

        float Xfinal = maindist * (float)Math.Cos(slop - angel * Math.PI / 180) + Xm;
        float Zfinal = maindist * (float)Math.Sin(slop - angel * Math.PI / 180) + Zm;

        made.transform.position = new Vector3(Xfinal, Targeted.transform.position.y, Zfinal);
    }
}
