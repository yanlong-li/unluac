local eventWindow
local WIDTH = 394
local HEIGHT = 212
local OFFSET = 0
function ClearEventView()
  eventWindow = nil
end
local function CreateEventWindow(id, parent)
  local window = SetViewOfWebbrowserWindow(id, parent, WIDTH, HEIGHT, OFFSET)
  window.clearProc = ClearEventView
  return window
end
local function OnToggleEventWindow(url)
  webbrowserLocale.OnToggleEventWindow(eventWindow, url)
end
UIParent:SetEventHandler("OPEN_PROMOTION_EVENT_URL", OnToggleEventWindow)
