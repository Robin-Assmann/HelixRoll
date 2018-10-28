using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController controller;

    private int stepsPerLevel = 20;
    private int level = 1;
    private int progressThisLevel = 0;
    private Step[] steps;
    private GameObject[] stepObjects;
    private bool playing = false;

    public delegate void OnLevelFinishedDelegate();
    public static OnLevelFinishedDelegate OnLevelFinished;

    public delegate void OnLevelStartedDelegate();
    public static OnLevelStartedDelegate OnLevelStarted;

    [SerializeField] private Transform playerBall;
    [SerializeField] private PlayerController playerParent;
    [SerializeField] private AnimationCurve stepDifficulty;
    [SerializeField] private Transform stepPrefab;
    [SerializeField] private Transform stepParent;

    [Header("Prefabs")]
    [SerializeField] private Transform stepEight;
    [SerializeField] private Transform stepQuarter;
    [SerializeField] private Transform stepThreeEights;
    [SerializeField] private Transform stepHalf;

    private Vector3 ballStart;
    private Vector3 cameraStart;


    private void Start()
    {
        controller = this;
        DontDestroyOnLoad(gameObject);

        steps = new Step[stepsPerLevel];
        stepObjects = new GameObject[stepsPerLevel];
        
        InitLevel();
    }

    public void UpdateProgress()
    {
        progressThisLevel++;
    }

    public void StartLevel()
    {
        playing = true;
        OnLevelStarted();
    }

    public void FinishLevel()
    {

        OnLevelFinished();
        playing = false;

        foreach (GameObject g in stepObjects)
        {
            Destroy(g);
        }
        LevelUp();
    }


    private void LevelUp()
    {
        level++;
        progressThisLevel = 0;
        InitLevel();
    }

    private void InitLevel()
    {
        for (int i = 0; i < steps.Length; i++)
        {
            steps[i] = new Step(level, i, stepsPerLevel, stepDifficulty.Evaluate(i / stepsPerLevel));
        }
        InitialiseSteps();
    }

    private void InitialiseSteps()
    {
        for (int i = 0; i < steps.Length; i++)
        {
            bool[] values = steps[i].Values;

            Transform step = Instantiate(stepPrefab, stepParent);
            step.localScale = new Vector3(1, 1, 1);
            step.localPosition = new Vector3(0, -(3  * i), 0);
            ApplySteps(i, step, values);
            stepObjects[i] = step.gameObject;

        }
    }

    private void ApplySteps(int id, Transform step, bool[] values)
    {

        for (int i = 0; i < values.Length; i++)
        {
            if (values[i])
            {
                Transform s = Instantiate(stepEight, step);
                s.localRotation = Quaternion.Euler(new Vector3(0,i*45,0));
            }
        }
    }

    private void Update()
    {
        if (!playing && Input.GetMouseButtonDown(0))
        {
            StartLevel();
        }
    }




}
