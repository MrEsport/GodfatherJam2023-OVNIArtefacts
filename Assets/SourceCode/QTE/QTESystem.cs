using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class QTESystem : MonoBehaviour
{
    [SerializeField] QTEInfos QTEObj;

    [SerializeField] QTE.QTEType debugType;
    [Button]
    void debugCreateQTE()
    {
        QTEObj.CreateQTE(debugType);
    }

}
