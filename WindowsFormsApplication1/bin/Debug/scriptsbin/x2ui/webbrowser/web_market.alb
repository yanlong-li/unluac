local marketWindow
local WIDTH = 604
local HEIGHT = 604
local OFFSET = 84
local TYPE = "NONE"
function ClearWebMarket()
  marketWindow = nil
  TYPE = "NONE"
end
local function CreateMarketWindow(id, parent)
  local window = SetViewOfWebbrowserWindow(id, parent, WIDTH, HEIGHT, OFFSET)
  window:SetSounds("web_play_diary")
  window.clearProc = ClearWebMarket
  return window
end
local function OnToggleWebMarket(target)
  if X2:IsWebEnable() and localeView.useWebContent then
    if marketWindow == nil then
      marketWindow = CreateMarketWindow("marketWindow", "UIParent")
      marketWindow.webBrowser:RequestMarket()
      marketWindow:Show(true)
    elseif marketWindow:IsVisible() then
      marketWindow:Show(false)
    end
  end
end
UIParent:SetEventHandler("TOGGLE_WEB_MARKET", OnToggleWebMarket)
