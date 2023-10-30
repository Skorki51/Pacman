using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private NodeController targetNode = null;
    private NodeController earlierNode = null;
    private NodeController[] allNodes = null;
    private void Awake()
    {
        if(targetNode == null)
        {
            RandomStart();
        }
        transform.position = targetNode.transform.position;
    }

    void FixedUpdate()
    {
        if(Vector2.Distance(transform.position, targetNode.transform.position) < 0.001f)
        {
            ChooseNextNode();
        }
       transform.position = Vector2.MoveTowards(transform.position, targetNode.transform.position, 0.1f);
    }

    void ChooseNextNode()
    {
        NodeController valueNodeController = targetNode;
        targetNode = targetNode.ReturnNeighborRandom(earlierNode);
        earlierNode = valueNodeController;
    }

    void RandomStart()
    {
        allNodes = FindObjectsOfType<NodeController>();
        int randomNumber = Random.Range(0, allNodes.Length);
        targetNode = allNodes[randomNumber];
    }
}
