using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTrail : MonoBehaviour
{
    PlayerController playerControll;

    public float activeTime;

    [Header("Mesh Related")]
    public float meshRefreshRate;
    public float meshDestroyDelay;
    public Transform positionToSpawn;

    [Header("Shader Related")]
    public Material mat;
    public string shaderVarRef;
    public float shaderVarRate;
    public float shaderVarRefreshRate;

    private bool isTrailActive;
    private SkinnedMeshRenderer[] skinnedMeshRenderers; 

    void Start()
    {
        playerControll = GetComponentInParent<PlayerController>();
        
    }

    void Update()
    {
        if(playerControll.isRolling && !isTrailActive)   
        {
            isTrailActive = true;
            StartCoroutine(ActiveTrail(activeTime));
        }
        else
        {

        }   
    }

    IEnumerator ActiveTrail (float timeActive)
    {
        while (timeActive > 0)
        {
            timeActive -= meshRefreshRate;

            if(skinnedMeshRenderers == null) skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

            for(int i = 0; i < skinnedMeshRenderers.Length; i++)
            {
                GameObject gObj = new GameObject();
                gObj.transform.SetPositionAndRotation(positionToSpawn.position, positionToSpawn.rotation);

                MeshRenderer mr = gObj.AddComponent<MeshRenderer>();
                MeshFilter mf = gObj.AddComponent<MeshFilter>();

                Mesh mesh = new Mesh();
                skinnedMeshRenderers[i].BakeMesh(mesh);

                mf.mesh = mesh;
                mr.material = mat;

                StartCoroutine(AnimationMaterialFloat(mr.material, 0, shaderVarRate, shaderVarRefreshRate));

                if(!playerControll.isRolling)
                {
                    Destroy(gObj, meshDestroyDelay);
                }
                else
                {
                    Destroy(gObj);
                }
                
            }

            yield return new WaitForSeconds(meshRefreshRate);
        }

        isTrailActive = false;
    }

    IEnumerator AnimationMaterialFloat(Material mat, float goal, float rate, float refreshRate)
    {
        float valueToAnimate = mat.GetFloat(shaderVarRef);

        while( valueToAnimate > goal)
        {
            valueToAnimate -= rate;
            mat.SetFloat(shaderVarRef, valueToAnimate);
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
