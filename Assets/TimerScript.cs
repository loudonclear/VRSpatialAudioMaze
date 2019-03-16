using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Valve.VR;

public class TimerScript : MonoBehaviour
{
    public Transform player;
    public AudioClose audioClose;

    public SteamVR_Action_Boolean grabPinch;
    public SteamVR_Action_Boolean grabGrip;
    public SteamVR_Input_Sources inputSource;

    private bool reset = false;
    private int pressCount = 0;
    private bool reset2 = false;
    private int pressCount2 = 0;

    public float timer = 0;
    private bool timerCount = true;

    private string audio = " using Audio";

    void Update()
    {
        if (timerCount)
        {
            timer += Time.deltaTime;
        }

        if (Vector3.Distance(player.position, transform.position) < 6 && timerCount)
        {
            timerCount = false;
            GetComponent<MeshRenderer>().enabled = false;

            System.IO.File.AppendAllText("vrspatialaudiotimings.txt", timer.ToString() + audio + "\n");
        }

        if (grabPinch != null)
        {
            if (grabPinch.GetStateDown(inputSource))
            {
                if (!reset)
                {
                    reset = true;
                }
            }
            if (grabPinch.GetStateUp(inputSource))
            {
                if (reset)
                {
                    reset = false;
                    pressCount++;
                    if (pressCount >= 5)
                    {
                        timerCount = true;
                        pressCount = 0;
                        player.position = new Vector3(0, -0.5f, 0);
                        timer = 0;
                        audioClose.ResetAudio();
                        GetComponent<MeshRenderer>().enabled = true;
                        audio = " using Audio";
                    }
                }
            }
        }

        if (grabGrip != null)
        {
            if (grabGrip.GetStateDown(inputSource))
            {
                if (!reset2)
                {
                    reset2 = true;
                }
            }
            if (grabGrip.GetStateUp(inputSource))
            {
                if (reset2)
                {
                    reset2 = false;
                    pressCount2++;
                    if (pressCount2 >= 5)
                    {
                        timerCount = true;
                        pressCount2 = 0;
                        player.position = new Vector3(0, -0.5f, 70);
                        timer = 0;
                        audioClose.ResetAudio();
                        GetComponent<MeshRenderer>().enabled = true;
                        audio = " not using Audio";
                    }
                }
            }
        }
    }
}
