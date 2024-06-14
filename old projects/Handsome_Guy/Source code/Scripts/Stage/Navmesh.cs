using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

public class Navmesh : MonoBehaviourPunCallbacks
{
    NavMeshPlus.Components.NavMeshSurface surfaces;

    private void Awake()
    {
        surfaces = GetComponent<NavMeshPlus.Components.NavMeshSurface>();
    }

    private void OnEnable()
    {
        BuildNavMeshLocally();
    }

    public override void OnJoinedRoom()
    {
        // 방에 참가하면 로컬 플레이어의 경우 빌드를 시작합니다.
        if (photonView.IsMine)
        {
            BuildNavMeshLocally();
        }
    }

    private void BuildNavMeshLocally()
    {
        if (surfaces != null)
        {
            surfaces.BuildNavMesh();
        }
        else
        {
            Debug.LogError("NavMeshSurface component not found!");
        }
    }
}
