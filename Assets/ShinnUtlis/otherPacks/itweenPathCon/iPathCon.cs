using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

namespace Shinn
{
    [RequireComponent(typeof(iTweenPath))]
    public class iPathCon : MonoBehaviour
    {

        public iTweenPath ipath;

        [Space]
        public List<Transform> node = new List<Transform>();
        Vector3[] pos;


        [Button]
        public void RemoveAllNode()
        {
            ipath.nodeCount = 0;
        }

        [Button]
        public void RefleshItweenPath()
        {
            ipath.nodeCount = 0;
            StartCoroutine(CallItweenPath());
        }


        IEnumerator CallItweenPath()
        {
            ipath.nodeCount = node.Count;
            pos = new Vector3[node.Count];

            for (int i = 0; i < node.Count; i++)
                pos[i] = node[i].transform.position;

            ipath.nodes.AddRange(pos);
            yield return null;
        }
    }

}
