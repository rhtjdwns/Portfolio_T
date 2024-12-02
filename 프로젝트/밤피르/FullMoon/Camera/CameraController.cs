/* Git Blame Auto Generated */

/* @LiF       - 2024-06-01 15:37:58 */ using System.Linq;
/* @rhtjdwns  - 2024-04-03 23:04:18 */ using System.Collections.Generic;
/* @Lee SJ    - 2024-04-08 19:40:53 */ using UnityEngine;
/* @LiF       - 2024-03-20 00:19:45 */ using Cinemachine;
/* @LiF       - 2024-06-03 16:41:20 */ using FullMoon.Entities;
/* @LiF       - 2024-06-03 16:41:20 */ using UnityEngine.Tilemaps;
/* @LiF       - 2024-06-03 16:41:20 */ using UnityEngine.EventSystems;
/* @LiF       - 2024-06-01 15:37:58 */ using FullMoon.UI;
/* @Lee SJ    - 2024-03-21 22:29:59 */ using FullMoon.Input;
/* @rhtjdwns  - 2024-04-03 23:04:18 */ using FullMoon.Entities.Unit;
/* @LiF       - 2024-06-01 20:28:02 */ using FullMoon.Util;
/* @LiF       - 2024-03-19 22:36:22 */ 
/* @rhtjdwns  - 2024-03-22 01:32:57 */ namespace FullMoon.Camera
/* @LiF       - 2024-03-19 22:36:22 */ {
/* @rhtjdwns  - 2024-03-22 01:32:57 */     public class CameraController : MonoBehaviour
/* @rhtjdwns  - 2024-03-22 01:32:57 */     {
/* @rhtjdwns  - 2024-03-22 01:32:57 */         [SerializeField] private CinemachineFreeLook freeLookCamera;
/* @Lee SJ    - 2024-03-27 22:58:51 */ 
/* @Lee SJ    - 2024-03-27 22:58:51 */         [SerializeField] private bool enableCursorMovement;
/* @Lee SJ    - 2024-03-27 22:58:51 */         
/* @rhtjdwns  - 2024-03-22 01:32:57 */         [Header("Movement")]
/* @rhtjdwns  - 2024-03-22 01:32:57 */         [SerializeField] private float moveSpeed = 12f;
/* @rhtjdwns  - 2024-03-22 01:32:57 */         [SerializeField] private float shiftMoveSpeed = 25f;
/* @Lee SJ    - 2024-06-02 22:56:15 */         [SerializeField] private float cameraAreaLimit = 20f;
/* @LiF       - 2024-03-20 00:19:45 */     
/* @rhtjdwns  - 2024-03-22 01:32:57 */         [Header("Zoom")]
/* @Lee SJ    - 2024-05-14 00:57:12 */         [SerializeField] private float zoomSensitivity = 5f;
/* @Lee SJ    - 2024-05-14 00:57:12 */         [SerializeField] private float zoomSpeed = 10f;
/* @rhtjdwns  - 2024-03-22 01:32:57 */         [SerializeField] private float minFov = 20f;
/* @rhtjdwns  - 2024-03-22 01:32:57 */         [SerializeField] private float maxFov = 55f;
/* @rhtjdwns  - 2024-04-03 23:04:18 */         
/* @Lee SJ    - 2024-04-09 01:06:08 */         [Header("Rotation")]
/* @Lee SJ    - 2024-05-14 00:57:12 */         [SerializeField] private float rotationSensitivity = 3f;
/* @Lee SJ    - 2024-04-18 17:16:02 */         
/* @LiF       - 2024-06-02 11:54:10 */         [Header("Tile Map")]
/* @LiF       - 2024-06-02 11:54:10 */         [SerializeField] private GameObjectDictionary tileMap;
/* @LiF       - 2024-06-02 11:54:10 */         
/* @rhtjdwns  - 2024-04-05 14:11:14 */         [Header("UI")]
/* @rhtjdwns  - 2024-04-19 00:38:06 */         [SerializeField] private CursorController cursor;
/* @LiF       - 2024-04-19 01:01:11 */         
/* @Lee SJ    - 2024-04-18 17:16:02 */         private UnityEngine.Camera mainCamera;
/* @LiF       - 2024-06-02 12:13:55 */         private float targetFov;
/* @Lee SJ    - 2024-04-18 17:16:02 */         
/* @Lee SJ    - 2024-04-18 17:16:02 */         private Vector3 mousePos;
/* @Lee SJ    - 2024-04-18 17:16:02 */         private Ray mouseRay;
/* @Lee SJ    - 2024-04-18 17:16:02 */         
/* @Lee SJ    - 2024-04-09 01:45:23 */         private bool altRotation;
/* @rhtjdwns  - 2024-05-31 16:04:17 */ 
/* @LiF       - 2024-06-02 11:32:36 */         private bool canCraft;
/* @LiF       - 2024-06-02 01:55:43 */         private BuildingType buildingType;
/* @Lee SJ    - 2024-04-18 17:16:02 */         
/* @Lee SJ    - 2024-05-14 00:57:12 */         private List<BaseUnitController> selectedUnitList;
/* @LiF       - 2024-06-03 16:41:20 */ 
/* @LiF       - 2024-06-03 16:41:20 */         private WaveManager waveManager;
/* @LiF       - 2024-05-04 22:45:41 */         
/* @rhtjdwns  - 2024-04-03 23:04:18 */         private void Awake()
/* @rhtjdwns  - 2024-04-03 23:04:18 */         {
/* @rhtjdwns  - 2024-04-03 23:04:18 */             mainCamera = UnityEngine.Camera.main;
/* @rhtjdwns  - 2024-04-03 23:04:18 */             selectedUnitList = new List<BaseUnitController>();
/* @rhtjdwns  - 2024-04-03 23:04:18 */         }
/* @rhtjdwns  - 2024-04-03 23:04:18 */ 
/* @rhtjdwns  - 2024-03-22 01:32:57 */         private void Start()
/* @rhtjdwns  - 2024-03-22 01:32:57 */         {
/* @rhtjdwns  - 2024-05-11 15:20:14 */             targetFov = freeLookCamera.m_Lens.OrthographicSize;
/* @LiF       - 2024-06-03 16:41:20 */             waveManager = FindObjectOfType<WaveManager>();
/* @rhtjdwns  - 2024-03-22 01:32:57 */             PlayerInputManager.Instance.ZoomEvent.AddEvent(ZoomEvent);
/* @rhtjdwns  - 2024-03-22 01:32:57 */         }
/* @Lee SJ    - 2024-03-21 23:48:20 */ 
/* @Lee SJ    - 2024-04-09 01:06:08 */         private void Update()
/* @Lee SJ    - 2024-04-09 01:06:08 */         {
/* @Lee SJ    - 2024-05-14 00:57:12 */             selectedUnitList.RemoveAll(unit => unit == null || !unit.gameObject.activeInHierarchy);
/* @Lee SJ    - 2024-05-14 00:57:12 */             
/* @rhtjdwns  - 2024-05-11 15:20:14 */             freeLookCamera.m_Lens.OrthographicSize = Mathf.Lerp(freeLookCamera.m_Lens.OrthographicSize, targetFov, Time.deltaTime * zoomSpeed);
/* @Lee SJ    - 2024-04-09 01:06:08 */             
/* @Lee SJ    - 2024-04-09 01:06:08 */             mousePos = UnityEngine.InputSystem.Mouse.current.position.value;
/* @Lee SJ    - 2024-04-09 01:06:08 */             mouseRay = mainCamera.ScreenPointToRay(mousePos);
/* @Lee SJ    - 2024-04-09 01:06:08 */             
/* @Lee SJ    - 2024-04-09 01:06:08 */             MouseAction();
/* @rhtjdwns  - 2024-06-03 11:58:12 */             ButtonAction();
/* @Lee SJ    - 2024-04-09 01:06:08 */         }
/* @Lee SJ    - 2024-04-09 01:06:08 */         
/* @Lee SJ    - 2024-06-02 22:56:15 */         private Vector3 ClampToRotatedSquare(Vector3 position, Vector3 center, float limit, float angle)
/* @Lee SJ    - 2024-06-02 22:56:15 */         {
/* @Lee SJ    - 2024-06-02 22:56:15 */             // 각도를 라디안으로 변환
/* @Lee SJ    - 2024-06-02 22:56:15 */             float rad = angle * Mathf.Deg2Rad;
/* @Lee SJ    - 2024-06-02 22:56:15 */             float cos = Mathf.Cos(rad);
/* @Lee SJ    - 2024-06-02 22:56:15 */             float sin = Mathf.Sin(rad);
/* @Lee SJ    - 2024-06-02 22:56:15 */ 
/* @Lee SJ    - 2024-06-02 22:56:15 */             // 원점 중심의 회전된 좌표계로 변환
/* @Lee SJ    - 2024-06-02 22:56:15 */             Vector3 dir = position - center;
/* @Lee SJ    - 2024-06-02 22:56:15 */             float rotatedX = cos * dir.x - sin * dir.z;
/* @Lee SJ    - 2024-06-02 22:56:15 */             float rotatedZ = sin * dir.x + cos * dir.z;
/* @Lee SJ    - 2024-06-02 22:56:15 */ 
/* @Lee SJ    - 2024-06-02 22:56:15 */             // 회전된 좌표계를 기준으로 클램프
/* @Lee SJ    - 2024-06-02 22:56:15 */             rotatedX = Mathf.Clamp(rotatedX, -limit, limit);
/* @Lee SJ    - 2024-06-02 22:56:15 */             rotatedZ = Mathf.Clamp(rotatedZ, -limit, limit);
/* @Lee SJ    - 2024-06-02 22:56:15 */ 
/* @Lee SJ    - 2024-06-02 22:56:15 */             // 다시 원래 좌표계로 변환
/* @Lee SJ    - 2024-06-02 22:56:15 */             position.x = cos * rotatedX + sin * rotatedZ + center.x;
/* @Lee SJ    - 2024-06-02 22:56:15 */             position.z = -sin * rotatedX + cos * rotatedZ + center.z;
/* @Lee SJ    - 2024-06-02 22:56:15 */ 
/* @Lee SJ    - 2024-06-02 22:56:15 */             return position;
/* @Lee SJ    - 2024-06-02 22:56:15 */         }
/* @Lee SJ    - 2024-06-02 22:56:15 */         
/* @rhtjdwns  - 2024-03-22 01:32:57 */         private void FixedUpdate()
/* @Lee SJ    - 2024-03-21 23:48:20 */         {
/* @rhtjdwns  - 2024-03-22 01:32:57 */             Vector3 moveDirection = AdjustMovementToCamera(PlayerInputManager.Instance.move);
/* @rhtjdwns  - 2024-03-22 01:32:57 */ 
/* @Lee SJ    - 2024-03-27 22:58:51 */             if (moveDirection == Vector3.zero && enableCursorMovement)
/* @rhtjdwns  - 2024-03-22 01:32:57 */             {
/* @rhtjdwns  - 2024-03-22 01:32:57 */                 moveDirection = AdjustMovementToCamera(GetScreenMovementInput());
/* @LiF       - 2024-06-02 12:13:55 */                 cursor.SetCursorState(moveDirection != Vector3.zero ? CursorType.Camera : CursorType.Idle);
/* @rhtjdwns  - 2024-03-22 01:32:57 */             }
/* @Lee SJ    - 2024-03-21 23:48:20 */         
/* @rhtjdwns  - 2024-03-22 01:32:57 */             float movementSpeed = PlayerInputManager.Instance.shift ? shiftMoveSpeed : moveSpeed;
/* @Lee SJ    - 2024-06-02 22:56:15 */             Vector3 newPosition = transform.position + moveDirection * (movementSpeed * Time.fixedDeltaTime);
/* @Lee SJ    - 2024-06-02 22:56:15 */ 
/* @Lee SJ    - 2024-06-02 22:56:15 */             newPosition = ClampToRotatedSquare(newPosition, Vector3.zero, cameraAreaLimit, freeLookCamera.m_XAxis.Value);
/* @Lee SJ    - 2024-06-02 22:56:15 */             
/* @Lee SJ    - 2024-06-02 22:56:15 */             transform.position = newPosition;
/* @rhtjdwns  - 2024-03-22 01:32:57 */         }
/* @LiF       - 2024-03-20 00:19:45 */ 
/* @rhtjdwns  - 2024-03-22 01:32:57 */         private Vector2 GetScreenMovementInput()
/* @rhtjdwns  - 2024-03-22 01:32:57 */         {
/* @LiF       - 2024-06-02 12:13:55 */             if (!Application.isFocused || Cursor.lockState != CursorLockMode.Confined)
/* @rhtjdwns  - 2024-03-22 01:32:57 */             {
/* @rhtjdwns  - 2024-03-22 01:32:57 */                 return Vector2.zero;
/* @rhtjdwns  - 2024-03-22 01:32:57 */             }
/* @Lee SJ    - 2024-03-21 23:48:20 */ 
/* @rhtjdwns  - 2024-03-22 01:32:57 */             Vector2 mousePosition = UnityEngine.Input.mousePosition;
/* @rhtjdwns  - 2024-03-22 01:32:57 */             float normalizedX = (mousePosition.x / Screen.width) * 2 - 1;
/* @rhtjdwns  - 2024-03-22 01:32:57 */             float normalizedY = (mousePosition.y / Screen.height) * 2 - 1;
/* @Lee SJ    - 2024-03-21 23:48:20 */ 
/* @rhtjdwns  - 2024-03-22 01:32:57 */             Vector2 normalizedPosition = new Vector2(normalizedX, normalizedY);
/* @Lee SJ    - 2024-03-22 16:20:42 */             normalizedPosition.Normalize();
/* @Lee SJ    - 2024-03-21 23:48:20 */ 
/* @rhtjdwns  - 2024-03-22 01:32:57 */             // 화면 가장자리에 있는지 확인
/* @LiF       - 2024-06-02 12:13:55 */             if (Mathf.Abs(normalizedX) > 0.98f || Mathf.Abs(normalizedY) > 0.98f)
/* @rhtjdwns  - 2024-03-22 01:32:57 */             {
/* @rhtjdwns  - 2024-03-22 01:32:57 */                 return normalizedPosition;
/* @rhtjdwns  - 2024-03-22 01:32:57 */             }
/* @Lee SJ    - 2024-03-21 23:48:20 */ 
/* @rhtjdwns  - 2024-03-22 01:32:57 */             return Vector2.zero;
/* @rhtjdwns  - 2024-03-22 01:32:57 */         }
/* @Lee SJ    - 2024-03-21 23:48:20 */ 
/* @rhtjdwns  - 2024-03-22 01:32:57 */         private Vector3 AdjustMovementToCamera(Vector2 input)
/* @rhtjdwns  - 2024-03-22 01:32:57 */         {
/* @Lee SJ    - 2024-04-09 00:07:46 */             Vector3 forward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
/* @Lee SJ    - 2024-04-09 00:07:46 */             Vector3 right = mainCamera.transform.right;
/* @LiF       - 2024-03-20 00:19:45 */ 
/* @rhtjdwns  - 2024-03-22 01:32:57 */             forward.y = 0;
/* @rhtjdwns  - 2024-03-22 01:32:57 */             right.y = 0;
/* @LiF       - 2024-03-20 00:19:45 */ 
/* @rhtjdwns  - 2024-03-22 01:32:57 */             forward.Normalize();
/* @rhtjdwns  - 2024-03-22 01:32:57 */             right.Normalize();
/* @LiF       - 2024-03-20 00:19:45 */ 
/* @rhtjdwns  - 2024-03-22 01:32:57 */             return (forward * input.y + right * input.x);
/* @rhtjdwns  - 2024-03-22 01:32:57 */         }
/* @LiF       - 2024-03-20 00:19:45 */ 
/* @rhtjdwns  - 2024-03-22 01:32:57 */         private void ZoomEvent(Vector2 scrollValue)
/* @LiF       - 2024-03-19 22:36:22 */         {
/* @rhtjdwns  - 2024-03-22 01:32:57 */             if (scrollValue.y != 0f)
/* @rhtjdwns  - 2024-03-22 01:32:57 */             {
/* @Lee SJ    - 2024-04-08 19:40:53 */                 targetFov -= (scrollValue.y > 0f ? 1f : -1f) * zoomSensitivity;
/* @Lee SJ    - 2024-04-08 19:40:53 */                 targetFov = Mathf.Clamp(targetFov, minFov, maxFov);
/* @rhtjdwns  - 2024-03-22 01:32:57 */             }
/* @LiF       - 2024-03-19 22:36:22 */         }
/* @rhtjdwns  - 2024-04-03 23:04:18 */         
/* @Lee SJ    - 2024-06-02 22:56:15 */         public void CreateTileSetting(bool canCraft, BuildingType type)
/* @rhtjdwns  - 2024-05-31 16:04:17 */         {
/* @Lee SJ    - 2024-06-02 22:56:15 */             this.canCraft = canCraft;
/* @LiF       - 2024-06-02 01:55:43 */             buildingType = type;
/* @rhtjdwns  - 2024-05-31 16:04:17 */         }
/* @rhtjdwns  - 2024-05-31 16:04:17 */ 
/* @rhtjdwns  - 2024-04-03 23:04:18 */         #region Mouse
/* @rhtjdwns  - 2024-04-03 23:04:18 */ 
/* @rhtjdwns  - 2024-04-03 23:04:18 */         /// <summary>
/* @rhtjdwns  - 2024-04-03 23:04:18 */         /// 마우스 클릭 액션
/* @rhtjdwns  - 2024-04-03 23:04:18 */         /// </summary>
/* @rhtjdwns  - 2024-04-10 15:06:53 */         private void MouseAction()
/* @rhtjdwns  - 2024-04-03 23:04:18 */         {
/* @rhtjdwns  - 2024-04-03 23:04:18 */             // 마우스 왼쪽 버튼 처리
/* @rhtjdwns  - 2024-04-03 23:04:18 */             if (UnityEngine.InputSystem.Mouse.current.leftButton.wasPressedThisFrame)
/* @rhtjdwns  - 2024-04-03 23:04:18 */             {
/* @rhtjdwns  - 2024-04-03 23:04:18 */                 HandleLeftClick();
/* @rhtjdwns  - 2024-04-03 23:04:18 */             }
/* @rhtjdwns  - 2024-04-03 23:04:18 */             else if (UnityEngine.InputSystem.Mouse.current.leftButton.isPressed)
/* @rhtjdwns  - 2024-04-03 23:04:18 */             {
/* @rhtjdwns  - 2024-04-03 23:04:18 */                 HandleLeftDrag();
/* @rhtjdwns  - 2024-04-03 23:04:18 */             }
/* @rhtjdwns  - 2024-04-03 23:04:18 */             else if (UnityEngine.InputSystem.Mouse.current.leftButton.wasReleasedThisFrame)
/* @rhtjdwns  - 2024-04-03 23:04:18 */             {
/* @rhtjdwns  - 2024-04-03 23:04:18 */                 HandleLeftRelease();
/* @rhtjdwns  - 2024-04-03 23:04:18 */             }
/* @rhtjdwns  - 2024-04-03 23:04:18 */ 
/* @rhtjdwns  - 2024-04-03 23:04:18 */             // 마우스 오른쪽 버튼 처리
/* @rhtjdwns  - 2024-04-03 23:04:18 */             if (UnityEngine.InputSystem.Mouse.current.rightButton.wasPressedThisFrame)
/* @rhtjdwns  - 2024-04-03 23:04:18 */             {
/* @Lee SJ    - 2024-04-21 22:04:35 */                 if (EventSystem.current.IsPointerOverGameObject())
/* @Lee SJ    - 2024-04-21 22:04:35 */                 {
/* @Lee SJ    - 2024-04-21 22:04:35 */                     return;
/* @Lee SJ    - 2024-04-21 22:04:35 */                 }
/* @rhtjdwns  - 2024-04-03 23:04:18 */                 HandleRightClick();
/* @rhtjdwns  - 2024-04-03 23:04:18 */             }
/* @rhtjdwns  - 2024-04-03 23:04:18 */         }
/* @LiF       - 2024-06-02 11:32:36 */         
/* @LiF       - 2024-06-02 11:32:36 */         private void HandleBuildingTile(RaycastHit hitInfo)
/* @LiF       - 2024-06-02 11:32:36 */         {
/* @LiF       - 2024-06-02 11:32:36 */             if (!UnityEngine.AI.NavMesh.SamplePosition(hitInfo.point, out var samplePoint, 0.1f, (1 << UnityEngine.AI.NavMesh.GetAreaFromName("Walkable"))))
/* @LiF       - 2024-06-02 11:32:36 */             {
/* @LiF       - 2024-06-03 18:58:33 */                 CancelCrafting("지을 수 있는 공간이 없습니다.");
/* @LiF       - 2024-06-02 11:32:36 */                 return;
/* @LiF       - 2024-06-02 11:32:36 */             }
/* @LiF       - 2024-06-02 11:32:36 */ 
/* @LiF       - 2024-06-02 11:32:36 */             var unitList = GetAvailableUnits();
/* @LiF       - 2024-06-02 11:32:36 */             if (unitList.Count < 6)
/* @LiF       - 2024-06-02 11:32:36 */             {
/* @LiF       - 2024-06-03 18:58:33 */                 CancelCrafting("자원 유닛이 부족합니다.");
/* @LiF       - 2024-06-02 11:32:36 */                 return;
/* @LiF       - 2024-06-02 11:32:36 */             }
/* @LiF       - 2024-06-02 11:32:36 */ 
/* @LiF       - 2024-06-02 11:54:10 */             var sampleCellPosition = tileMap.GetComponentByName<Tilemap>("Building").WorldToCell(samplePoint.position);
/* @LiF       - 2024-06-02 11:54:10 */             if (tileMap.GetComponentByName<Tilemap>("Building").HasTile(sampleCellPosition))
/* @LiF       - 2024-06-02 11:32:36 */             {
/* @LiF       - 2024-06-03 18:58:33 */                 CancelCrafting("지을 수 있는 공간이 없습니다.");
/* @LiF       - 2024-06-02 11:32:36 */                 return;
/* @LiF       - 2024-06-02 11:32:36 */             }
/* @LiF       - 2024-06-02 11:32:36 */ 
/* @LiF       - 2024-06-02 11:32:36 */             CraftBuilding(unitList, hitInfo.point, samplePoint.position);
/* @LiF       - 2024-06-02 11:32:36 */         }
/* @LiF       - 2024-06-02 11:32:36 */ 
/* @LiF       - 2024-06-02 11:32:36 */         private void HandleGroundTile(RaycastHit hitInfo)
/* @LiF       - 2024-06-02 11:32:36 */         {
/* @LiF       - 2024-06-02 11:54:10 */             var sampleCellPosition = tileMap.GetComponentByName<Tilemap>("Ground").WorldToCell(hitInfo.point);
/* @LiF       - 2024-06-02 11:54:10 */             if (tileMap.GetComponentByName<Tilemap>("Ground").HasTile(sampleCellPosition) || hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
/* @LiF       - 2024-06-02 11:32:36 */             {
/* @LiF       - 2024-06-03 18:58:33 */                 CancelCrafting("해당 위치에 이미 땅이 존재합니다.");
/* @LiF       - 2024-06-02 11:32:36 */                 return;
/* @LiF       - 2024-06-02 11:32:36 */             }
/* @LiF       - 2024-06-03 16:41:20 */             
/* @LiF       - 2024-06-02 11:32:36 */             TileController.Instance.CreateTile(hitInfo.point, buildingType);
/* @LiF       - 2024-06-03 16:41:20 */             
/* @LiF       - 2024-06-03 16:41:20 */             OnCancelAction();
/* @LiF       - 2024-06-02 11:32:36 */         }
/* @LiF       - 2024-06-02 11:32:36 */ 
/* @LiF       - 2024-06-02 11:32:36 */         private List<CommonUnitController> GetAvailableUnits()
/* @LiF       - 2024-06-02 11:32:36 */         {
/* @LiF       - 2024-06-02 11:32:36 */             return FindObjectsByType<CommonUnitController>(FindObjectsSortMode.None)
/* @LiF       - 2024-06-02 11:32:36 */                 .Where(t => t.UnitType is "Player" && t.gameObject.activeInHierarchy && t.Alive)
/* @LiF       - 2024-06-02 11:32:36 */                 .Take(6)
/* @LiF       - 2024-06-02 11:32:36 */                 .ToList();
/* @LiF       - 2024-06-02 11:32:36 */         }
/* @LiF       - 2024-06-02 11:32:36 */ 
/* @LiF       - 2024-06-02 11:32:36 */         private void CraftBuilding(List<CommonUnitController> unitList, Vector3 buildPoint, Vector3 samplePoint)
/* @LiF       - 2024-06-02 11:32:36 */         {
/* @LiF       - 2024-06-02 11:32:36 */             foreach (var unit in unitList)
/* @LiF       - 2024-06-02 11:32:36 */             {
/* @LiF       - 2024-06-02 11:32:36 */                 unit.CraftBuilding(buildPoint);
/* @LiF       - 2024-06-02 11:32:36 */             }
/* @LiF       - 2024-06-03 16:41:20 */             
/* @LiF       - 2024-06-02 11:32:36 */             TileController.Instance.CreateTile(samplePoint, buildingType);
/* @LiF       - 2024-06-03 16:41:20 */             
/* @LiF       - 2024-06-03 16:41:20 */             OnCancelAction();
/* @LiF       - 2024-06-02 11:32:36 */         }
/* @LiF       - 2024-06-02 11:32:36 */ 
/* @LiF       - 2024-06-03 18:58:33 */         private void CancelCrafting(string message, string color = "#FF7C7F")
/* @LiF       - 2024-06-02 11:32:36 */         {
/* @LiF       - 2024-06-03 16:41:20 */             OnCancelAction();
/* @LiF       - 2024-06-02 11:32:36 */             ToastManager.Instance.ShowToast(message, color);
/* @LiF       - 2024-06-02 11:32:36 */         }
/* @rhtjdwns  - 2024-04-03 23:04:18 */ 
/* @rhtjdwns  - 2024-04-03 23:04:18 */         private void HandleLeftClick()
/* @rhtjdwns  - 2024-04-03 23:04:18 */         {
/* @Lee SJ    - 2024-04-21 22:04:35 */             if (EventSystem.current.IsPointerOverGameObject())
/* @Lee SJ    - 2024-04-21 22:04:35 */             {
/* @Lee SJ    - 2024-04-21 22:04:35 */                 return;
/* @Lee SJ    - 2024-04-21 22:04:35 */             }
/* @rhtjdwns  - 2024-04-03 23:04:18 */ 
/* @Lee SJ    - 2024-04-09 01:45:23 */             if (PlayerInputManager.Instance.rotation)
/* @Lee SJ    - 2024-04-09 01:45:23 */             {
/* @Lee SJ    - 2024-04-09 01:45:23 */                 altRotation = true;
/* @Lee SJ    - 2024-04-09 01:45:23 */                 return;
/* @Lee SJ    - 2024-04-09 01:45:23 */             }
/* @Lee SJ    - 2024-04-09 01:45:23 */ 
/* @LiF       - 2024-06-02 11:32:36 */             if (canCraft)
/* @rhtjdwns  - 2024-05-31 16:04:17 */             {
/* @LiF       - 2024-06-02 11:32:36 */                 if (Physics.Raycast(mouseRay, out var hitInfo, Mathf.Infinity, (1 << LayerMask.NameToLayer("Ground"))) && buildingType is not BuildingType.Ground)
/* @rhtjdwns  - 2024-05-31 16:04:17 */                 {
/* @LiF       - 2024-06-02 11:32:36 */                     HandleBuildingTile(hitInfo);
/* @rhtjdwns  - 2024-05-31 16:04:17 */                 }
/* @LiF       - 2024-06-02 11:32:36 */                 else if (Physics.Raycast(mouseRay, out var waterHitInfo, Mathf.Infinity, (1 << LayerMask.NameToLayer("Water")) | (1 << LayerMask.NameToLayer("Ground"))) && buildingType is BuildingType.Ground)
/* @rhtjdwns  - 2024-05-31 16:04:17 */                 {
/* @LiF       - 2024-06-02 11:32:36 */                     HandleGroundTile(waterHitInfo);
/* @rhtjdwns  - 2024-05-31 16:04:17 */                 }
/* @LiF       - 2024-06-02 11:32:36 */                 else
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 {
/* @LiF       - 2024-06-03 18:58:33 */                     CancelCrafting("생성할 수 없는 위치입니다.");
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 }
/* @rhtjdwns  - 2024-06-02 06:42:57 */             }
/* @rhtjdwns  - 2024-05-31 16:04:17 */ 
/* @Lee SJ    - 2024-05-14 00:57:12 */             DeselectAll();
/* @Lee SJ    - 2024-05-14 00:57:12 */             
/* @Lee SJ    - 2024-05-14 00:57:12 */             if (Physics.Raycast(mouseRay, out var hit, Mathf.Infinity, (1 << LayerMask.NameToLayer("Unit"))))
/* @rhtjdwns  - 2024-04-03 23:04:18 */             {
/* @rhtjdwns  - 2024-04-03 23:04:18 */                 var unitController = hit.transform.GetComponent<BaseUnitController>();
/* @Lee SJ    - 2024-05-14 00:57:12 */                 ClickSelectUnit(unitController);
/* @rhtjdwns  - 2024-04-03 23:04:18 */             }
/* @rhtjdwns  - 2024-04-03 23:04:18 */         }
/* @rhtjdwns  - 2024-04-03 23:04:18 */ 
/* @rhtjdwns  - 2024-04-03 23:04:18 */         private void HandleLeftDrag()
/* @rhtjdwns  - 2024-04-03 23:04:18 */         {
/* @Lee SJ    - 2024-04-09 01:45:23 */             if (altRotation == false)
/* @Lee SJ    - 2024-04-09 01:45:23 */             {
/* @Lee SJ    - 2024-04-09 01:45:23 */                 return;
/* @Lee SJ    - 2024-04-09 01:45:23 */             }
/* @Lee SJ    - 2024-04-09 01:45:23 */             
/* @Lee SJ    - 2024-04-09 01:45:23 */             if (PlayerInputManager.Instance.rotation == false)
/* @Lee SJ    - 2024-04-09 01:45:23 */             {
/* @Lee SJ    - 2024-04-09 01:45:23 */                 return;
/* @Lee SJ    - 2024-04-09 01:45:23 */             }
/* @Lee SJ    - 2024-04-09 01:45:23 */ 
/* @Lee SJ    - 2024-04-09 01:45:23 */             freeLookCamera.m_XAxis.Value += freeLookCamera.m_XAxis.m_InputAxisValue * rotationSensitivity;
/* @rhtjdwns  - 2024-04-03 23:04:18 */         }
/* @rhtjdwns  - 2024-04-03 23:04:18 */ 
/* @rhtjdwns  - 2024-04-03 23:04:18 */         private void HandleLeftRelease()
/* @rhtjdwns  - 2024-04-03 23:04:18 */         {
/* @Lee SJ    - 2024-04-09 01:45:23 */             altRotation = false;
/* @rhtjdwns  - 2024-04-03 23:04:18 */         }
/* @rhtjdwns  - 2024-04-03 23:04:18 */ 
/* @rhtjdwns  - 2024-04-03 23:04:18 */         private void HandleRightClick()
/* @rhtjdwns  - 2024-04-03 23:04:18 */         {
/* @Lee SJ    - 2024-04-21 22:04:35 */             if (EventSystem.current.IsPointerOverGameObject())
/* @Lee SJ    - 2024-04-21 22:04:35 */             {
/* @Lee SJ    - 2024-04-21 22:04:35 */                 return;
/* @Lee SJ    - 2024-04-21 22:04:35 */             }
/* @Lee SJ    - 2024-04-21 22:04:35 */             
/* @Lee SJ    - 2024-05-14 00:57:12 */             if (Physics.Raycast(mouseRay, out var hit, Mathf.Infinity, (1 << LayerMask.NameToLayer("Ground"))))
/* @rhtjdwns  - 2024-04-18 09:55:38 */             {
/* @Lee SJ    - 2024-05-14 00:57:12 */                 if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
/* @Lee SJ    - 2024-05-14 00:57:12 */                 {
/* @Lee SJ    - 2024-05-14 00:57:12 */                     MoveSelectedUnits(hit.point);
/* @LiF       - 2024-06-02 12:13:55 */                     // cursor.SetMoveAniTarget(hit.point);
/* @Lee SJ    - 2024-05-14 00:57:12 */                 }
/* @rhtjdwns  - 2024-04-18 09:55:38 */             }
/* @Lee SJ    - 2024-05-14 00:57:12 */         }
/* @rhtjdwns  - 2024-04-18 09:55:38 */ 
/* @Lee SJ    - 2024-05-14 00:57:12 */         private void MoveSelectedUnits(Vector3 hitInfoPoint)
/* @Lee SJ    - 2024-05-14 00:57:12 */         {
/* @Lee SJ    - 2024-05-14 00:57:12 */             foreach (var unit in selectedUnitList)
/* @rhtjdwns  - 2024-04-03 23:04:18 */             {
/* @Lee SJ    - 2024-05-14 00:57:12 */                 if (unit.UnitType is "Enemy")
/* @rhtjdwns  - 2024-04-03 23:04:18 */                 {
/* @Lee SJ    - 2024-05-14 00:57:12 */                     continue;
/* @rhtjdwns  - 2024-04-03 23:04:18 */                 }
/* @Lee SJ    - 2024-05-14 00:57:12 */ 
/* @Lee SJ    - 2024-05-14 00:57:12 */                 if (unit.Flag is null)
/* @rhtjdwns  - 2024-04-03 23:04:18 */                 {
/* @Lee SJ    - 2024-05-14 00:57:12 */                     unit.MoveToPosition(hitInfoPoint);
/* @Lee SJ    - 2024-05-14 00:57:12 */                     return;
/* @rhtjdwns  - 2024-04-05 14:11:14 */                 }
/* @Lee SJ    - 2024-05-14 00:57:12 */                 
/* @Lee SJ    - 2024-05-14 00:57:12 */                 unit.Flag.MoveToPosition(hitInfoPoint);
/* @rhtjdwns  - 2024-04-05 14:11:14 */             }
/* @rhtjdwns  - 2024-04-05 14:11:14 */         }
/* @rhtjdwns  - 2024-04-05 14:11:14 */ 
/* @rhtjdwns  - 2024-04-03 23:04:18 */         /// <summary>
/* @rhtjdwns  - 2024-04-03 23:04:18 */         /// 마우스 클릭으로 유닛을 선택할 때 호출
/* @rhtjdwns  - 2024-04-03 23:04:18 */         /// </summary>
/* @rhtjdwns  - 2024-04-03 23:04:18 */         private void ClickSelectUnit(BaseUnitController newUnit)
/* @rhtjdwns  - 2024-04-03 23:04:18 */         {
/* @rhtjdwns  - 2024-04-03 23:04:18 */             // 기존에 선택되어 있는 모든 유닛 해제
/* @rhtjdwns  - 2024-04-03 23:04:18 */             DeselectAll();
/* @rhtjdwns  - 2024-04-03 23:04:18 */             
/* @rhtjdwns  - 2024-04-03 23:04:18 */             SelectUnit(newUnit);
/* @rhtjdwns  - 2024-04-03 23:04:18 */         }
/* @rhtjdwns  - 2024-04-03 23:04:18 */ 
/* @rhtjdwns  - 2024-04-03 23:04:18 */         /// <summary>
/* @rhtjdwns  - 2024-04-03 23:04:18 */         /// 모든 유닛의 선택을 해제할 때 호출
/* @rhtjdwns  - 2024-04-03 23:04:18 */         /// </summary>
/* @rhtjdwns  - 2024-04-03 23:04:18 */         private void DeselectAll()
/* @rhtjdwns  - 2024-04-03 23:04:18 */         {
/* @rhtjdwns  - 2024-04-03 23:04:18 */             foreach (var unit in selectedUnitList)
/* @rhtjdwns  - 2024-04-03 23:04:18 */             {
/* @rhtjdwns  - 2024-04-03 23:04:18 */                 unit.Deselect();
/* @rhtjdwns  - 2024-04-03 23:04:18 */             }
/* @rhtjdwns  - 2024-04-03 23:04:18 */ 
/* @rhtjdwns  - 2024-04-03 23:04:18 */             selectedUnitList.Clear();
/* @rhtjdwns  - 2024-04-03 23:04:18 */         }
/* @rhtjdwns  - 2024-04-03 23:04:18 */ 
/* @rhtjdwns  - 2024-04-03 23:04:18 */         /// <summary>
/* @rhtjdwns  - 2024-04-03 23:04:18 */         /// 매개변수로 받아온 newUnit 선택 설정
/* @rhtjdwns  - 2024-04-03 23:04:18 */         /// </summary>
/* @rhtjdwns  - 2024-04-03 23:04:18 */         private void SelectUnit(BaseUnitController newUnit)
/* @rhtjdwns  - 2024-04-03 23:04:18 */         {
/* @rhtjdwns  - 2024-04-03 23:04:18 */             // 유닛이 선택되었을 때 호출하는 메소드
/* @rhtjdwns  - 2024-04-03 23:04:18 */             newUnit.Select();
/* @rhtjdwns  - 2024-04-03 23:04:18 */             // 선택한 유닛 정보를 리스트에 저장
/* @rhtjdwns  - 2024-04-03 23:04:18 */             selectedUnitList.Add(newUnit);
/* @rhtjdwns  - 2024-04-03 23:04:18 */         }
/* @rhtjdwns  - 2024-04-21 17:35:53 */ 
/* @rhtjdwns  - 2024-04-03 23:04:18 */         #endregion Mouse
/* @rhtjdwns  - 2024-06-03 10:53:15 */ 
/* @rhtjdwns  - 2024-06-03 10:53:15 */         #region Button
/* @rhtjdwns  - 2024-06-03 10:53:15 */ 
/* @rhtjdwns  - 2024-06-03 11:58:12 */         private void ButtonAction()
/* @rhtjdwns  - 2024-06-03 11:58:12 */         {
/* @LiF       - 2024-06-03 16:41:20 */             if (PlayerInputManager.Instance.cancel)
/* @LiF       - 2024-06-03 16:41:20 */             {
/* @LiF       - 2024-06-03 16:41:20 */                 OnCancelAction();
/* @LiF       - 2024-06-03 16:41:20 */             }
/* @rhtjdwns  - 2024-06-03 11:58:12 */         }
/* @rhtjdwns  - 2024-06-03 11:58:12 */ 
/* @rhtjdwns  - 2024-06-03 10:53:15 */         private void OnCancelAction()
/* @rhtjdwns  - 2024-06-03 10:53:15 */         {
/* @LiF       - 2024-06-03 16:41:20 */             canCraft = false;
/* @LiF       - 2024-06-03 16:41:20 */             buildingType = BuildingType.None;
/* @LiF       - 2024-06-03 16:41:20 */             waveManager.DisableAllUnlockButton();
/* @rhtjdwns  - 2024-06-03 10:53:15 */         }
/* @rhtjdwns  - 2024-06-03 10:53:15 */ 
/* @rhtjdwns  - 2024-06-03 10:53:15 */         #endregion Button
/* @Lee SJ    - 2024-06-02 22:56:15 */         
/* @Lee SJ    - 2024-06-02 22:56:15 */ #if UNITY_EDITOR
/* @Lee SJ    - 2024-06-02 22:56:15 */         private void OnDrawGizmos()
/* @Lee SJ    - 2024-06-02 22:56:15 */         {
/* @Lee SJ    - 2024-06-02 22:56:15 */             Gizmos.color = Color.green;
/* @Lee SJ    - 2024-06-02 22:56:15 */ 
/* @Lee SJ    - 2024-06-02 22:56:15 */             Vector3 center = Vector3.zero;
/* @Lee SJ    - 2024-06-02 22:56:15 */             Vector3 size = new Vector3(cameraAreaLimit * 2, 1, cameraAreaLimit * 2);
/* @Lee SJ    - 2024-06-02 22:56:15 */         
/* @Lee SJ    - 2024-06-02 22:56:15 */             Gizmos.matrix = Matrix4x4.TRS(center, Quaternion.Euler(0, freeLookCamera.m_XAxis.Value, 0), Vector3.one);
/* @Lee SJ    - 2024-06-02 22:56:15 */             Gizmos.DrawWireCube(Vector3.zero, size);
/* @Lee SJ    - 2024-06-02 22:56:15 */         }
/* @Lee SJ    - 2024-06-02 22:56:15 */ #endif
/* @LiF       - 2024-03-19 22:36:22 */     }
/* @rhtjdwns  - 2024-04-05 14:11:14 */ }