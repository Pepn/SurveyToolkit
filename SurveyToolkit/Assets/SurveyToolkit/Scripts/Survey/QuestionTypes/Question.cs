using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Question : FormObject
{
    [HideInInspector] private QuestionData Data;
    [SerializeField] protected GameObject questionObject;
    [SerializeField] protected GameObject answerObject;

    public abstract string GetAnswer();
    public abstract string GetQuestion();
    public virtual QuestionData GetData() => Data;
    public virtual void SetData(QuestionData qd) => Data = qd;
    public abstract List<GameObject> GetInCompletedForms();
}
