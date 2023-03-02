using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : NetworkBehaviour {
    [SerializeField] private NavMeshAgent agent = null;

    #region Server
    [Command]
    public void CmdMove(Vector3 position)
    {
        bool validPosition = NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas);

        if(!validPosition){
            return;
        }

        agent.SetDestination(hit.position);
    }
    #endregion


}
