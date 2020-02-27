//---------------------------------------------------------------
//
//  Copyright ©2015 D3DGear Technologies. All rights reserved
//
//---------------------------------------------------------------
#pragma once

#include <Windows.h>
#include <tchar.h>
#include <mmreg.h>
#include <strmif.h>
//#include "Windows.h"
//#include "tchar.h"
//#include "Mmreg.h"
//#include "Strmif.h"

//
// Overlay type
//
typedef enum
{
	OVERLAY_WEBCAM = 0,
	OVERLAY_MEDIAFILE,
	OVERLAY_BROWSERURL,

} OVERLAY_FORMAT;


//
// Screen shot file format
//
typedef enum
{
	SCREENSHORT_BMP,
	SCREENSHORT_JPG,
	SCREENSHORT_PNG,
	SCREENSHORT_GIF,

} SCREENSHORT_FORMAT;


//
// Movie file formats
//
typedef enum
{
	MOVIEFMT_AVI1 = 0,
	MOVIEFMT_AVI2,
	MOVIEFMT_ASF,
	MOVIEFMT_MP4,
	MOVIEFMT_RTMP,
	MOVIEFMT_RAWFRAMECAPTURE,

} MOVIE_FORMAT;

//
// Movie recording info structure
//
//      * For full screen/window resolution, set both dwMovieWidth and dwMovieHeight to 0.
//
typedef struct {
	MOVIE_FORMAT MovieFmt;
	DWORD dwMovieWidth;
	DWORD dwMovieHeight;
	DWORD dwFrameRate;
	BOOL  bEnableAudio;
	BOOL  bEnableMicrophone;
	BOOL  bShowHUD;
	FLOAT fHUDXSize;    // 0.f ~ 1.f. 
	FLOAT fHUDXCoord;   // -1.f ~ 1.f.
	FLOAT fHUDYCoord;   // -1.f ~ 1.f. 
	BOOL  bUseSurfaceShare;

} MOVIE_CMDLINE_INFO;

//
// Screen shot info structure
//
typedef struct {
	DWORD dwInitialIndex;
	DWORD dwSeries;
	DWORD dwInterval;
	DWORD dwSaveFmt;
	BOOL  bForceRenderTime;
	BOOL  bForceRenderFrame;

	DWORD dwCurTickCount;

} SCREENSHOT_CMDLINE_INFO;

//
// Screen shot info structure
//
typedef struct {
	DWORD dwSeries;
	DWORD dwInterval;

} BENCHMARK_CMDLINE_INFO;

//
// Capture mode
//
typedef enum {
	CAPTURE_SCREENSHOT = 0,
	CAPTURE_MOVIE,
	CAPTURE_BROADCAST,
	CAPTURE_BENCHMARK,
	CAPTURE_STOPALL,
	CAPTURE_UNKNOWN,

} CAPTURE_MODE;

//
// Capture status
//
typedef enum {
	CAPTURE_STATUS_ON,
	CAPTURE_STATUS_OFF,

} CAPTURE_STATUS;

//
// Capture info structure
//

#ifdef UNICODE
#define CAPTURE_CMDLINE_INFO  CAPTURE_CMDLINE_INFOW
#else
#define CAPTURE_CMDLINE_INFO  CAPTURE_CMDLINE_INFOA
#endif 

typedef struct {

	CHAR  szExeName[MAX_PATH];
	DWORD dwProcessId;

	CHAR  szCaptureFileName[MAX_PATH];

	CHAR  szRtmpUrl[MAX_PATH];
	CHAR  szStreamKey[MAX_PATH];

	CHAR  szLogFileName[MAX_PATH];
	CHAR  szKeyFileName[MAX_PATH];
	CHAR  szSaveFmt[MAX_PATH];
	CHAR  szCurFileName[MAX_PATH];

	CAPTURE_MODE CaptureMode;

	BOOL  bAddDate;
	BOOL  bAddTime;
	BOOL  bNoExit;
	DWORD dwCurSeries;

	SCREENSHOT_CMDLINE_INFO ScreenshotCmdInfo;
	MOVIE_CMDLINE_INFO MovieCmdInfo;

	CHAR  szRecDevName[MAX_PATH];
	CHAR  szMicDevName[MAX_PATH];

} CAPTURE_CMDLINE_INFOA;

