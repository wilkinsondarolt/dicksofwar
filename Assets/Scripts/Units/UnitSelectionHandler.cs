using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitSelectionHandler : NetworkBehaviour
{
  [SerializeField] private LayerMask layerMask = new LayerMask();
  private Camera mainCamera;
  public List<Unit> selectedUnits { get; } = new List<Unit>();

  private void Start()
  {
    mainCamera = Camera.main;
  }

  private void Update()
  {
    Mouse mouse = Mouse.current;

    if (mouse.leftButton.wasPressedThisFrame)
    {
        foreach(Unit selectedUnit in selectedUnits)
        {
            selectedUnit.Deselect();
        }
        selectedUnits.Clear();

    }
    else if (mouse.leftButton.wasReleasedThisFrame)
    {
      ClearSelectionArea();
    }
  }

  private void ClearSelectionArea()
  {
    Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

    if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
    {
      return;
    }

    if (!hit.collider.TryGetComponent<Unit>(out Unit unit)) {
        return;
    }

    if (!unit.isOwned) {
        return;
    }

    selectedUnits.Add(unit);

    foreach(Unit selectedUnit in selectedUnits)
    {
        selectedUnit.Select();
    }
  }
}
