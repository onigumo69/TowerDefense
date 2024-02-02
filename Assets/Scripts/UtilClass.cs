using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilClass
{
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        return mouseWorldPosition;
    }

}
