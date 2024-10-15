using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SideCamera : MonoBehaviour
{
    public TMP_Text exteralJSText;
    public void MoveCamera(int index)
    {
        exteralJSText.gameObject.SetActive(true);
        exteralJSText.text = index.ToString();
    }
}
