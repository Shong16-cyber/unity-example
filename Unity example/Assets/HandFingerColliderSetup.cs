using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class HandFingerColliderSetup : MonoBehaviour
{
    public OVRSkeleton skeleton;

    void Start()
    {
        StartCoroutine(SetupColliderWhenReady());
    }

    IEnumerator SetupColliderWhenReady()
    {
        while (!skeleton.IsDataValid || !skeleton.IsDataHighConfidence)
            yield return null;

        foreach (var bone in skeleton.Bones)
        {
            if (bone.Id.ToString().ToLower().Contains("index3")) // 找食指末端
            {
                var tip = bone.Transform;
                if (!tip.GetComponent<Collider>())
                {
                    var sphere = tip.gameObject.AddComponent<SphereCollider>();
                    sphere.radius = 0.015f;

                    var rb = tip.gameObject.AddComponent<Rigidbody>();
                    rb.isKinematic = true;
                    rb.useGravity = false;

                    tip.tag = "Finger";

                    Debug.Log($"✅ {skeleton.GetSkeletonType()} Index finger ready! {tip.name}");
                }
            }
        }
    }
}
