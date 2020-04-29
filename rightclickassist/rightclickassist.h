#pragma once

#include <windows.h>
#include <stdlib.h>
#include <string.h>
#include <tchar.h>
#include <commctrl.h>

void WinMainCRTStartup();

int CALLBACK WinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPSTR lpCmdLine, _In_ int nCmdShow);

LRESULT CALLBACK hook_proc(int code, WPARAM wParam, LPARAM lParam);

void ShowLastError();