using UnityEngine;
using System.Collections;

public class Platform  {

    public int startIndex;
    public int length;
    public int heigth;
    public bool isDanger;
    public GameObject addOn;

    public Platform ( int myStart, int myLength, int myHeight, bool myIsDanger)
    {
        startIndex = myStart;
        length = myLength;
        heigth = myHeight;
        isDanger = myIsDanger;

    }
}
