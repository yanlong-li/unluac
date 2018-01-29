local wnd
local function CreateMobilizationOrder(id, parent)
  if nil == wnd then
    wnd = SetViewOfMobilizationOrder(id, parent)
  else
    wnd:Show(true)
  end
end
local function ShowMobilizationOrder(param)
  CreateMobilizationOrder("MobilizationOrder", "UIParent")
  wnd.showTime = param.openSecond * 1000
  local alreadyRequest = false
  local result = MOBILIZATION_ORDER_RESULT.CANCEL
  wnd.heroName:SetText(GetCommonText("mobilization_order_hero_name", param.heroName))
  wnd.condition:SetText(GetCommonText("issuance_of_mobilization_order_condition", tostring(param.limitLevel), tostring(param.limitLeadershipPoint), param.zoneGroupName))
  wnd.checkBtn:AddAnchor("BOTTOM", wnd, "BOTTOM", -(wnd.checkBtn.textButton:GetWidth() / 2), -35)
  wnd.checkBtn:SetChecked(false)
  function wnd:OnHide()
    if false == alreadyRequest then
      X2Faction:RequestMobilizationOrder(result, param.heroId, param.zoneGroupType)
    end
  end
  wnd:SetHandler("OnHide", wnd.OnHide)
  function wnd.acceptBtn:OnClick()
    if X2Player:PlayerInCombat() == true then
      AddMessageToSysMsgWindow(X2Locale:LocalizeUiText(ERROR_MSG, "IMPOSSIBLE_COMBAT_STATE"))
      return
    end
    result = MOBILIZATION_ORDER_RESULT.ACCEPT
    wnd:Show(false)
  end
  wnd.acceptBtn:SetHandler("OnClick", wnd.acceptBtn.OnClick)
  function wnd.noBtn:OnClick()
    wnd:Show(false)
  end
  wnd.noBtn:SetHandler("OnClick", wnd.noBtn.OnClick)
  function wnd.checkBtn:OnCheckChanged()
    X2Faction:RequestMobilizationOrderNotRecv(wnd.checkBtn:GetChecked())
  end
  wnd.checkBtn:SetHandler("OnCheckChanged", wnd.checkBtn.OnCheckChanged)
  function OnUpdate(self, dt)
    wnd.showTime = wnd.showTime - dt
    if wnd.showTime < 0 then
      X2Faction:RequestMobilizationOrder(MOBILIZATION_ORDER_RESULT.TIME_OVER, param.heroId, param.zoneGroupType)
      wnd.showTime = 0
      alreadyRequest = true
      wnd:Show(false)
    end
    wnd.remainTime:SetText(GetCommonText("remain_time", FormatTime(wnd.showTime, false)))
  end
  wnd:SetHandler("OnUpdate", OnUpdate)
  local events = {
    LEFT_LOADING = function()
      wnd:Show(false)
    end
  }
  wnd:SetHandler("OnEvent", function(this, event, ...)
    events[event](...)
  end)
  RegistUIEvent(wnd, events)
end
UIParent:SetEventHandler("MOBILIZATION_ORDER", ShowMobilizationOrder)
