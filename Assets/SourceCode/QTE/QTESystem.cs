using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Unity.VisualScripting;
using System;

public class QTESystem : MonoBehaviour
{
    [SerializeField] QTEInfos QTEObj;

    [SerializeField] QTE.QTEType debugType;
    [Button]
    void DebugCreateQTE()
    {
        QTEObj.CreateQTE(debugType);
    }

    void CreateNewQTE(QTE.QTEType type, InputButton.BPosition pos, InputButton.BColor col, InputButton.BLabel lab)
    {
        throw new NotImplementedException();   
    }


}
