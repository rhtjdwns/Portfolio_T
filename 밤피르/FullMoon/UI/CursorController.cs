/* Git Blame Auto Generated */

/* @LiF       - 2024-05-04 22:45:41 */ using MyBox;
/* @rhtjdwns  - 2024-04-19 00:38:06 */ using System.Linq;
/* @LiF       - 2024-05-04 22:45:41 */ using System.Collections.Generic;
/* @rhtjdwns  - 2024-04-19 00:38:06 */ using UnityEngine;
/* @rhtjdwns  - 2024-04-19 00:38:06 */ using FullMoon.Util;
/* @rhtjdwns  - 2024-04-19 00:38:06 */ 
/* @rhtjdwns  - 2024-04-19 00:38:06 */ namespace FullMoon.UI
/* @rhtjdwns  - 2024-04-19 00:38:06 */ {
/* @rhtjdwns  - 2024-04-19 00:38:06 */     public enum CursorType
/* @rhtjdwns  - 2024-04-19 00:38:06 */     {
/* @rhtjdwns  - 2024-04-19 00:38:06 */         Idle,
/* @rhtjdwns  - 2024-04-19 00:38:06 */         Attack,
/* @rhtjdwns  - 2024-04-19 00:38:06 */         Move,
/* @rhtjdwns  - 2024-04-20 13:05:38 */         Unit,
/* @rhtjdwns  - 2024-04-21 17:35:53 */         Create,
/* @LiF       - 2024-05-04 22:45:41 */         Camera
/* @rhtjdwns  - 2024-04-19 00:38:06 */     }
/* @rhtjdwns  - 2024-04-19 00:38:06 */ 
/* @rhtjdwns  - 2024-04-19 00:38:06 */     public class CursorController : MonoBehaviour
/* @rhtjdwns  - 2024-04-19 00:38:06 */     {
/* @LiF       - 2024-05-04 22:45:41 */         [SerializeField] private bool enable = true;
/* @LiF       - 2024-05-04 22:45:41 */         
/* @LiF       - 2024-05-04 22:45:41 */         [Separator("Mouse Cursor Settings")] 
/* @LiF       - 2024-05-04 22:45:41 */ 
/* @LiF       - 2024-05-04 22:45:41 */         [SerializeField] private CursorType cursorType;
/* @rhtjdwns  - 2024-04-19 00:38:06 */ 
/* @LiF       - 2024-05-04 22:45:41 */         [SerializeField] private List<Texture2D> textures;
/* @rhtjdwns  - 2024-04-19 00:38:06 */ 
/* @LiF       - 2024-05-04 22:45:41 */         [SerializeField] private GameObject moveAnim;
/* @rhtjdwns  - 2024-04-21 14:58:17 */ 
/* @Lee SJ    - 2024-04-21 22:04:35 */         private void Start()
/* @rhtjdwns  - 2024-04-19 00:38:06 */         {
/* @rhtjdwns  - 2024-04-19 00:38:06 */             cursorType = CursorType.Idle;
/* @LiF       - 2024-04-22 02:36:43 */             textures = textures.Select(tex => ScaleTexture(tex, 0.3f)).ToList();
/* @rhtjdwns  - 2024-04-19 00:38:06 */         }
/* @rhtjdwns  - 2024-04-19 00:38:06 */ 
/* @Lee SJ    - 2024-04-21 22:04:35 */         private void Update()
/* @rhtjdwns  - 2024-04-19 00:38:06 */         {
/* @rhtjdwns  - 2024-04-19 00:38:06 */             UpdateCursorState();
/* @rhtjdwns  - 2024-04-19 00:38:06 */         }
/* @rhtjdwns  - 2024-04-19 00:38:06 */ 
/* @rhtjdwns  - 2024-04-21 14:58:17 */         public void SetMoveAniTarget(Vector3 pos)
/* @rhtjdwns  - 2024-04-21 14:58:17 */         {
/* @Lee SJ    - 2024-05-08 18:25:40 */             ObjectPoolManager.Instance.SpawnObject(moveAnim, pos + new Vector3(0, 0.5f), Quaternion.identity);
/* @rhtjdwns  - 2024-04-21 14:58:17 */         }
/* @rhtjdwns  - 2024-04-21 14:58:17 */ 
/* @rhtjdwns  - 2024-04-19 00:38:06 */         public void SetCursorState(CursorType type)
/* @rhtjdwns  - 2024-04-19 00:38:06 */         {
/* @rhtjdwns  - 2024-04-24 22:24:12 */             if (cursorType is not CursorType.Idle && type is CursorType.Camera)
/* @rhtjdwns  - 2024-04-24 22:24:12 */             {
/* @rhtjdwns  - 2024-04-24 22:24:12 */                 return;
/* @rhtjdwns  - 2024-04-24 22:24:12 */             }
/* @rhtjdwns  - 2024-04-19 00:38:06 */             cursorType = type;
/* @rhtjdwns  - 2024-04-19 00:38:06 */         }
/* @rhtjdwns  - 2024-04-19 00:38:06 */ 
/* @rhtjdwns  - 2024-04-21 14:58:17 */         private void UpdateCursorState()
/* @rhtjdwns  - 2024-04-19 00:38:06 */         {
/* @LiF       - 2024-06-02 12:13:55 */             if (!Application.isPlaying || !Application.isFocused || enable == false)
/* @Lee SJ    - 2024-04-21 22:04:35 */             {
/* @Lee SJ    - 2024-04-21 22:04:35 */                 return;
/* @Lee SJ    - 2024-04-21 22:04:35 */             }
/* @Lee SJ    - 2024-04-21 22:04:35 */             
/* @rhtjdwns  - 2024-04-19 00:38:06 */             switch (cursorType)
/* @rhtjdwns  - 2024-04-19 00:38:06 */             {
/* @rhtjdwns  - 2024-04-19 00:38:06 */                 case CursorType.Idle:
/* @rhtjdwns  - 2024-04-19 00:38:06 */                     Cursor.SetCursor(textures[0], Vector2.zero, CursorMode.ForceSoftware);
/* @rhtjdwns  - 2024-04-19 00:38:06 */                     break;
/* @rhtjdwns  - 2024-04-19 00:38:06 */                 case CursorType.Attack:
/* @Lee SJ    - 2024-04-21 22:04:35 */                     Cursor.SetCursor(textures[1], new Vector2(textures[1].width / 2f, textures[1].height / 2f), CursorMode.ForceSoftware);
/* @rhtjdwns  - 2024-04-19 00:38:06 */                     break;
/* @rhtjdwns  - 2024-04-19 00:38:06 */                 case CursorType.Move:
/* @rhtjdwns  - 2024-04-19 00:38:06 */                     Cursor.SetCursor(textures[2], Vector2.zero, CursorMode.ForceSoftware);
/* @rhtjdwns  - 2024-04-19 00:38:06 */                     break;
/* @rhtjdwns  - 2024-04-20 13:05:38 */                 case CursorType.Unit:
/* @rhtjdwns  - 2024-04-20 13:05:38 */                     Cursor.SetCursor(textures[3], Vector2.zero, CursorMode.ForceSoftware);
/* @rhtjdwns  - 2024-04-20 13:05:38 */                     break;
/* @rhtjdwns  - 2024-04-21 17:35:53 */                 case CursorType.Create:
/* @rhtjdwns  - 2024-04-21 17:35:53 */                     Cursor.SetCursor(textures[4], Vector2.zero, CursorMode.ForceSoftware);
/* @rhtjdwns  - 2024-04-21 17:35:53 */                     break;
/* @rhtjdwns  - 2024-04-24 22:24:12 */                 case CursorType.Camera:
/* @rhtjdwns  - 2024-04-24 22:24:12 */                     Cursor.SetCursor(textures[5], Vector2.zero, CursorMode.ForceSoftware);
/* @rhtjdwns  - 2024-04-24 22:24:12 */                     break;
/* @rhtjdwns  - 2024-04-19 00:38:06 */             }
/* @rhtjdwns  - 2024-04-19 00:38:06 */         }
/* @rhtjdwns  - 2024-04-19 00:38:06 */ 
/* @rhtjdwns  - 2024-04-19 00:38:06 */         // Texture2D 크기 조정
/* @Lee SJ    - 2024-04-21 22:04:35 */         private Texture2D ScaleTexture(Texture2D source, float scaleFactor)
/* @rhtjdwns  - 2024-04-19 00:38:06 */         {
/* @Lee SJ    - 2024-04-21 22:04:35 */             if (Mathf.Approximately(scaleFactor, 1f))
/* @rhtjdwns  - 2024-04-19 00:38:06 */             {
/* @rhtjdwns  - 2024-04-19 00:38:06 */                 return source;
/* @rhtjdwns  - 2024-04-19 00:38:06 */             }
/* @Lee SJ    - 2024-04-21 22:04:35 */ 
/* @Lee SJ    - 2024-04-21 22:04:35 */             if (Mathf.Approximately(scaleFactor, 0f))
/* @rhtjdwns  - 2024-04-19 00:38:06 */             {
/* @rhtjdwns  - 2024-04-19 00:38:06 */                 return Texture2D.blackTexture;
/* @rhtjdwns  - 2024-04-19 00:38:06 */             }
/* @rhtjdwns  - 2024-04-19 00:38:06 */ 
/* @Lee SJ    - 2024-04-21 22:04:35 */             int newWidth = Mathf.RoundToInt(source.width * scaleFactor);
/* @Lee SJ    - 2024-04-21 22:04:35 */             int newHeight = Mathf.RoundToInt(source.height * scaleFactor);
/* @rhtjdwns  - 2024-04-19 00:38:06 */             
/* @Lee SJ    - 2024-04-21 22:04:35 */             Color[] scaledTexPixels = new Color[newWidth * newHeight];
/* @rhtjdwns  - 2024-04-19 00:38:06 */ 
/* @Lee SJ    - 2024-04-21 22:04:35 */             for (int yCord = 0; yCord < newHeight; yCord++)
/* @rhtjdwns  - 2024-04-19 00:38:06 */             {
/* @Lee SJ    - 2024-04-21 22:04:35 */                 float vCord = yCord / (newHeight * 1f);
/* @Lee SJ    - 2024-04-21 22:04:35 */                 int scanLineIndex = yCord * newWidth;
/* @rhtjdwns  - 2024-04-19 00:38:06 */ 
/* @Lee SJ    - 2024-04-21 22:04:35 */                 for (int xCord = 0; xCord < newWidth; xCord++)
/* @rhtjdwns  - 2024-04-19 00:38:06 */                 {
/* @Lee SJ    - 2024-04-21 22:04:35 */                     float uCord = xCord / (newWidth * 1f);
/* @rhtjdwns  - 2024-04-19 00:38:06 */ 
/* @Lee SJ    - 2024-04-21 22:04:35 */                     scaledTexPixels[scanLineIndex + xCord] = source.GetPixelBilinear(uCord, vCord);
/* @rhtjdwns  - 2024-04-19 00:38:06 */                 }
/* @rhtjdwns  - 2024-04-19 00:38:06 */             }
/* @rhtjdwns  - 2024-04-19 00:38:06 */ 
/* @rhtjdwns  - 2024-04-19 00:38:06 */             // Create Scaled Texture
/* @Lee SJ    - 2024-04-21 22:04:35 */             Texture2D result = new Texture2D(newWidth, newHeight, source.format, false);
/* @Lee SJ    - 2024-04-21 22:04:35 */             result.SetPixels(scaledTexPixels, 0);
/* @rhtjdwns  - 2024-04-19 00:38:06 */             result.Apply();
/* @rhtjdwns  - 2024-04-19 00:38:06 */ 
/* @rhtjdwns  - 2024-04-19 00:38:06 */             return result;
/* @rhtjdwns  - 2024-04-19 00:38:06 */         }
/* @rhtjdwns  - 2024-04-19 00:38:06 */     }
/* @rhtjdwns  - 2024-04-19 00:38:06 */ }
/* @rhtjdwns  - 2024-04-19 00:38:06 */ 
