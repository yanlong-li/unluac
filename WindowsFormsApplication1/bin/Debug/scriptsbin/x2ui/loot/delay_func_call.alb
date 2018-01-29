local timerWidget = UIParent:CreateWidget("emptywidget", "timer", "UIParent")
timerWidget:SetExtent(0, 0)
timerWidget:Show(true)
local delayCallFuncTable = {}
function DelayCall(time, func)
  local funcTable = {}
  funcTable.baseTime = X2Time:GetUiMsec()
  funcTable.time = time
  funcTable.func = func
  table.insert(delayCallFuncTable, funcTable)
  function timerWidget:OnUpdate()
    local count = 0
    for key, funcInfo in pairs(delayCallFuncTable) do
      local diff = X2Time:GetUiMsec() - funcInfo.baseTime
      if diff >= funcInfo.time then
        funcInfo.func()
        delayCallFuncTable[key] = nil
      end
      count = count + 1
    end
    if count == 0 then
      self:ReleaseHandler("OnUpdate")
    end
  end
  timerWidget:SetHandler("OnUpdate", timerWidget.OnUpdate)
end
