using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadKindergardens : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextAsset data = Resources.Load<TextAsset>("POIS_Kindergarten");
        string[] kindergardens = data.text.Split(new char[] { '\n' });
        Debug.Log(kindergardens.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
