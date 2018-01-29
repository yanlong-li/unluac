local handleEvents = {
  SHOW_SERVER_SELECT_WINDOW = function(visible)
    UIParent:LogAlways("EnterWorld")
    X2World:EnterWorld(1, -1)
  end,
  READY_TO_CONNECT_WORLD = function()
    UIParent:LogAlways("ConnectToWorld")
    X2LoginCharacter:ConnectToWorld()
  end
}
testWindow = CreateEmptyWindow("testWindow", "UIParent")
testWindow:SetExtent(0, 0)
testWindow:AddAnchor("TOPLEFT", 0, 0)
testWindow:Show(true)
testWindow:SetHandler("OnEvent", function(this, event, ...)
  handleEvents[event](...)
end)
testWindow:RegisterEvent("SHOW_SERVER_SELECT_WINDOW")
testWindow:RegisterEvent("READY_TO_CONNECT_WORLD")
X2:ConnectToServer("", Test:GetAccount(0), Test:GetPassword(0))
