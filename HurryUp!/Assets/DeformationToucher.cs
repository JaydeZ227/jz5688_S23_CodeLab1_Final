using UnityEngine;
public class DeformationToucher : MonoBehaviour
 {
    public MeshFilter targetMeshFilter;
    private Mesh targetMesh;

    public Camera mainCamera;

    private Vector3[] originalVertices, displacedVertices, vertexVelocities;

     private int verticesCount;

     public float force = 10;
     public float forceOffset = 0.1f;
    public float springForce = 20f;
    public float damping = 5f;

   void Start()
    {
         targetMesh = targetMeshFilter.mesh;
         verticesCount = targetMesh.vertices.Length;

         originalVertices = targetMesh.vertices;
         displacedVertices = targetMesh.vertices;
         vertexVelocities = new Vector3[verticesCount];
     }
    public float timeSpeed = 1;
    public void TriggerThis(Vector3 pos) 
    {
        Vector3 actingForcePoint = targetMeshFilter.transform.InverseTransformPoint(pos);//������ָ����ı�����������

        for (int i = 0; i < verticesCount; i++)
        {
            Vector3 pointToVertex = displacedVertices[i] - actingForcePoint;//��������ָ��ǰ����λ�õ�����

            float actingForce = force / (1f + pointToVertex.sqrMagnitude);//��������С
            vertexVelocities[i] += pointToVertex.normalized * actingForce * Time.deltaTime* timeSpeed;//�����ٶ�����
        }
    }

     void Update()
     {
        //if (Input.GetMouseButton(0))
        //{
        //    if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo))
        //    {
        //        TriggerThis(hitInfo.point + hitInfo.normal * forceOffset);
        //    }
        //}

        for (int i = 0; i < verticesCount; i++)
                 {
                     vertexVelocities[i] += (originalVertices[i] - displacedVertices[i]) * springForce * Time.deltaTime*timeSpeed;//����+���㵱ǰλ��ָ�򶥵��ʼλ�õ��ٶ�����==�ص���
                     vertexVelocities[i] *= 1f - damping * Time.deltaTime* timeSpeed;//��������
                     displacedVertices[i] += vertexVelocities[i] * Time.deltaTime*timeSpeed;//����������һ��λ��
                 }
    
         targetMesh.vertices = displacedVertices;
             targetMesh.RecalculateNormals();
         }
}