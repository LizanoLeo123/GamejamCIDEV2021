using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CableObject), typeof(PolygonCollider2D))]
public class CableCollision : MonoBehaviour
{

    public CableObject cable;
    public PolygonCollider2D collider;
    public List<Vector2> colliderPoints = new List<Vector2>();
    // Start is called before the first frame update
    private void Awake()
    {
        cable = GetComponent<CableObject>();
        collider = GetComponent<PolygonCollider2D>();
        
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3[] positions = cable.GetPositions();
        int numberofLines = positions.Length - 1;
        collider.pathCount = numberofLines;
        for(int i  = 0; i < numberofLines; i++){
            List<Vector2> currentPositions = new List<Vector2>
            {
                positions[i],
                positions[i+1]
            };

            List<Vector2> currentColliderPoints = CallculateColliderPoints(currentPositions);
            collider.SetPath(i, currentColliderPoints.ConvertAll(p => (Vector2)transform.InverseTransformPoint(p)));
        }
    }

    private List<Vector2> CallculateColliderPoints(List<Vector2> positions)
    {
        float width = cable.getLineWidth();

        float m = (positions[1].y - positions[0].y) / (positions[1].x - positions[0].x);
        float deltaX = (width / 2f) * (m / Mathf.Pow(m * m + 1, 0.5f));
        float deltaY = (width / 2f) * (1 / Mathf.Pow(1 + m * 1, 0.5f));

        Vector2[] offsets = new Vector2[2];
        offsets[0] = new Vector2(-deltaX, deltaY);
        offsets[1] = new Vector2(deltaX, -deltaY);

        List<Vector2> colliderPositions = new List<Vector2>{
            positions[0] + offsets[0],
            positions[1] + offsets[0],
            positions[1] + offsets[1],
            positions[0] + offsets[1]
        };

        return colliderPositions;
        
    }
}
