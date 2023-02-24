using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class UnitMovement : NetworkBehaviour {
    [SerializeField] private NavMeshAgent agent = null;
    private Camera mainCamera;

    #region Server
    [Command]
    private void CmdMove(Vector3 position)
    {
        bool validPosition = NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas);

        if(!validPosition){
            return;
        }

        agent.SetDestination(hit.position);
    }
    #endregion

    #region Client
    public override void OnStartAuthority()
    {
        mainCamera = Camera.main;
    }

    [ClientCallback]
    private void Update() {
        if (!isOwned) {
            return;
        }

        Mouse mouse = Mouse.current;
        if(!mouse.rightButton.wasPressedThisFrame) {
            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(mouse.position.ReadValue());
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) {
            return;
        }

        CmdMove(hit.point);
    }

    #endregion
}
