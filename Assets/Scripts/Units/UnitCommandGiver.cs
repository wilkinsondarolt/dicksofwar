using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitCommandGiver : MonoBehaviour
{
  [SerializeField] private UnitSelectionHandler unitSelectionHandler = null;
  [SerializeField] private LayerMask layerMask = new LayerMask();
  private Camera mainCamera;

  private void Start()
  {
    mainCamera = Camera.main;
  }

  private void Update()
  {
    Mouse mouse = Mouse.current;

    if (!mouse.rightButton.wasPressedThisFrame) {
        return;
    }

    Ray ray = mainCamera.ScreenPointToRay(mouse.position.ReadValue());

    if(!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask)) {
        return;
    }

    TryMove(hit.point);
  }

  private void TryMove(Vector3 point)
  {
    foreach(Unit unit in unitSelectionHandler.selectedUnits)
    {
        unit.GetUnitMovement().CmdMove(point);
    }
  }
}
