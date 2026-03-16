using UnityEngine;

public class WorldMapManager : MonoBehaviour
{
    public LevelNode[] levelNodes;

    private void Start()
    {
        RefreshAllNodes();
    }

    public void RefreshAllNodes()
    {
        foreach (LevelNode node in levelNodes)
        {
            if (node != null)
            {
                node.RefreshVisual();
            }
        }
    }
}