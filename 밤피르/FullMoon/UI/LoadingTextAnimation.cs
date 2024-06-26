/* Git Blame Auto Generated */

/* @LiF  - 2024-06-03 14:20:46 */ using TMPro;
/* @LiF  - 2024-06-03 14:20:46 */ using UnityEngine;
/* @LiF  - 2024-06-03 14:20:46 */ 
/* @LiF  - 2024-06-03 14:20:46 */ namespace FullMoon.UI
/* @LiF  - 2024-06-03 14:20:46 */ {
/* @LiF  - 2024-06-03 14:20:46 */     public class LoadingTextAnimation : MonoBehaviour
/* @LiF  - 2024-06-03 14:20:46 */     {
/* @LiF  - 2024-06-03 14:20:46 */         [SerializeField] private TextMeshProUGUI loadingText;
/* @LiF  - 2024-06-03 14:20:46 */         [SerializeField] private float waveSpeed = 2f;
/* @LiF  - 2024-06-03 14:20:46 */         [SerializeField] private float waveHeight = 5f;
/* @LiF  - 2024-06-03 14:20:46 */         [SerializeField] private float waveDifference = 0.5f;
/* @LiF  - 2024-06-03 14:20:46 */ 
/* @LiF  - 2024-06-03 14:20:46 */         private void Update()
/* @LiF  - 2024-06-03 14:20:46 */         {
/* @LiF  - 2024-06-03 14:20:46 */             if (loadingText != null)
/* @LiF  - 2024-06-03 14:20:46 */             {
/* @LiF  - 2024-06-03 14:20:46 */                 loadingText.ForceMeshUpdate();
/* @LiF  - 2024-06-03 14:20:46 */                 TMP_TextInfo textInfo = loadingText.textInfo;
/* @LiF  - 2024-06-03 14:20:46 */ 
/* @LiF  - 2024-06-03 14:20:46 */                 AnimateWave(textInfo);
/* @LiF  - 2024-06-03 14:20:46 */                 UpdateGeometry(textInfo);
/* @LiF  - 2024-06-03 14:20:46 */             }
/* @LiF  - 2024-06-03 14:20:46 */         }
/* @LiF  - 2024-06-03 14:20:46 */ 
/* @LiF  - 2024-06-03 14:20:46 */         private void UpdateGeometry(TMP_TextInfo textInfo)
/* @LiF  - 2024-06-03 14:20:46 */         {
/* @LiF  - 2024-06-03 14:20:46 */             for (int i = 0; i < textInfo.meshInfo.Length; i++)
/* @LiF  - 2024-06-03 14:20:46 */             {
/* @LiF  - 2024-06-03 14:20:46 */                 TMP_MeshInfo meshInfo = textInfo.meshInfo[i];
/* @LiF  - 2024-06-03 14:20:46 */                 meshInfo.mesh.vertices = meshInfo.vertices;
/* @LiF  - 2024-06-03 14:20:46 */                 loadingText.UpdateGeometry(meshInfo.mesh, i);
/* @LiF  - 2024-06-03 14:20:46 */             }
/* @LiF  - 2024-06-03 14:20:46 */         }
/* @LiF  - 2024-06-03 14:20:46 */ 
/* @LiF  - 2024-06-03 14:20:46 */         private void AnimateWave(TMP_TextInfo textInfo)
/* @LiF  - 2024-06-03 14:20:46 */         {
/* @LiF  - 2024-06-03 14:20:46 */             for (int i = 0; i < textInfo.characterCount; i++)
/* @LiF  - 2024-06-03 14:20:46 */             {
/* @LiF  - 2024-06-03 14:20:46 */                 TMP_CharacterInfo charInfo = textInfo.characterInfo[i];
/* @LiF  - 2024-06-03 14:20:46 */ 
/* @LiF  - 2024-06-03 14:20:46 */                 if (!charInfo.isVisible)
/* @LiF  - 2024-06-03 14:20:46 */                     continue;
/* @LiF  - 2024-06-03 14:20:46 */ 
/* @LiF  - 2024-06-03 14:20:46 */                 var vertices = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;
/* @LiF  - 2024-06-03 14:20:46 */                 Vector3 charMidBaseline = (vertices[charInfo.vertexIndex + 0] + vertices[charInfo.vertexIndex + 2]) / 2;
/* @LiF  - 2024-06-03 14:20:46 */ 
/* @LiF  - 2024-06-03 14:20:46 */                 for (int j = 0; j < 4; j++)
/* @LiF  - 2024-06-03 14:20:46 */                 {
/* @LiF  - 2024-06-03 14:20:46 */                     Vector3 offset = vertices[charInfo.vertexIndex + j] - charMidBaseline;
/* @LiF  - 2024-06-03 14:20:46 */                     offset.y += Mathf.Sin(Time.time * waveSpeed + charMidBaseline.x * waveDifference) * waveHeight;
/* @LiF  - 2024-06-03 14:20:46 */                     vertices[charInfo.vertexIndex + j] = charMidBaseline + offset;
/* @LiF  - 2024-06-03 14:20:46 */                 }
/* @LiF  - 2024-06-03 14:20:46 */             }
/* @LiF  - 2024-06-03 14:20:46 */         }
/* @LiF  - 2024-06-03 14:20:46 */     }
/* @LiF  - 2024-06-03 14:20:46 */ }