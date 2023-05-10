using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class MathManager : MonoBehaviour
{
    public delegate void MathDelegate();
    public MathDelegate d_OnCompleteEquation;

    public static MathManager instance;
    public int resultToTutorial;

    private int multiplier = 1;
    private Queue<int> pendingMultipliers = new Queue<int>();
    public List<int> availableNumbers;

    private TMPro.TextMeshProUGUI multiplierText;
    private TMPro.TextMeshProUGUI pendingText;
    public int Multiplier 
    { 
        get { return multiplier; } 
        set 
        { 
            if (multiplier == 1)
            {
                multiplier = value;
                if (multiplierText)
                    multiplierText.text = $"[x{multiplier}]";
            }
            else
            {
                pendingMultipliers.Enqueue(value);
                if (pendingText && pendingMultipliers.Count < 2)
                    pendingText.text = $"next: x{value}";
            }
        } 
    }

    public enum operation
    {
        none,
        add,
        subtract,
        divide,
        multiply
    }

    public operation currOperation;
    
    public int randomMin = -5;
    public int randomMax = 10;
    public int result = 0;

    // randomly generated number
    [HideInInspector] public int randNumber = Int32.MinValue;
    //public int gainedNumber = 0;
    public int currentSolution = -1;
    [HideInInspector] public int currentKnownNumber = 0;
    [HideInInspector] public int currentResult = 0;
    [HideInInspector] public int oldResult;

    [HideInInspector]
    // result of operation
    public int currNumber = Int32.MinValue;

    public string lhsString;
    public string rhsString;
    public string operatorString;

    public AI_Boss boss;
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("MathManager instance already exists!");
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        int rand = UnityEngine.Random.Range(0, 10);
        if (rand % 2 == 0)
        {
            currOperation = operation.add;
            operatorString = "+";
        }
        else
        {
            currOperation = operation.multiply;
            operatorString = "*";
        }
        UIManager.instance.randNumText.text = $"__ {operatorString} __";
        multiplierText = UIManager.instance.multiplierText;
        pendingText = UIManager.instance.pendingText;
        //UIManager.instance.currentResultText.text = $"Result: ";

        if (!boss)
            boss = GameObject.Find("Level").transform.Find("BossParent")?
                .Find("TempBoss")?.GetComponent<AI_Boss>();

        GenerateQuestion();
    }

    public void doMath(int rhs)
    {
        bool newNum = false;
        result = Int32.MinValue;
        
        switch (currOperation)
        {
            case operation.add:
                result = currNumber + rhs;
                operatorString = "+";
                break;
            case operation.subtract:
                result = currNumber - rhs;
                operatorString = "-";
                break;
            case operation.multiply:
                result = currNumber * rhs;
                operatorString = "*";
                break;
            case operation.divide:
                result = currNumber / rhs;
                operatorString = "/";
                break;
            default:
                //Debug.LogError("invalid math operation!");
                currNumber = rhs;
                lhsString = rhs.ToString();
                newNum = true;
                break;
        }

        if (result != Int32.MinValue)
        {
            // apply multiplier
            result = ApplyMultiplier(result);

            resultToTutorial = result;
            //currNumber = result;
            Debug.Log($"Math operation: {currNumber} & {rhs}");
            Debug.Log("got number: " + result);
            UIManager.instance.DisplayResult(result);
            UIManager.instance.currentResultText.text = $"Ammo: {Manager.Instance.ammoCount}";
            GenerateRandomOperator();
            //boss?.UpdateHP(-result);
        }

        if (!newNum)
        {
            currNumber = Int32.MinValue;
        }
    }

    void GenerateRandomOperator()
    {
        int rand = UnityEngine.Random.Range(0, 100);
        int index = /*rand % 4*/ rand % 2;
        switch (index)
        {
            case 0:
                currOperation = operation.add;
                operatorString = "+";
                break;
            case 1:
                currOperation = operation.multiply;
                operatorString = "*";
                break;
            /*case 2:
                currOperation = operation.subtract;
                break;
            case 3:
                currOperation = operation.divide;
                break;*/
        }
    }

    public void GenerateQuestion()
    {
        if (availableNumbers.Count < 1)
            return;
        GenerateRandomOperator();
        currentKnownNumber = UnityEngine.Random.Range(1, 10);
        int solutionIdx = UnityEngine.Random.Range(0, availableNumbers.Count);
        currentSolution = availableNumbers[solutionIdx];
        //availableNumbers.RemoveAt(solutionIdx);
        currentResult = 0;

        switch (currOperation)
        {
            case operation.add:
                currentResult = currentKnownNumber + currentSolution;
                break;
            case operation.multiply:
                currentResult = currentKnownNumber * currentSolution;
                break;
            default:
                break;
        }
        
        UIManager.instance.randNumText.text = $"{currentKnownNumber} {operatorString} ? = {currentResult}";
    }

    public void GenerateQuestion(List<int> range)
    {
        List<int> defaultList = availableNumbers;
        availableNumbers = range;
        GenerateQuestion();
        availableNumbers = defaultList;
    }

    private int ApplyMultiplier(int result)
    {
        result *= multiplier;

        if (pendingMultipliers.Count > 0)
            multiplier = pendingMultipliers.Dequeue();
        else
            multiplier = 1;

        if (multiplierText)
            multiplierText.text = multiplier == 1 ? "" : $"[x{multiplier}]";
        if (pendingText)
            pendingText.text = pendingMultipliers.Count < 1 ? "" : $"next: x{pendingMultipliers.Peek()}";

        return result;
    }

    public void OnCompleteQuestion()
    {
        d_OnCompleteEquation?.Invoke();
    }
}
