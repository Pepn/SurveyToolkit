using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoForm : FormObject
{
    [HideInInspector] public InfoFormData Data;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI info;

    // Start is called before the first frame update
    void Start()
    {
        title.text = Data.title;
        info.text = Data.info;
    }

    public void SetData(InfoFormData ifd) => Data = ifd;

}