typedef struct {

	WCHAR szExeName[MAX_PATH];
	DWORD dwProcessId;
	DWORD dwCurrentPId;

	WCHAR szCaptureFileName[MAX_PATH];

	WCHAR szRtmpUrl[MAX_PATH];
	WCHAR szStreamKey[MAX_PATH];

	WCHAR szLogFileName[MAX_PATH];
	WCHAR szKeyFileName[MAX_PATH];
	WCHAR szSaveFmt[MAX_PATH];
	WCHAR szCurFileName[MAX_PATH];

	CAPTURE_MODE CaptureMode;

	BOOL  bAddDate;
	BOOL  bAddTime;
	BOOL  bNoExit;
	DWORD dwCurSeries;


	MOVIE_CMDLINE_INFO MovieCmdInfo;
	SCREENSHOT_CMDLINE_INFO ScreenshotCmdInfo;
	BENCHMARK_CMDLINE_INFO BenchmarkCmdInfo;

	WCHAR szRecDevName[MAX_PATH];
	WCHAR szMicDevName[MAX_PATH];

} CAPTURE_CMDLINE_INFOW;

typedef enum _D3DGEAR_RFC_D3D_API_TYPE
{
	D3DGEAR_RFC_D3D_API_TYPE_OPENGL,
	D3DGEAR_RFC_D3D_API_TYPE_DX8,
	D3DGEAR_RFC_D3D_API_TYPE_DX9,
	D3DGEAR_RFC_D3D_API_TYPE_DX9EX,
	D3DGEAR_RFC_D3D_API_TYPE_DX10,
	D3DGEAR_RFC_D3D_API_TYPE_DX11,
	D3DGEAR_RFC_D3D_API_TYPE_DX12,
	D3DGEAR_RFC_D3D_API_TYPE_MANTLE,
	D3DGEAR_RFC_D3D_API_TYPE_VULKAN

}D3DGEAR_RFC_D3D_API_TYPE;

typedef struct _D3DGEAR_RFC_GAME_PROCESS_INFOA
{
	DWORD dwProcessId;
	CHAR szExeFileName[MAX_PATH];
	D3DGEAR_RFC_D3D_API_TYPE d3dApiType;
	DWORD dwBackBufferWidth;
	DWORD dwBackBufferHeight;
	BOOL bSupportSurfaceShare;
	LUID adapterLuid; // Only valid if bSupportSurfaceShare is TRUE
	HWND hRenderWnd;
	HWND hRootWnd;
	CHAR szRootWndTitle[MAX_PATH];
}D3DGEAR_RFC_GAME_PROCESS_INFOA, *PD3DGEAR_RFC_GAME_PROCESS_INFOA;
typedef const D3DGEAR_RFC_GAME_PROCESS_INFOA* PCD3DGEAR_RFC_GAME_PROCESS_INFOA;

typedef struct _D3DGEAR_RFC_GAME_PROCESS_INFOW
{
	DWORD dwProcessId;
	TCHAR szExeFileName[MAX_PATH];
	D3DGEAR_RFC_D3D_API_TYPE d3dApiType;
	DWORD dwBackBufferWidth;
	DWORD dwBackBufferHeight;
	BOOL bSupportSurfaceShare;
	LUID adapterLuid; // Only valid if bSupportSurfaceShare is TRUE
	HWND hRenderWnd;
	HWND hRootWnd;
	WCHAR szRootWndTitle[MAX_PATH];
	TCHAR szAppContainerSID[MAX_PATH];
}D3DGEAR_RFC_GAME_PROCESS_INFOW, *PD3DGEAR_RFC_GAME_PROCESS_INFOW;
typedef const D3DGEAR_RFC_GAME_PROCESS_INFOW* PCD3DGEAR_RFC_GAME_PROCESS_INFOW;

#ifdef UNICODE
#define CAPTURE_CMDLINE_INFO  CAPTURE_CMDLINE_INFOW
#else
#define CAPTURE_CMDLINE_INFO  CAPTURE_CMDLINE_INFOA
#endif 

