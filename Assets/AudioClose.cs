using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class AudioClose : MonoBehaviour
{
    public Transform player;
    public float threshold;

    public Transform[] nextPositions;

    private int index = 0;

    private Transform startTrans;

    void Start()
    {
        startTrans = this.transform;
    }

    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) < threshold)
        {
            transform.position = nextPositions[index++].position;
            index = index % nextPositions.Length;
        }
    }

    public void ResetAudio()
    {
        transform.position = new Vector3(0, 0, 15);
        index = 0;
    }
}
