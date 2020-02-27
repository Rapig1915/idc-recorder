// This is the main DLL file.
#include "Recorder.h"
#include <memory>
#include <msclr\marshal.h>
#include <msclr\marshal_cppstd.h>
#include "cleanup.h"
#include "ManagedIStream.h"
#include "internal_recorder.h"
using namespace ScreenRecorderLib;
using namespace nlohmann;

#pragma region "For Game Recorder"

#if (_MSC_VER >= 1915)
#define no_init_all deprecated
#endif

#include "D3GearSDK.h"
#include "assert.h"
#include < stdio.h >
#include < stdlib.h >
#include < vcclr.h >

#ifndef _WIN64
#pragma comment(lib, "D3DGearSDK-VS2017-32.lib") 
#else
#pragma comment(lib, "D3DGearSDK-VS2017-64.lib") 
#endif

#pragma endregion

Recorder::Recorder(RecorderOptions^ options)
{
	lRec = new internal_recorder();
	SetOptions(options);
	createErrorCallback();
	createCompletionCallback();
	createStatusCallback();
}

void Recorder::SetOptions(RecorderOptions^ options) {
	if (options && lRec) {
		if (options->VideoOptions) {
			lRec->SetVideoBitrate(options->VideoOptions->Bitrate);
			lRec->SetVideoQuality(options->VideoOptions->Quality);
			lRec->SetVideoFps(options->VideoOptions->Framerate);
			lRec->SetFixedFramerate(options->VideoOptions->IsFixedFramerate);
			lRec->SetH264EncoderProfile((UINT32)options->VideoOptions->EncoderProfile);
			lRec->SetVideoBitrateMode((UINT32)options->VideoOptions->BitrateMode);
			switch (options->VideoOptions->SnapshotFormat)
			{
			case ImageFormat::BMP:
				lRec->SetSnapshotSaveFormat(GUID_ContainerFormatBmp);
				break;
			case ImageFormat::JPEG:
				lRec->SetSnapshotSaveFormat(GUID_ContainerFormatJpeg);
				break;
			case ImageFormat::TIFF:
				lRec->SetSnapshotSaveFormat(GUID_ContainerFormatTiff);
				break;
			default:
			case ImageFormat::PNG:
				lRec->SetSnapshotSaveFormat(GUID_ContainerFormatPng);
				break;
			}
		}
		if (options->DisplayOptions) {
			RECT rect;
			rect.left = options->DisplayOptions->Left;
			rect.top = options->DisplayOptions->Top;
			rect.right = options->DisplayOptions->Right;
			rect.bottom = options->DisplayOptions->Bottom;
			lRec->SetDestRectangle(rect);
			lRec->SetDisplayOutput(msclr::interop::marshal_as<std::wstring>(options->DisplayOptions->MonitorDeviceName));
		}

		if (options->AudioOptions) {
			lRec->SetAudioEnabled(options->AudioOptions->IsAudioEnabled);
			lRec->SetOutputDeviceEnabled(options->AudioOptions->IsOutputDeviceEnabled);
			lRec->SetInputDeviceEnabled(options->AudioOptions->IsInputDeviceEnabled);
			lRec->SetAudioBitrate((UINT32)options->AudioOptions->Bitrate);
			lRec->SetAudioChannels((UINT32)options->AudioOptions->Channels);
			if (options->AudioOptions->AudioOutputDevice != nullptr) {
				lRec->SetOutputDevice(msclr::interop::marshal_as<std::wstring>(options->AudioOptions->AudioOutputDevice));
			}
			if (options->AudioOptions->AudioInputDevice != nullptr) {
				lRec->SetInputDevice(msclr::interop::marshal_as<std::wstring>(options->AudioOptions->AudioInputDevice));
			}
		}
		if (options->MouseOptions) {
			lRec->SetMousePointerEnabled(options->MouseOptions->IsMousePointerEnabled);
			lRec->SetDetectMouseClicks(options->MouseOptions->IsMouseClicksDetected);
			lRec->SetMouseClickDetectionLMBColor(msclr::interop::marshal_as<std::string>(options->MouseOptions->MouseClickDetectionColor));
			lRec->SetMouseClickDetectionRMBColor(msclr::interop::marshal_as<std::string>(options->MouseOptions->MouseRightClickDetectionColor));
			lRec->SetMouseClickDetectionRadius(options->MouseOptions->MouseClickDetectionRadius);
			lRec->SetMouseClickDetectionDuration(options->MouseOptions->MouseClickDetectionDuration);
			lRec->SetMouseClickDetectionMode((UINT32)options->MouseOptions->MouseClickDetectionMode);
		}

		lRec->SetRecorderMode((UINT32)options->RecorderMode);
		lRec->SetIsThrottlingDisabled(options->IsThrottlingDisabled);
		lRec->SetIsLowLatencyModeEnabled(options->IsLowLatencyEnabled);
		lRec->SetIsFastStartEnabled(options->IsMp4FastStartEnabled);
		lRec->SetIsHardwareEncodingEnabled(options->IsHardwareEncodingEnabled);
		lRec->SetIsFragmentedMp4Enabled(options->IsFragmentedMp4Enabled);
	}
}

