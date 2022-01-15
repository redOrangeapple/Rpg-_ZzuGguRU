using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public int WalkCount;
   protected int currentWalkCount;

    public LayerMask layerMask;
    public float speed;
    protected Vector3 vector;
    public BoxCollider2D boxCollider2D;
     public Animator animator;
}