local pages = {
  {
    windowTitle = "1. Total Aggro",
    tableName = "total"
  },
  {
    windowTitle = "2. Damage Aggro ",
    tableName = "damage"
  },
  {
    windowTitle = "3. Heal Aggro",
    tableName = "heal"
  },
  {
    windowTitle = "4. Etc Aggro",
    tableName = "etc"
  }
}
local function Update()
  local cur = pages[1]
  local param = X2:GetAggroTable(cur.tableName)
  aggroMeterWnd:SetTitle(cur.windowTitle)
  local base = 1
  if param[1] then
    base = math.max(1, tonumber(param[1].aggro))
  end
  for i = 1, #aggroMeterWnd.child do
    local val = param[i]
    local text = ""
    if val then
      text = tostring(i) .. ". " .. val.name .. "  " .. val.aggro .. " (" .. tostring(math.floor(val.aggro * 100 / base)) .. "%)"
    end
    aggroMeterWnd.child[i]:SetText(text)
  end
  aggroMeterWnd:Show(true)
end
local aggroMeterEvents = {
  AGGRO_METER_UPDATED = function()
    Update()
  end,
  AGGRO_METER_CLEARED = function()
    aggroMeterWnd:Show(false)
  end
}
aggroMeterWnd:SetHandler("OnEvent", function(this, event, ...)
  aggroMeterEvents[event](...)
end)
aggroMeterWnd:RegisterEvent("AGGRO_METER_UPDATED")
aggroMeterWnd:RegisterEvent("AGGRO_METER_CLEARED")
aggroMeterWnd.nextButton:SetHandler("OnClick", function()
  table.insert(pages, table.remove(pages, 1))
  Update()
end)
aggroMeterWnd.prevButton:SetHandler("OnClick", function()
  table.insert(pages, 1, table.remove(pages))
  Update()
end)
