using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : NetworkBehaviour
{
    [SerializeField] private UnitMovement unitMovement = null;
    [SerializeField] private UnityEvent onSelected = null;
    [SerializeField] private UnityEvent onDeselected = null;

    public UnitMovement GetUnitMovement()
    {
        return unitMovement;
    }

    #region Client
    [Client]
    public void Select()
    {
        if (!isOwned) {
            return;
        }

        onSelected?.Invoke();
    }

    [Client]
    public void Deselect()
    {
        if (!isOwned) {
            return;
        }

        onDeselected?.Invoke();
    }
    #endregion
}