List<String^>^ Recorder::GetSystemAudioDevices(AudioDeviceSource source)
{
	std::vector<std::wstring> vector;
	EDataFlow dFlow;

	switch (source)
	{
	case  AudioDeviceSource::OutputDevices:
		dFlow = eRender;
		break;
	case AudioDeviceSource::InputDevices:
		dFlow = eCapture;
		break;
	case AudioDeviceSource::All:
		dFlow = eAll;
		break;
	default:
		break;
	}

	List<String^>^ devices = gcnew List<String^>();

	HRESULT hr = CPrefs::list_devices(dFlow, &vector);

	if (hr == S_OK)
	{
		if (vector.size() != 0)
		{
			for (UINT i = 0; i < vector.size(); ++i)
			{
				devices->Add(gcnew String(vector[i].c_str()));
			}
		}
	}
	return devices;
}

void Recorder::createErrorCallback() {
	InternalErrorCallbackDelegate^ fp = gcnew InternalErrorCallbackDelegate(this, &Recorder::EventFailed);
	_errorDelegateGcHandler = GCHandle::Alloc(fp);
	IntPtr ip = Marshal::GetFunctionPointerForDelegate(fp);
	CallbackErrorFunction cb = static_cast<CallbackErrorFunction>(ip.ToPointer());
	lRec->RecordingFailedCallback = cb;

}
void Recorder::createCompletionCallback() {
	InternalCompletionCallbackDelegate^ fp = gcnew InternalCompletionCallbackDelegate(this, &Recorder::EventComplete);
	_completedDelegateGcHandler = GCHandle::Alloc(fp);
	IntPtr ip = Marshal::GetFunctionPointerForDelegate(fp);
	CallbackCompleteFunction cb = static_cast<CallbackCompleteFunction>(ip.ToPointer());
	lRec->RecordingCompleteCallback = cb;
}
void Recorder::createStatusCallback() {
	InternalStatusCallbackDelegate^ fp = gcnew InternalStatusCallbackDelegate(this, &Recorder::EventStatusChanged);
	_statusChangedDelegateGcHandler = GCHandle::Alloc(fp);
	IntPtr ip = Marshal::GetFunctionPointerForDelegate(fp);
	CallbackStatusChangedFunction cb = static_cast<CallbackStatusChangedFunction>(ip.ToPointer());
	lRec->RecordingStatusChangedCallback = cb;
}
Recorder::~Recorder()
{
	this->!Recorder();
	GC::SuppressFinalize(this);
}

Recorder::!Recorder() {
	if (lRec) {
		delete lRec;
		lRec = nullptr;
	}
	if (m_ManagedStream) {
		delete m_ManagedStream;
		m_ManagedStream = nullptr;
	}
	_statusChangedDelegateGcHandler.Free();
	_errorDelegateGcHandler.Free();
	_completedDelegateGcHandler.Free();
}

