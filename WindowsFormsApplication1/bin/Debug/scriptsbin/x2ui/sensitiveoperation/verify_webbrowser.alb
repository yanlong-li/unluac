if X2Util:GetGameProvider() == TENCENT then
  do
    local verifyWindow
    local WIDTH = 430
    local HEIGHT = 397
    local BROWSER_WIDTH = 410
    local BROWSER_HEIGHT = 350
    function ClearWebHelp()
      verifyWindow = nil
    end
    local function CreateVerifyWindow(id, parent)
      local window = CreateWindow(id, parent)
      window:ApplyUIScale(false)
      window:SetExtent(WIDTH, HEIGHT)
      window:AddAnchor("CENTER", "UIParent", 0, 0)
      window:Clickable(true)
      window:Show(false)
      window:SetSounds("web_play_diary")
      local webBrowser = window:CreateChildWidget("webview", "webBrowser", 0, true)
      window.webBrowser:SetExtent(BROWSER_WIDTH, BROWSER_HEIGHT)
      window.webBrowser:RemoveAllAnchors()
      window.webBrowser:AddAnchor("BOTTOM", window, 0, -2)
      function window:SetFocusHandler()
        function webBrowser:OnEnter()
          webBrowser:SetFocus()
        end
        webBrowser:SetHandler("OnEnter", webBrowser.OnEnter)
        function webBrowser:OnLeave()
          webBrowser:ClearFocus()
        end
        webBrowser:SetHandler("OnLeave", webBrowser.OnLeave)
      end
      window.clearProc = ClearWebHelp
      return window
    end
    function OnToggleSensitiveOperationVerify(url)
      if X2:IsWebEnable() then
        if verifyWindow == nil then
          verifyWindow = CreateVerifyWindow("verifyWindow", "UIParent")
        end
        if verifyWindow then
          verifyWindow:Show(true)
          verifyWindow.webBrowser:RequestSensitiveOperationVerify(url)
        end
      end
    end
    function ShowSensitiveOperationVerify(seqNum, url)
      local function SensitiveOperationCertiVerify(wnd, infoTable)
        function wnd:OkProc()
          OnToggleSensitiveOperationVerify(url)
          wnd:Show(false)
        end
        function wnd:CancelProc()
          X2Player:CancelSensitiveOperationVerify(seqNum)
          wnd:Show(false)
        end
        wnd:SetTitle(locale.sensitiveOperation.title)
        wnd:SetContent(locale.sensitiveOperation.content, true)
      end
      X2DialogManager:RequestDefaultDialog(SensitiveOperationCertiVerify, "")
    end
    UIParent:SetEventHandler("SENSITIVE_OPERATION_VERIFY", ShowSensitiveOperationVerify)
    function ShowSensitiveOperationVerifySuccess()
      local SensitiveOperationCertiVerifySuccess = function(wnd, infoTable)
        function wnd:OkProc()
          wnd:Show(false)
        end
        wnd:SetTitle(locale.sensitiveOperation.title)
        wnd:SetContent(locale.sensitiveOperation.successContent, true)
      end
      X2DialogManager:RequestNoticeDialog(SensitiveOperationCertiVerifySuccess, "")
    end
    UIParent:SetEventHandler("SENSITIVE_OPERATION_VERIFY_SUCCESS", ShowSensitiveOperationVerifySuccess)
  end
end
