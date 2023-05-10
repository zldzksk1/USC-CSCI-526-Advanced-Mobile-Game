using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MathPickup : MonoBehaviour
{
    public int numToTutorial; //I just created to access number from Tutorial

    public int randomMin = 1;
    public int randomMax = 10;

    public static bool equationSolved = false;

    [SerializeField] private float respawnDelay = 30.0f;

    private GameObject textObj;
    private TextScript textScript;
    private Collider textCollider;
    private bool canPickup;

    private float rSpeed = 0.3f;

    private void Start()
    {
        Debug.Log("Starting mathpickup");
        textScript = gameObject.transform.GetComponentInChildren<TextScript>();
        textObj = textScript.gameObject;
        textCollider = GetComponent<Collider>();
        canPickup = true;

        if(textScript != null)
        {
            textScript.ChangeText(GenerateRandomValue().ToString());
            MathManager.instance.availableNumbers.Add(numToTutorial);
        }
        else
        {
            Debug.Log("TEXT SCRIPT IS NULL");
        }
        Debug.Log("ENding mathpickup");
    }

    private void Update()
    {
        // Rotate the object around its y-axis
        transform.Rotate(0, 1 * rSpeed, 0); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canPickup && other.transform.parent.CompareTag("Player"))
        {
            string myText = textScript.EnterTextHere;
            int num;
            Debug.Log("num");

            Debug.Log("text name is: " + name);

            if (int.TryParse(myText, out num))
            {
                /*
                if (MathManager.instance.currNumber == Int32.MinValue)
                {
                    MathManager.instance.currNumber = num;
                    MathManager.instance.lhsString = myText;
                    UIManager.instance.randNumText.text = $"{myText} {MathManager.instance.operatorString} __";
                }
                else
                {
                    MathManager.instance.rhsString = myText;
                    UIManager.instance.randNumText.text = $"{MathManager.instance.lhsString} {MathManager.instance.operatorString} {MathManager.instance.rhsString}";
                    MathManager.instance.doMath(num);
                    MathManager.instance.currNumber = Int32.MinValue;
                }
                */
                if (MathManager.instance.currentSolution == num)
                {
                    MathManager.instance.oldResult = MathManager.instance.currentResult;
                    if(UIManager.instance.currentResultText)
                    {
                        UIManager.instance.currentResultText.text = $"Ammo: {++Manager.Instance.ammoCount}";
                    }
                    UIManager.instance.randNumText.text =
                        $"{MathManager.instance.currentKnownNumber} {MathManager.instance.operatorString} {MathManager.instance.currentSolution}";
                    UIManager.instance.DisplayResult(MathManager.instance.currentResult);
                    //MathManager.instance.boss?.UpdateHP(-MathManager.instance.currentResult);
                    equationSolved = true;
                    MathManager.instance?.OnCompleteQuestion();
                    MathManager.instance?.GenerateQuestion();
                }
                else
                {
                    switch (myText)
                    {
                        case "+":
                            MathManager.instance.currOperation = MathManager.operation.add;
                            break;
                        case "-":
                            MathManager.instance.currOperation = MathManager.operation.subtract;
                            break;
                        case "*":
                            MathManager.instance.currOperation = MathManager.operation.multiply;
                            break;
                        case "/":
                            MathManager.instance.currOperation = MathManager.operation.divide;
                            break;
                        default:
                            Debug.Log("invalid operator!");
                            break;
                    }
                }
                
                StartCoroutine(RandomizeNumValue(respawnDelay));
            }
        }
    }

    public int GenerateRandomValue()
    {
        MathManager.instance.availableNumbers.Remove(numToTutorial);

        int value = UnityEngine.Random.Range(randomMin, randomMax);
        while(value == 0)
        {
            value = UnityEngine.Random.Range(randomMin, randomMax);
        }

        numToTutorial = value;
        MathManager.instance.availableNumbers.Add(value);
        return value;
    }

    private IEnumerator RandomizeNumValue(float delay)
    {
        int newNum = GenerateRandomValue();
        textScript.ShowText(false);
        textCollider.enabled = false;
        canPickup = false;
        textScript.ChangeText(newNum.ToString());
        Debug.Log("Picked up, waiting");
        yield return new WaitForSeconds(delay);
        Debug.Log("Waiting over");
        textScript.ShowText(true);
        // add new value
        MathManager.instance.availableNumbers.Add(numToTutorial);
        textCollider.enabled = true;
        canPickup = true;
    }
}