void Recorder::EventComplete(std::wstring str, fifo_map<std::wstring, int> delays)
{
	List<FrameData^>^ frameInfos = gcnew List<FrameData^>();

	for (auto x : delays) {
		frameInfos->Add(gcnew FrameData(gcnew String(x.first.c_str()), x.second));
	}
	if (m_ManagedStream) {
		delete m_ManagedStream;
		m_ManagedStream = nullptr;
	}
	RecordingCompleteEventArgs^ args = gcnew RecordingCompleteEventArgs(gcnew String(str.c_str()), frameInfos);
	OnRecordingComplete(this, args);
}
void Recorder::EventFailed(std::wstring str)
{
	if (m_ManagedStream) {
		delete m_ManagedStream;
		m_ManagedStream = nullptr;
	}
	OnRecordingFailed(this, gcnew RecordingFailedEventArgs(gcnew String(str.c_str())));
}
void Recorder::EventStatusChanged(int status)
{
	RecorderStatus recorderStatus = (RecorderStatus)status;
	Status = recorderStatus;
	OnStatusChanged(this, gcnew RecordingStatusEventArgs(recorderStatus));
}
Recorder^ Recorder::CreateRecorder() {
	return gcnew Recorder(nullptr);
}

Recorder^ Recorder::CreateRecorder(RecorderOptions ^ options)
{
	Recorder^ rec = gcnew Recorder(options);
	return rec;
}
void Recorder::Record(System::Runtime::InteropServices::ComTypes::IStream^ stream) {
	IStream *pNativeStream = (IStream*)Marshal::GetComInterfaceForObject(stream, System::Runtime::InteropServices::ComTypes::IStream::typeid).ToPointer();
	lRec->BeginRecording(pNativeStream);
}
void Recorder::Record(System::IO::Stream^ stream) {
	m_ManagedStream = new ManagedIStream(stream);
	lRec->BeginRecording(m_ManagedStream);
}
void Recorder::Record(System::String^ path) {
	std::wstring stdPathString = msclr::interop::marshal_as<std::wstring>(path);
	lRec->BeginRecording(stdPathString);
}
void Recorder::Pause() {
	lRec->PauseRecording();
}
void Recorder::Resume() {
	lRec->ResumeRecording();
}
void Recorder::Stop() {
	lRec->EndRecording();
}

GameRecorder::GameRecorder()
{
	IsRecording = false;
}

List<GameProcessInfo^>^ GameRecorder::LoadGameList()
{
	List<GameProcessInfo^>^ processes = gcnew List<GameProcessInfo^>();

	LONG lRet;
	DWORD dwGameProcCount = 0;
	D3DGEAR_RFC_GAME_PROCESS_INFO *pGameInfo = NULL;
	do
	{
		lRet = D3DGear_RFC_EnumerateGameProcesses(&pGameInfo, &dwGameProcCount);
		if (ERROR_SUCCESS != lRet)
		{
			//assert(0);
			return processes;
		}

		if (dwGameProcCount)
			break;

		// if no process, try again after 1 second
		Sleep(1000);

		lRet = D3DGear_RFC_EnumerateGameProcesses(&pGameInfo, &dwGameProcCount);
		if (ERROR_SUCCESS != lRet)
		{
			//assert(0);
			return processes;
		}

		break;

	} while (!dwGameProcCount);

	if (pGameInfo)
	{
		for (UINT i = 0; i < dwGameProcCount; i++)
		{
			processes->Add(gcnew GameProcessInfo(pGameInfo[i].dwProcessId, gcnew String(pGameInfo[i].szRootWndTitle), gcnew String(pGameInfo[i].szExeFileName)));
		}
	}
	
	return processes;
}

