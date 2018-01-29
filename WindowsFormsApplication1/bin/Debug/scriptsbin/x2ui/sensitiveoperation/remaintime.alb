function CreateSensitiveOpeartion(id)
  local wnd = SetViewOfSensitiveOpeartion(id)
  local sensitiveOperationTime = X2Player:GetSensitiveOperationTime()
  local prevSensitiveOperationTime = X2Player:GetSensitiveOperationTime()
  local viewToolTip = false
  if sensitiveOperationTime > 0 then
    wnd:Show(true)
  end
  function SetSensitiveOperationTime(maxTime)
    sensitiveOperationTime = maxTime
    if sensitiveOperationTime > 0 then
      wnd:Show(true)
    end
  end
  function SetPrevSensitiveOperationTime(preTime)
    prevSensitiveOperationTime = preTime
  end
  function wnd:OnUpdate(dt)
    sensitiveOperationTime = sensitiveOperationTime - dt
    X2Player:SetSensitiveOperationTime(sensitiveOperationTime)
    if sensitiveOperationTime < 0 then
      SetSensitiveOperationTime(0)
      HideTooltip()
      wnd:Show(false)
      X2Player:SetSensitiveOperationTime(0)
    end
    if viewToolTip and prevSensitiveOperationTime - sensitiveOperationTime >= 1000 then
      SetPrevSensitiveOperationTime(sensitiveOperationTime)
      SetTooltip(locale.sensitiveOperation.remain_time(string.format("%d", sensitiveOperationTime / 1000)), wnd)
    end
  end
  wnd:SetHandler("OnUpdate", wnd.OnUpdate)
  function wnd:OnEnter()
    if sensitiveOperationTime > 0 then
      viewToolTip = true
    end
  end
  wnd:SetHandler("OnEnter", wnd.OnEnter)
  function wnd:OnLeave()
    viewToolTip = false
    HideTooltip()
  end
  wnd:SetHandler("OnLeave", wnd.OnLeave)
  function wnd:OnScale()
    self:RemoveAllAnchors()
    self:AddAnchor("TOPRIGHT", "UIParent", SENSITIVE_OPERATION_OFFSET_X, SENSITIVE_OPERATION_OFFSET_Y)
  end
  wnd:SetHandler("OnScale", wnd.OnScale)
  return wnd
end
local featureSet = X2Player:GetFeatureSet()
if featureSet.sensitiveOpeartion then
  sensitiveOperationFrame = CreateSensitiveOpeartion("sensitiveOperationTime")
  do
    local sensitiveOperationFrameEvents = {
      START_SENSITIVE_OPERATION = function(remainTime)
        SetSensitiveOperationTime(remainTime)
        SetPrevSensitiveOperationTime(remainTime + 1)
      end
    }
    sensitiveOperationFrame:SetHandler("OnEvent", function(this, event, ...)
      sensitiveOperationFrameEvents[event](...)
    end)
    RegistUIEvent(sensitiveOperationFrame, sensitiveOperationFrameEvents)
  end
end
