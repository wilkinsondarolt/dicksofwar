using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSNetworkManager : NetworkManager
{
  [SerializeField] private GameObject unitSpawnerPrefab = null;

  public override void OnServerAddPlayer(NetworkConnectionToClient conn)
  {
    base.OnServerAddPlayer(conn);

    GameObject playerSpawner = Instantiate(
      unitSpawnerPrefab,
      conn.identity.transform.position,
      conn.identity.transform.rotation
    );
    NetworkServer.Spawn(playerSpawner, conn);
  }
}
