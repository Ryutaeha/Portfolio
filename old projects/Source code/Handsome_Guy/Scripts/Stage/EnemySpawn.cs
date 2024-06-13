using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawn : MonoBehaviour
{
    private Tilemap tilemap;

    private float blueSpawnInterval = 5f;
    private float blackSpawnInterval = 7f;
    private float bossSpawnInterval = 60f;

    private int blueInitialSpawnCount = 1 * PhotonNetwork.CurrentRoom.Players.Count;
    private int blackInitialSpawnCount = 1 * PhotonNetwork.CurrentRoom.Players.Count;
    private int bossInitialSpawnCount = 1 * PhotonNetwork.CurrentRoom.Players.Count;

    private int blueSpawnCount = 1;
    private int blackSpawnCount = 1;
    private int bossSpawnCount = 1;

    public void StartSpawn()
    {
        GameObject gridObject = GameObject.Find("Grid");

        if (gridObject != null)
        {
            Transform grundTransform = gridObject.transform.Find("Grund");

            if (grundTransform != null)
            {
                tilemap = grundTransform.GetComponent<Tilemap>();

                if (tilemap != null)
                {
                    StartCoroutine(SpawnBlueCoroutine());
                    StartCoroutine(SpawnBlackCoroutine());
                    StartCoroutine(SpawnBossCoroutine());
                    StartCoroutine(IncreaseSpawnCountCoroutine());
                }
            }
        }
    }

    // 리팩토링이 필요해 보임

    private IEnumerator SpawnBlueCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < blueSpawnCount; i++)
            {
                Vector3Int randomTilePosition = GetRandomTilePosition();
                Main.ObjectManager.Spawn<Enemy>("Blue", tilemap.GetCellCenterWorld(randomTilePosition));
            }

            yield return new WaitForSeconds(blueSpawnInterval);
        }
    }

    private IEnumerator SpawnBlackCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < blackSpawnCount; i++)
            {
                Vector3Int randomTilePosition = GetRandomTilePosition();
                Main.ObjectManager.Spawn<Enemy>("Black", tilemap.GetCellCenterWorld(randomTilePosition));
            }

            yield return new WaitForSeconds(blackSpawnInterval);
        }
    }

    private IEnumerator SpawnBossCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < bossSpawnCount; i++)
            {
                Vector3Int randomTilePosition = GetRandomTilePosition();
                Main.ObjectManager.Spawn<Boss>("Boss", tilemap.GetCellCenterWorld(randomTilePosition));
            }

            yield return new WaitForSeconds(bossSpawnInterval);
        }
    }

    private IEnumerator IncreaseSpawnCountCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f); // 매 분마다

            // 매 분마다 몬스터 스폰 수를 증가
            blueSpawnCount += blueInitialSpawnCount;
            blackSpawnCount += blackInitialSpawnCount;
            bossSpawnCount += bossInitialSpawnCount;

            // TODO 플레이어 수만큼 증가 추가 해야됨
        }
    }

    private Vector3Int GetRandomTilePosition()
    {
        BoundsInt bounds = tilemap.cellBounds;

        while (true)
        {
            Vector3Int randomPosition = new Vector3Int(
                Random.Range(bounds.x, bounds.x + bounds.size.x),
                Random.Range(bounds.y, bounds.y + bounds.size.y),
                0
            );

            // 해당 위치의 타일을 가져옵니다.
            TileBase tile = tilemap.GetTile(randomPosition);

            // 빈 공간이 아니면 해당 위치를 반환합니다.
            if (tile != null)
            {
                return randomPosition;
            }
        }
    }
}