#ifdef UNICODE
#define D3DGear_GetCaptureStatus  D3DGear_GetCaptureStatusW
#else
#define D3DGear_GetCaptureStatus  D3DGear_GetCaptureStatusA
#endif 

#ifdef UNICODE
#define D3DGear_ExecuteCaptureCommand  D3DGear_ExecuteCaptureCommandW
#else
#define D3DGear_ExecuteCaptureCommand  D3DGear_ExecuteCaptureCommandA
#endif 

#ifdef UNICODE
#define D3DGear_LoadEngine  D3DGear_LoadEngineW
#else
#define D3DGear_LoadEngine  D3DGear_LoadEngineA
#endif 

#ifdef UNICODE
#define D3DGEAR_RFC_GAME_PROCESS_INFO  D3DGEAR_RFC_GAME_PROCESS_INFOW
#else
#define D3DGEAR_RFC_GAME_PROCESS_INFO  D3DGEAR_RFC_GAME_PROCESS_INFOA
#endif 

#ifdef UNICODE
#define D3DGear_RFC_EnumerateGameProcesses  D3DGear_RFC_EnumerateGameProcessesW
#else
#define D3DGear_RFC_EnumerateGameProcesses  D3DGear_RFC_EnumerateGameProcessesA
#endif 

//
// Get current capture status
//
//      * pszExename indicates the target process from which the capture status is retrieved. 
//      * CaptureMode indicates the capture mode from which the capture status is retrieved. Currently only CAPTURE_MOVIE is supported.
//      * pCaptureStatus indicates the address to retrieve the requested catpure status. 
//
// Return value: 
//      The return value is Win32 error code.
//      If the function succeeds, the return value is ERROR_SUCCESS. 
//
LONG D3DGear_GetCaptureStatus(LPCTSTR pszExeName, CAPTURE_MODE CaptureMode, CAPTURE_STATUS *pCaptureStatus);

//
// Execute capture command
//
//      * The CAPTURE_CMDLINE_INFO::szExeName indicates the target process from which the movie/screenshot will be captured. 
//      * This function can be called from the same target game process. It also can be called from different process runs.
//        at same logon session.
//      * bAsync parameter indicates if the function does not need to wait for the completion of the execute command.
//
// Return value: 
//      The return value is Win32 error code.
//      If the function succeeds, the return value is ERROR_SUCCESS. 
//      If the function fails to wait for command response, the return value is WAIT_TIMEOUT.
//
LONG D3DGear_ExecuteCaptureCommand(CAPTURE_CMDLINE_INFO *pCaptureInfo, BOOL bAsync = FALSE);


//
// Load D3DGear Engine
//
//      * This funciton must be called from the context of target game process.
//      * This function must be called before initializing DirectX/OpenGL API.
//      * This funciton must be called after initializing Steam API if you use Steam SDK.
//
// Return value: 
//      The return value is Win32 error code.
//      If the function succeeds, the return value is ERROR_SUCCESS. 
//      If the function fails because of invalid license, the return value is PEERDIST_ERROR_NOT_LICENSED.
//      If the function fails because it fails to load D3DGear runtime engine from given path, the return value is ERROR_MOD_NOT_FOUND. 
LONG D3DGear_LoadEngine(LPCTSTR pszLicenseKey, LPCTSTR pszD3DGearPath);


//
// Enumerate current game processes
//
//      * ppGameProcessInfo is to receive a array of GAME_PROCESS_INFO. 
//      * pdwGameProcessCount is to receive the count of GAME_PROCESS_INFO array that received from ppGameProcessInfo. 
//      * Call D3DGear_RFC_MemFree to free array memory received from ppGameProcessInfo. 
//
// Return value: 
//      The return value is Win32 error code.
//      If the function succeeds, the return value is ERROR_SUCCESS. 
//
LONG D3DGear_RFC_EnumerateGameProcesses(D3DGEAR_RFC_GAME_PROCESS_INFO **ppGameProcessInfo, DWORD *pdwGameProcessCount);