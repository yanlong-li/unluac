local eventWindow
local WIDTH = WINDOW_SIZE.SMALL
local HEIGHT = 450
local OFFSET = 0
local HEIGHT_OFFSET = 45
local timeStampKey = "inGameWebNotification_one_day_hide"
local function CreateEventWindow(id, parent)
  local sideMargin, titleMargin, bottomMargin = GetWindowMargin()
  local window = SetViewOfWebbrowserWindow(id, parent, WIDTH, HEIGHT, OFFSET, HEIGHT_OFFSET)
  window:RemoveAllAnchors()
  window:AddAnchor("TOPLEFT", "UIParent", 28, 120)
  local text = X2Locale:LocalizeUiText(COMMON_TEXT, "hide_window")
  local checkButton = CreateCheckButton(window:GetId() .. ".checkButton", window, text)
  checkButton:AddAnchor("BOTTOMLEFT", window, sideMargin + -5, -sideMargin + 5)
  function checkButton:CheckBtnCheckChagnedProc()
    local checked = checkButton:GetChecked()
    if checked then
      window.timeStamp = X2Time:GetLocalDate()
    else
      window.timeStamp = nil
    end
  end
  function window:clearProc()
    if window.timeStamp ~= nil then
      local stamp = string.format("%s-%s-%s", window.timeStamp.year, window.timeStamp.month, window.timeStamp.day)
      UI:SetUIStamp(timeStampKey, stamp)
    end
    eventWindow = nil
  end
  return window
end
local function OnToggleEventWindow(url)
  local localData = X2Time:GetLocalDate()
  local stamp = string.format("%s-%s-%s", localData.year, localData.month, localData.day)
  local savedStamp = UI:GetUIStamp(timeStampKey)
  if stamp == savedStamp then
    return
  end
  if X2:IsWebEnable() and localeView.useWebContent and url ~= nil then
    if eventWindow == nil then
      eventWindow = CreateEventWindow("inGameWebNotificationWindow", "UIParent")
      eventWindow.webBrowser:SetURL(url)
      eventWindow:Show(true)
    elseif eventWindow:IsVisible() then
      eventWindow:Show(false)
    end
  end
  if RaiseEventCenter ~= nil then
    RaiseEventCenter()
  end
end
UIParent:SetEventHandler("TOGGLE_IN_GAME_NOTICE", OnToggleEventWindow)
