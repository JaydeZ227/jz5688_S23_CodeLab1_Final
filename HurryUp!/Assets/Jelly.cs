using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Jelly : MonoBehaviour
{
    public float Intensity = 1f;
    public float Mass = 1f;
    public float Stiffness = 1f;
    public float Damping = 0.75f;
    public int Subdivisions = 1;

    private Mesh OriginalMesh, MeshClone;
    private MeshRenderer Renderer;
    private JellyVertex[] Vertices;

    void Start()
    {
        OriginalMesh = GetComponent<MeshFilter>().sharedMesh;
        MeshClone = Instantiate(OriginalMesh);
        GetComponent<MeshFilter>().sharedMesh = MeshClone;
        Renderer = GetComponent<MeshRenderer>();
        Vertices = new JellyVertex[MeshClone.vertices.Length];
        for (int i = 0; i < MeshClone.vertices.Length; i++)
        {
            Vertices[i] = new JellyVertex(i, transform.TransformPoint(MeshClone.vertices[i]));
        }
    }

    void FixedUpdate()
    {
        for (int i = 0; i < Vertices.Length; i++)
        {
            Vertices[i].UpdateVelocity(Time.deltaTime, Mass);
            Vertices[i].Settle(Time.deltaTime, Stiffness, Damping);
        }

        for (int i = 0; i < Vertices.Length; i++)
        {
            Vector3[] neighbours = Vertices[i].GetNeighbours(Vertices);
            Vector3 delta = Vector3.zero;
            for (int j = 0; j < neighbours.Length; j++)
            {
                delta += Vertices[i].GetPosition() - neighbours[j];
            }

            Vector3 force = delta * (Stiffness / neighbours.Length);
            Vertices[i].AddForce(force * Intensity);
        }

        for (int i = 0; i < Vertices.Length; i++)
        {
            Vertices[i].UpdatePosition(Time.deltaTime);
        }

        MeshClone.vertices = Vertices.Select(v => transform.InverseTransformPoint(v.GetPosition())).ToArray();
        MeshClone.RecalculateNormals();
    }
}
public class JellyVertex
{
    private int index;
    private Vector3 position;
    private Vector3 velocity;
    private Vector3 force;

    public JellyVertex(int _index, Vector3 _pos)
    {
        index = _index;
        position = _pos;
        velocity = Vector3.zero;
        force = Vector3.zero;
    }

    public void AddForce(Vector3 _force)
    {
        force += _force;
    }

    public void UpdateVelocity(float _deltaTime, float _mass)
    {
        velocity += (force / _mass) * _deltaTime;
        force = Vector3.zero;
    }

    public void Settle(float _deltaTime, float _stiffness, float _damping)
    {
        velocity *= 1f - _damping * _deltaTime;
        position += velocity * _deltaTime;
        velocity *= 1f / (1f + (_stiffness * _deltaTime));
    }

    public void UpdatePosition(float _deltaTime)
    {
        position += velocity * _deltaTime;
    }

    public int GetIndex()
    {
        return index;
    }

    public Vector3 GetPosition()
    {
        return position;
    }

    public Vector3[] GetNeighbours(JellyVertex[] _vertices)
    {
        List<Vector3> neighbours = new List<Vector3>();
        for (int i = 0; i < _vertices.Length; i++)
        {
            if (i != index)
            {
                neighbours.Add(_vertices[i].GetPosition());
            }
        }
        return neighbours.ToArray();
    }
}
