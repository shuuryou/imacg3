#include "rightclickassist.h"
#include "resource.h"

HINSTANCE hInst;
HHOOK hHook;
INPUT input;

void WinMainCRTStartup()
{
    int Result = WinMain(GetModuleHandle(0), 0, 0, 0);
    ExitProcess(Result);
}

int CALLBACK WinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPSTR lpCmdLine, _In_ int nCmdShow)
{
    InitCommonControls();

    input.type = INPUT_MOUSE;

    hHook = SetWindowsHookEx(WH_MOUSE_LL, hook_proc, hInst, 0);

    if (hHook == NULL)
    {
        ShowLastError();
        return 1;
    }

    MSG msg;
    while (GetMessage(&msg, NULL, 0, 0))
    {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }

    return (int)msg.wParam;
}

LRESULT CALLBACK hook_proc(int code, WPARAM wParam, LPARAM lParam)
{
    if (code == HC_ACTION && (wParam == WM_LBUTTONDOWN || wParam == WM_LBUTTONUP))
    {
        if ((GetAsyncKeyState(VK_CONTROL) & 0x8000) == 0)
            return false;

        // Exit on CTRL+F2+Left
        if ((GetAsyncKeyState(VK_F2) & 0x8000) != 0)
        {
            WCHAR msg[1000];
            WCHAR title[1000];

            LoadString(hInst, IDS_QUIT_MSG, msg, sizeof(msg));
            LoadString(hInst, IDS_QUIT_TITLE, title, sizeof(title));

            UnhookWindowsHookEx(hHook);
            MessageBox(NULL, msg, title, MB_OK);
            ExitProcess(0);
            goto end;
        }

        if (wParam == WM_LBUTTONUP)
            input.mi.dwFlags = MOUSEEVENTF_RIGHTUP;
        else
            input.mi.dwFlags = MOUSEEVENTF_RIGHTDOWN;
        
        SendInput(1, &input, sizeof(INPUT));
    }

    return CallNextHookEx(hHook, code, wParam, lParam);

end:
    ;
}

void ShowLastError()
{
    DWORD err = GetLastError();

    LPTSTR errmsg = nullptr;
    if (FormatMessage(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM, NULL, err, 0, (LPTSTR)&errmsg, 0, NULL) == 0)
        return;

    WCHAR title[1000];
    LoadString(hInst, IDS_QUIT_TITLE, title, sizeof(title));

    MessageBox(nullptr, errmsg, title, MB_OK | MB_ICONERROR);

    if (errmsg)
        LocalFree(errmsg);
}