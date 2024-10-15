using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ErrorPopupMessage : MonoBehaviour
{
    public Text Error_Text;
    public string Receive_Message;

    public void Cancel_Click()
    {
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    private void Awake()
    {
        Error_Text.text = Receive_Message;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
