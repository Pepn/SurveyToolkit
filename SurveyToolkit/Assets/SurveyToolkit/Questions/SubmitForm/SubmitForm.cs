using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SubmitForm : FormObject
{
    [HideInInspector] public SubmitFormData Data;
    [SerializeField] public TextMeshProUGUI title;
    [SerializeField] public TextMeshProUGUI buttonText;

    public void SetData(SubmitFormData ifd) => Data = ifd;
}
