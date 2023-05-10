using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handicap : MonoBehaviour
{
    public static Handicap Current;

    [System.Serializable]
    public struct HandicapMessage
    {
        public KeyCode key;
        public string msg;
    };

    #region handicaps
    [SerializeField]
    List<KeyCode> startAsLocked = new List<KeyCode>();
    [SerializeField]
    List<HandicapMessage> unlockMsg = new List<HandicapMessage>();
    [SerializeField]
    List<HandicapMessage> lockMsg = new List<HandicapMessage>();

    HashSet<KeyCode> currentlyLocked = new HashSet<KeyCode>();
    #endregion

    private void Awake()
    {
        if (Current)
        {
            Destroy(this);
            return;
        }

        Current = this;
        foreach (KeyCode key in startAsLocked)
        {
            currentlyLocked.Add(key);
        }
    }

    public static bool HasHandicapStatic(KeyCode key)
    {
        return Current ? Current.HasHandicap(key) : false;
    }

    public static bool ReportMessageStatic(string msg)
    {
        return Current ? Current.ReportMessage(msg) : false;
    }

    public bool HasHandicap(KeyCode key)
    {
        return currentlyLocked.Contains(key);
    }

    public bool ReportMessage(string msg)
    {
        return ParseMessage(msg);
    }

    private bool ParseMessage(string msg)
    {
        bool result = false;
        foreach (HandicapMessage item in unlockMsg)
        {
            if (item.msg.Equals(msg))
            {
                currentlyLocked.Remove(item.key);
                result = true;
            }
        }
        foreach (HandicapMessage item in lockMsg)
        {
            if (item.msg.Equals(msg))
            {
                currentlyLocked.Add(item.key);
                result = true;
            }
        }
        return result;
    }
}
