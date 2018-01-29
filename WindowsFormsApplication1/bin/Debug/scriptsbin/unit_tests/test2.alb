local handleEvents = {
  SHOW_CHARACTER_SELECT_WINDOW = function()
    UIParent:LogAlways("SelectCharacter")
  end
}
testWindow = CreateEmptyWindow("testWindow", "UIParent")
testWindow:SetExtent(0, 0)
testWindow:AddAnchor("TOPLEFT", 0, 0)
testWindow:Show(true)
testWindow:SetHandler("OnEvent", function(this, event, ...)
  handleEvents[event](...)
end)
testWindow:RegisterEvent("SHOW_CHARACTER_SELECT_WINDOW")
X2LoginCharacter:SelectCharacter(1)
