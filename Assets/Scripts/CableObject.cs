using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CableType
{
    Default
}

public class CableObject : MonoBehaviour
{
        public Transform StartPoint;
    public Transform EndPoint;

    private LineRenderer lineRenderer;
    private List<CableSegment> cableSegments = new List<CableSegment>();
    private float cableSegLen = -0.1f;
    private int segmentLength = 200;
    private float lineWidth = 0.2f;

    //Sling shot 
    private bool moveToMouse = false;
    private Vector3 mousePositionWorld;
    private int indexMousePos;

    // Use this for initialization
    void Start()
    {
        this.lineRenderer = this.GetComponent<LineRenderer>();
        Vector3 cableStartPoint = StartPoint.position;

        for (int i = 0; i < segmentLength; i++)
        {
            this.cableSegments.Add(new CableSegment(cableStartPoint));
            cableStartPoint.y -= cableSegLen;
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.DrawCable();
        if (Input.GetMouseButtonDown(0)) {
            this.moveToMouse = true;
        } else if (Input.GetMouseButtonUp(0)) {
            this.moveToMouse = false;
        }

        Vector3 screenMousePos = Input.mousePosition;
        float xStart = StartPoint.position.x;
        float xEnd = EndPoint.position.x;
        this.mousePositionWorld = Camera.main.ScreenToWorldPoint(new Vector3(screenMousePos.x, screenMousePos.y, 10));
        float currX = this.mousePositionWorld.x;

        float ratio = (currX - xStart) / (xEnd - xStart);
        if (ratio > 0) {
            this.indexMousePos = (int)(this.segmentLength * ratio);
        }
    }

    private void FixedUpdate()
    {
        this.Simulate();
    }

    private void Simulate()
    {
        // SIMULATION
        Vector2 forceGravity = new Vector2(0f, -1f);

        for (int i = 1; i < this.segmentLength; i++)
        {
            CableSegment firstSegment = this.cableSegments[i];
            Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            firstSegment.posNow += velocity;
            firstSegment.posNow += forceGravity * Time.fixedDeltaTime;
            this.cableSegments[i] = firstSegment;
        }

        //CONSTRAINTS
        for (int i = 0; i < 50; i++)
        {
            this.ApplyConstraint();
        }
    }

    private void ApplyConstraint()
    {
        //Constrant to First Point 
        CableSegment firstSegment = this.cableSegments[0];
        firstSegment.posNow = this.StartPoint.position;
        this.cableSegments[0] = firstSegment;


        //Constrant to Second Point 
        CableSegment endSegment = this.cableSegments[this.cableSegments.Count - 1];
        endSegment.posNow = this.EndPoint.position;//Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.cableSegments[this.cableSegments.Count - 1] = endSegment;

        for (int i = 0; i < this.segmentLength - 1; i++)
        {
            CableSegment firstSeg = this.cableSegments[i];
            CableSegment secondSeg = this.cableSegments[i + 1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = Mathf.Abs(dist - this.cableSegLen);
            Vector2 changeDir = Vector2.zero;

            if (dist > cableSegLen)
            {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            }
            else if (dist < cableSegLen)
            {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            Vector2 changeAmount = changeDir * error;
            if (i != 0)
            {
                firstSeg.posNow -= changeAmount * 0.5f;
                this.cableSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                this.cableSegments[i + 1] = secondSeg;
            }
            else
            {
                secondSeg.posNow += changeAmount;
                this.cableSegments[i + 1] = secondSeg;
            }

            if (this.moveToMouse && indexMousePos > 0 && indexMousePos < this.segmentLength - 1 && i == indexMousePos) {
                CableSegment segment = this.cableSegments[i];
                CableSegment segment2 = this.cableSegments[i + 1];
                segment.posNow = new Vector2(this.mousePositionWorld.x, this.mousePositionWorld.y);
                segment2.posNow = new Vector2(this.mousePositionWorld.x, this.mousePositionWorld.y);
                this.cableSegments[i] = segment;
                this.cableSegments[i + 1] = segment2;
            }
        }
    }

    private void DrawCable()
    {
        float lineWidth = this.lineWidth;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        Vector3[] cablePositions = new Vector3[this.segmentLength];
        for (int i = 0; i < this.segmentLength; i++)
        {
            cablePositions[i] = this.cableSegments[i].posNow;
        }

        lineRenderer.positionCount = cablePositions.Length;
        lineRenderer.SetPositions(cablePositions);
    }

    public struct CableSegment
    {
        public Vector2 posNow;
        public Vector2 posOld;

        public CableSegment(Vector2 pos)
        {
            this.posNow = pos;
            this.posOld = pos;
        }
    }
}