int GameRecorder::ShotGame(String^ processExeName, String^ targetFileName)
{
	pin_ptr<const wchar_t> wchProcessExeName = PtrToStringChars(processExeName);
	pin_ptr<const wchar_t> wchTargetFileName = PtrToStringChars(targetFileName);
	LONG lRet;
	CAPTURE_CMDLINE_INFO CaptureCmdInfo;

	wchar_t szProcessExeName[MAX_PATH];
	wchar_t szTargetFileName[MAX_PATH];

	wcscpy_s(szProcessExeName, processExeName->Length + 1, wchProcessExeName);
	wcscpy_s(szTargetFileName, targetFileName->Length + 1, wchTargetFileName);

	ZeroMemory(&CaptureCmdInfo, sizeof(CaptureCmdInfo));
	_tcscpy_s(CaptureCmdInfo.szExeName, _countof(CaptureCmdInfo.szExeName), szProcessExeName);
	_tcscpy_s(CaptureCmdInfo.szCaptureFileName, _countof(CaptureCmdInfo.szCaptureFileName), szTargetFileName);
	_tcscpy_s(CaptureCmdInfo.szSaveFmt, _countof(CaptureCmdInfo.szSaveFmt), _T("bmp"));
	CaptureCmdInfo.CaptureMode = CAPTURE_SCREENSHOT;
	CaptureCmdInfo.ScreenshotCmdInfo.dwSeries = 1;
	CaptureCmdInfo.ScreenshotCmdInfo.dwSaveFmt = SCREENSHORT_BMP;

	lRet = D3DGear_ExecuteCaptureCommand(&CaptureCmdInfo);
	if (ERROR_SUCCESS != lRet)
	{
		//assert(0);
		return 0;
	}

	return 1;
}

int GameRecorder::LoadD3DEngine()
{
	LONG lRet;
	lRet = D3DGear_LoadEngine(_T("D3DGearLicenseKey"), NULL);
	if (ERROR_SUCCESS != lRet)
	{
		//assert(0);
	}

	return lRet;
}

int GameRecorder::Start(String^ processExeName, String^ videoPath, RecorderOptions^ options)
{
	if (IsRecording)
		return -1;

	SetOptions(options);

	LONG lRet;
	CAPTURE_CMDLINE_INFO CaptureCmdInfo;
	int nLoop = 0;
	CAPTURE_STATUS captureStatus;

	pin_ptr<const wchar_t> wchProcessExeName = PtrToStringChars(processExeName);
	pin_ptr<const wchar_t> wchVideoPath = PtrToStringChars(videoPath);
	pin_ptr<const wchar_t> wchAudioDeviceOutput = PtrToStringChars(options->AudioOptions->AudioOutputDevice);
	pin_ptr<const wchar_t> wchAudioDeviceInput = PtrToStringChars(options->AudioOptions->AudioInputDevice);

	wchar_t szProcessExeName[MAX_PATH], szTargetFileName[MAX_PATH], szAudioDeviceOutput[MAX_PATH], szAudioDeviceInput[MAX_PATH];

	wcscpy_s(szProcessExeName, processExeName->Length + 1, wchProcessExeName);
	wcscpy_s(szTargetFileName, videoPath->Length + 1, wchVideoPath);
	wcscpy_s(szAudioDeviceOutput, options->AudioOptions->AudioOutputDevice->Length + 1, wchAudioDeviceOutput);
	wcscpy_s(szAudioDeviceInput, options->AudioOptions->AudioInputDevice->Length + 1, wchAudioDeviceInput);

	//
	// Fill in capture parameters
	//

	ZeroMemory(&CaptureCmdInfo, sizeof(CaptureCmdInfo));
	_tcscpy_s(CaptureCmdInfo.szExeName, _countof(CaptureCmdInfo.szExeName), szProcessExeName);
	_tcscpy_s(CaptureCmdInfo.szCaptureFileName, _countof(CaptureCmdInfo.szCaptureFileName), szTargetFileName);
	_tcscpy_s(CaptureCmdInfo.szSaveFmt, _countof(CaptureCmdInfo.szSaveFmt), _T("mp4"));
	CaptureCmdInfo.CaptureMode = CAPTURE_MOVIE;
	CaptureCmdInfo.MovieCmdInfo.dwMovieWidth = options->VideoOptions->OutputWidth;
	CaptureCmdInfo.MovieCmdInfo.dwMovieHeight = options->VideoOptions->OutputWidth;
	CaptureCmdInfo.MovieCmdInfo.dwFrameRate = options->VideoOptions->Framerate;
	CaptureCmdInfo.MovieCmdInfo.MovieFmt = MOVIEFMT_MP4;
	CaptureCmdInfo.MovieCmdInfo.bEnableAudio = (options->AudioOptions->IsAudioEnabled && options->AudioOptions->IsOutputDeviceEnabled) ? TRUE : FALSE;
	CaptureCmdInfo.MovieCmdInfo.bEnableMicrophone = (options->AudioOptions->IsAudioEnabled && options->AudioOptions->IsInputDeviceEnabled) ? TRUE : FALSE;
	_tcscpy_s(CaptureCmdInfo.szRecDevName, _countof(CaptureCmdInfo.szRecDevName), szAudioDeviceOutput);
	_tcscpy_s(CaptureCmdInfo.szMicDevName, _countof(CaptureCmdInfo.szMicDevName), szAudioDeviceInput);
	CaptureCmdInfo.MovieCmdInfo.bShowHUD = TRUE;
	CaptureCmdInfo.MovieCmdInfo.fHUDXSize = .05f;
	CaptureCmdInfo.MovieCmdInfo.fHUDXCoord = .9f;
	CaptureCmdInfo.MovieCmdInfo.fHUDYCoord = .9f;

	lRet = D3DGear_ExecuteCaptureCommand(&CaptureCmdInfo);
	if (ERROR_SUCCESS != lRet)
	{
		//assert(0);
		return lRet;
	}

	IsRecording = true;

	// sucesss : ERROR_SUCCESS = 0
	m_CurrentRecordingGame = processExeName;
	m_VideoPath = videoPath + "_0_0.mp4";
	return 0;
}

