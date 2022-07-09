using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCount : MonoBehaviour
{
    TextMesh m_text;

    [SerializeField]
    string m_label;

    int m_count = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_text = GetComponent<TextMesh>();
        m_text.text = m_label + m_count.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCount()
    {
        ++m_count;
        m_text.text = m_label + m_count.ToString();
    }
}
