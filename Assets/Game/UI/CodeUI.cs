using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeUI : MonoBehaviour
{
    public string code;
    public Text codeText;

    // Start is called before the first frame update
    void Start()
    {
        code = "";
    }

    // Update is called once per frame
    void Update()
    {
        codeText.text = code;
    }

    public void AddToCode(string code)
    {
        this.code += code;
    }
    public void RemoveFromCode(string code)
    {
        this.code = code.Remove(code.Length-1);
    }

    public void RemoveAll()
    {
        this.code = "";
    }
}
