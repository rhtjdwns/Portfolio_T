/* Git Blame Auto Generated */

/* @rhtjdwns  - 2024-06-02 06:42:57 */ using System.Collections;
/* @rhtjdwns  - 2024-06-02 06:42:57 */ using System.Collections.Generic;
/* @rhtjdwns  - 2024-06-02 06:42:57 */ using MyBox;
/* @rhtjdwns  - 2024-06-02 06:42:57 */ using UnityEngine;
/* @rhtjdwns  - 2024-06-02 06:42:57 */ using UnityEngine.AI;
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */ namespace FullMoon.NavMesh
/* @rhtjdwns  - 2024-06-02 06:42:57 */ {
/* @rhtjdwns  - 2024-06-02 06:42:57 */     public class NavMeshTag : MonoBehaviour
/* @rhtjdwns  - 2024-06-02 06:42:57 */     {
/* @rhtjdwns  - 2024-06-02 06:42:57 */         [SerializeField, OverrideLabel("Type"), DefinedValues("Both", "Player", "Enemy")]
/* @rhtjdwns  - 2024-06-02 06:42:57 */         private string type = "Both";
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */         public static List<MeshFilter> playerMeshes = new List<MeshFilter>();
/* @rhtjdwns  - 2024-06-02 06:42:57 */         public static List<MeshFilter> enemyMeshes = new List<MeshFilter>();
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */         void OnEnable()
/* @rhtjdwns  - 2024-06-02 06:42:57 */         {
/* @rhtjdwns  - 2024-06-02 06:42:57 */             var m = GetComponent<MeshFilter>();
/* @rhtjdwns  - 2024-06-02 06:42:57 */             if (m != null)
/* @rhtjdwns  - 2024-06-02 06:42:57 */             {
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 if (type is "")
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 {
/* @rhtjdwns  - 2024-06-02 06:42:57 */                     type = "Both";
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 }
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 if (type is "Both")
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 {
/* @rhtjdwns  - 2024-06-02 06:42:57 */                     playerMeshes.Add(m);
/* @rhtjdwns  - 2024-06-02 06:42:57 */                     enemyMeshes.Add(m);
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 }
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 else if (type is "Player")
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 {
/* @rhtjdwns  - 2024-06-02 06:42:57 */                     playerMeshes.Add(m);
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 }
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 else if (type is "Enemy")
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 {
/* @rhtjdwns  - 2024-06-02 06:42:57 */                     enemyMeshes.Add(m);
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 }
/* @rhtjdwns  - 2024-06-02 06:42:57 */             }
/* @rhtjdwns  - 2024-06-02 06:42:57 */         }
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */         void OnDisable()
/* @rhtjdwns  - 2024-06-02 06:42:57 */         {
/* @rhtjdwns  - 2024-06-02 06:42:57 */             var m = GetComponent<MeshFilter>();
/* @rhtjdwns  - 2024-06-02 06:42:57 */             if (m != null)
/* @rhtjdwns  - 2024-06-02 06:42:57 */             {
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 if (type is "Both")
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 {
/* @rhtjdwns  - 2024-06-02 06:42:57 */                     playerMeshes.Remove(m);
/* @rhtjdwns  - 2024-06-02 06:42:57 */                     enemyMeshes.Remove(m);
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 }
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 else if (type is "Player")
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 {
/* @rhtjdwns  - 2024-06-02 06:42:57 */                     playerMeshes.Remove(m);
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 }
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 else if (type is "Enemy")
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 {
/* @rhtjdwns  - 2024-06-02 06:42:57 */                     enemyMeshes.Remove(m);
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 }
/* @rhtjdwns  - 2024-06-02 06:42:57 */             }
/* @rhtjdwns  - 2024-06-02 06:42:57 */         }
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */         public static void PlayerCollect(ref List<NavMeshBuildSource> sources)
/* @rhtjdwns  - 2024-06-02 06:42:57 */         {
/* @rhtjdwns  - 2024-06-02 06:42:57 */             sources.Clear();
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */             for (var i = 0; i < playerMeshes.Count; ++i)
/* @rhtjdwns  - 2024-06-02 06:42:57 */             {
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 var mf = playerMeshes[i];
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 if (mf == null) continue;
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 var m = mf.sharedMesh;
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 if (m == null) continue;
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 var s = new NavMeshBuildSource();
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 s.shape = NavMeshBuildSourceShape.Mesh;
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 s.sourceObject = m;
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 s.transform = mf.transform.localToWorldMatrix;
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 s.area = 0;
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 sources.Add(s);
/* @rhtjdwns  - 2024-06-02 06:42:57 */             }
/* @rhtjdwns  - 2024-06-02 06:42:57 */         }
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */         public static void EnemyCollect(ref List<NavMeshBuildSource> sources)
/* @rhtjdwns  - 2024-06-02 06:42:57 */         {
/* @rhtjdwns  - 2024-06-02 06:42:57 */             sources.Clear();
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */             for (var i = 0; i < enemyMeshes.Count; ++i)
/* @rhtjdwns  - 2024-06-02 06:42:57 */             {
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 var mf = enemyMeshes[i];
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 if (mf == null) continue;
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 var m = mf.sharedMesh;
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 if (m == null) continue;
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 var s = new NavMeshBuildSource();
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 s.shape = NavMeshBuildSourceShape.Mesh;
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 s.sourceObject = m;
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 s.transform = mf.transform.localToWorldMatrix;
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 s.area = 0;
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 sources.Add(s);
/* @rhtjdwns  - 2024-06-02 06:42:57 */             }
/* @rhtjdwns  - 2024-06-02 06:42:57 */         }
/* @rhtjdwns  - 2024-06-02 06:42:57 */     }
/* @rhtjdwns  - 2024-06-02 06:42:57 */ }
