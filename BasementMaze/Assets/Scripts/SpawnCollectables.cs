using UnityEngine;

public class SpawnCollectables : MonoBehaviour {
    [SerializeField] private Vector3ListSO activeTileList;
    [SerializeField] private LocationRotationListSO wallList;
    [SerializeField] private GameObject objsToCollect;
    [SerializeField] private GameObject urinal;
    [SerializeField] private GameObject lightPrefab;
    [SerializeField] private float verticalOffset = 1f;

    public void SpawnObjects() {
        var tilesUnused = activeTileList;
        var count = 15;
        while (count > 0) {
            var totalTiles = tilesUnused.Vector3s.Count;
            var selectedTile = tilesUnused.Vector3s[Random.Range(0, totalTiles)];
            Instantiate(objsToCollect, new Vector3(selectedTile.x, selectedTile.y + verticalOffset, selectedTile.z), Quaternion.identity);
            tilesUnused.Vector3s.Remove(selectedTile);
            count--;
        }

        SpawnAmountOfObjectOnWalls(100, urinal);
        SpawnAmountOfObjectOnWalls(25, lightPrefab);
    }

    private Vector3 DetermineHorizontalOffset(Quaternion inRotation) {
        var horizontalOffset = Vector3.zero;
        if (inRotation == Quaternion.LookRotation(Vector3.left)) {
            horizontalOffset += new Vector3(-.4f, 0, 0);
        }
        if (inRotation == Quaternion.LookRotation(Vector3.right)) {
            horizontalOffset += new Vector3(.4f, 0, 0);
        }
        if (inRotation == Quaternion.LookRotation(Vector3.forward)) {
            horizontalOffset += new Vector3(0, 0, .4f);
        }
        if (inRotation == Quaternion.LookRotation(Vector3.back)) {
            horizontalOffset += new Vector3(0, 0, -.4f);
        }

        return horizontalOffset;
    }

    private void SpawnAmountOfObjectOnWalls(int amount, GameObject objectToSpawn) {
        var wallsUnused = wallList;
        while (amount > 0) {
            var totalWalls = wallsUnused.locationRotationTypes.Count;
            var selectedWall = wallsUnused.locationRotationTypes[Random.Range(0, totalWalls)];
            var verticalPositionOffset = new Vector3(0, -1.4f, 0f);
            var horizontalPositionOffset = DetermineHorizontalOffset(selectedWall.rotation);
            var positionOffset = verticalPositionOffset + horizontalPositionOffset;
            var rotationOffset = new Vector3(-90, 90, 0);
            var scaleOffset = new Vector3(0, 1, 0);
            var newObject = Instantiate(objectToSpawn, selectedWall.location + positionOffset, selectedWall.rotation);
            newObject.transform.Rotate(rotationOffset);
            newObject.transform.localScale += scaleOffset;
            wallsUnused.locationRotationTypes.Remove(selectedWall);
            amount--;
        }
    }
}
