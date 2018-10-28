using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using System;
using System.Security.Cryptography;
using System.Text;

public class Step
{

    private int random = 0;
    private float offset = 0.5f;
    private bool gotOpening = false;

    private bool[] values = new bool[8];
    public bool[] Values
    {
        get { return values; }
    }


    public Step(int level, int step, int maxSteps, float stepDifficulty)
    {
        MD5 md5Hash = MD5.Create();
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(level+""));
        int clamped = Mathf.Clamp(step , 0, maxSteps);
        int value = clamped / data.Length;
        Random randomSequence = new Random(data[value] + step);

        random = randomSequence.Next();

        offset = 0.1f + stepDifficulty;

        MakeValues();

    }

    private void MakeValues()
    {
        Random rand = new Random(random);
        for (int i = 0; i < values.Length; i++)
        {
            float val = (float)rand.NextDouble();
            values[i] = true;
            if (val > offset) {
                values[i] = false;
                gotOpening = true;
            }  
        }
        if (!gotOpening)
        {
            int value = (int)((float) rand.NextDouble() * values.Length);
            values[value] = false;
        }
    }

    public void ApplyStepController(StepController con)
    {


    }

}
