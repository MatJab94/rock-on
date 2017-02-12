using System.Collections.Generic;
using UnityEngine;
using System;

public class AStar_PathRequestManager : MonoBehaviour
{
    // requests for finding the paths
    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;

    // instance of this script
    static AStar_PathRequestManager instance;

    // pathfinding script
    AStar_Pathfinding pathfinding;

    bool isProcessingPath;
    
    private void Awake()
    {
        instance = this;
        pathfinding = GetComponent<AStar_Pathfinding>();
    }

    // request to find a path for an object
    public static void requestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        instance.pathRequestQueue.Enqueue(newRequest);
        instance.tryProcessNext();
    }

    private void tryProcessNext()
    {
        if(!isProcessingPath && pathRequestQueue.Count > 0)
        {
            isProcessingPath = true;
            currentPathRequest = pathRequestQueue.Dequeue();
            pathfinding.startFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }

    public void finishedProcessingPath(Vector3[] path, bool success)
    {
        currentPathRequest.callback(path, success);
        isProcessingPath = false;
        tryProcessNext();
    }

    // struct for path requests
    struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;

        public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback)
        {
            pathStart = _start;
            pathEnd = _end;
            callback = _callback;
        }
    }
}
