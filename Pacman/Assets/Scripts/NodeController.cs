using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    [SerializeField] private List<NodeController> nodeNeighbors = null;
    [SerializeField] private bool enable = false;
    private NodeController nearestNeighbor = null;
    private float distance;
    private float nearestDistance = 1000;
     private NodeController ReturnNeighborNearest()
    {
        foreach (NodeController node in nodeNeighbors)
        {
            distance = Vector2.Distance(this.transform.position, node.transform.position);

            if(distance < nearestDistance)
            {
                nearestNeighbor = node;
                nearestDistance = distance;
            }
        }
        return nearestNeighbor;
    }

    public NodeController ReturnNeighborRandom(NodeController earlierNode)
    {
        if(nodeNeighbors.Count > 1)
        {
            if(earlierNode == null)
            {
                return nodeNeighbors[Random.Range(0, nodeNeighbors.Count)];
            }
            else
            {
                    List<NodeController> newList = RewriteList(nodeNeighbors);
                    newList.Remove(earlierNode);
                    return newList[Random.Range(0, newList.Count)];
            }
        }
        else { return nodeNeighbors[0]; }
    }
    private List<NodeController> RewriteList(List<NodeController> listSource)
    {
        List<NodeController> listOutput = new List<NodeController>();

        foreach(NodeController node in listSource)
        {
            listOutput.Add(node);
        }
        return listOutput;
    }

    private void OnMouseEnter()
    {
        if (enable)
        {
            foreach (NodeController node in nodeNeighbors)
            {
                node.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                ReturnNeighborNearest();
            }
        }
    }
    private void OnMouseExit()
    {
        if (enable)
        {
            foreach (NodeController node in nodeNeighbors)
            {
                node.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
}
