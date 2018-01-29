expedition:SetCloseOnEscape(true)
local OnHide = function()
  GetCreateExpeditionMessageBoxHandleWindow():Show(false)
end
expedition:SetHandler("OnHide", OnHide)
local OnTextChanged = function()
  if string.len(expedition.cmd.expeditionName:GetText()) >= 1 then
    expedition.cmd.confirm:Enable(true)
  else
    expedition.cmd.confirm:Enable(false)
  end
end
expedition.cmd.expeditionName:SetHandler("OnTextChanged", OnTextChanged)
function ShowCreateExpedition(show, alliance)
  if expedition:IsVisible() == show then
    return
  end
  if show == false then
    expedition:Show(false)
    return
  end
  local success, isOutLaw, isUserAlliance = expedition:FillAllianceInfo(alliance)
  if success == false then
    AddMessageToSysMsgWindow(X2Locale:LocalizeUiText(ERROR_MSG, "INTERNAL_ERROR"))
    return
  end
  if isOutLaw then
    local OnClick = function(arg)
      if expedition.outlaw:IsVisible() == false then
        return
      end
      local factionName = expedition.cmd.expeditionName:GetText()
      if factionName == nil then
        return
      end
      local sponsorId = expedition.outlaw.allianceId
      X2Faction:CreateExpedition(factionName, sponsorId)
      expedition:Show(false)
    end
    expedition.cmd.confirm:SetHandler("OnClick", OnClick)
  elseif isUserAlliance then
    local function OnClick(arg)
      local factionName = expedition.cmd.expeditionName:GetText()
      if factionName == nil then
        return
      end
      X2Faction:CreateExpedition(factionName, alliance)
      expedition:Show(false)
    end
    expedition.cmd.confirm:SetHandler("OnClick", OnClick)
  else
    local OnClick = function(arg)
      if expedition.selectSponsor:IsVisible() == false then
        return
      end
      local factionName = expedition.cmd.expeditionName:GetText()
      local selectedTab = expedition.selectSponsor.tab:GetSelectedTab()
      if selectedTab < 1 then
        AddMessageToSysMsgWindow(locale.expedition.notice)
        return
      end
      if factionName == nil then
        return
      end
      local sponsorId = expedition.selectSponsor.tab.window[selectedTab].allianceId
      X2Faction:CreateExpedition(factionName, sponsorId)
      expedition:Show(false)
    end
    expedition.cmd.confirm:SetHandler("OnClick", OnClick)
  end
  expedition:Show(true)
end
local events = {
  NPC_INTERACTION_END = function()
    expedition:Show(false)
  end
}
expedition:SetHandler("OnEvent", function(this, event, ...)
  events[event](...)
end)
RegistUIEvent(expedition, events)