int GameRecorder::Stop()
{
	if (!IsRecording)
		return 0;

	LONG lRet;
	CAPTURE_CMDLINE_INFO CaptureCmdInfo;
	int nLoop = 0;
	CAPTURE_STATUS captureStatus;

	pin_ptr<const wchar_t> wchProcessExeName = PtrToStringChars(m_CurrentRecordingGame);
	wchar_t szProcessExeName[MAX_PATH];

	wcscpy_s(szProcessExeName, m_CurrentRecordingGame->Length + 1, wchProcessExeName);
	//
	// Fill in stop command parameters
	//
	ZeroMemory(&CaptureCmdInfo, sizeof(CaptureCmdInfo));
	_tcscpy_s(CaptureCmdInfo.szExeName, _countof(CaptureCmdInfo.szExeName), szProcessExeName);
	CaptureCmdInfo.CaptureMode = CAPTURE_STOPALL;

	lRet = D3DGear_ExecuteCaptureCommand(&CaptureCmdInfo);
	if (ERROR_SUCCESS != lRet)
	{
		//assert(0);
		return lRet;
	}

	captureStatus = CAPTURE_STATUS_ON;
	while (captureStatus == CAPTURE_STATUS_ON)
	{
		lRet = D3DGear_GetCaptureStatus(szProcessExeName, CAPTURE_MOVIE, &captureStatus);
		if (ERROR_SUCCESS != lRet)
		{
			//assert(0);
			return lRet;
		}
	}

	IsRecording = false;
	return 0;
}

// 0 : capture off
// 1 : capture on
// -1 : error
int GameRecorder::GetCaptureStatus()
{
	if (!IsRecording)
		return 0;

	LONG lRet;
	CAPTURE_STATUS captureStatus;

	pin_ptr<const wchar_t> wchProcessExeName = PtrToStringChars(m_CurrentRecordingGame);
	wchar_t szProcessExeName[MAX_PATH];

	wcscpy_s(szProcessExeName, m_CurrentRecordingGame->Length + 1, wchProcessExeName);
	
	captureStatus = CAPTURE_STATUS_ON;
	lRet = D3DGear_GetCaptureStatus(szProcessExeName, CAPTURE_MOVIE, &captureStatus);
	if (ERROR_SUCCESS != lRet)
	{
		return -1;
	}

	if (captureStatus != CAPTURE_STATUS_ON)
		IsRecording = false;

	return captureStatus == CAPTURE_STATUS_ON ? 1 : 0;
}

void GameRecorder::SetOptions(RecorderOptions^ options)
{
	m_RecoderOptions = options;
}