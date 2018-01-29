local wnd
local function CreateIssuanceOfMobilizationOrder(id, parent)
  if nil == wnd then
    wnd = SetViewOfIssuanceOfMobilizationOrder(id, parent)
  else
    wnd:Show(true)
  end
end
local function ShowIssuanceOfMobilizationOrder(param)
  CreateIssuanceOfMobilizationOrder("IssuanceOfMobilizationOrder", "UIParent")
  wnd.factionName:SetText(param.factionName)
  wnd.condition:SetText(GetCommonText("issuance_of_mobilization_order_condition", tostring(param.limitLevel), tostring(param.limitLeadershipPoint), param.zoneGroupName))
  wnd.currentStatus:SetText(GetCommonText("issuance_of_mobilization_order_current_status", tostring(param.todayCount), tostring(param.todayCountMax)))
  if param.todayCount < param.todayCountMax then
    wnd.orderBtn:Enable(true)
  else
    wnd.orderBtn:Enable(false)
  end
  function wnd.orderBtn:OnClick()
    X2Faction:RequestIssuanceOfMobilizationOrder(param.doodadId)
    wnd:Show(false)
  end
  wnd.orderBtn:SetHandler("OnClick", wnd.orderBtn.OnClick)
  function wnd.cancelBtn:OnClick()
    wnd:Show(false)
  end
  wnd.cancelBtn:SetHandler("OnClick", wnd.cancelBtn.OnClick)
  local events = {
    INTERACTION_END = function()
      wnd:Show(false)
    end
  }
  wnd:SetHandler("OnEvent", function(this, event, ...)
    events[event](...)
  end)
  RegistUIEvent(wnd, events)
end
UIParent:SetEventHandler("ISSUANCE_OF_MOBILIZATION_ORDER", ShowIssuanceOfMobilizationOrder)
