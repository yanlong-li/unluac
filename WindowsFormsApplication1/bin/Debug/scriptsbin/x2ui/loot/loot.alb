LOOT_ALL_SHOW_TIME = 2000
lootWindow = nil
local OnLootBagClose = function()
  if lootWindow ~= nil then
    if lootWindow:HasHandler("OnUpdate") then
      return
    end
    lootWindow:Show(false)
  end
end
local lootItemEvent = {LOOT_BAG_CLOSE = OnLootBagClose}
local FillLootList = function()
  if lootWindow ~= nil then
    lootWindow.scrollListCtrl:DeleteAllDatas()
    local numItem = X2Loot:GetNumLootItems()
    for k = 1, numItem do
      local itemInfo = X2Loot:GetLootingBagItemInfo(k)
      lootWindow.scrollListCtrl:InsertData(k, 1, itemInfo)
    end
  end
end
local function CreateLootWindow(id, parent)
  local window = SetViewOfLootWindow(id, parent)
  window:Show(true)
  window:SetHandler("OnEvent", function(this, event, ...)
    lootItemEvent[event](...)
  end)
  window:RegisterEvent("LOOT_BAG_CLOSE")
  local LootAllItem = function()
    for k = 1, X2Loot:GetNumLootItems() do
      X2Loot:LootItem(k)
    end
  end
  local allLootBtn = window.allLootBtn
  local function OnClickAllLootBtn(self, arg)
    if arg == "LeftButton" then
      LootAllItem()
    end
  end
  allLootBtn:SetHandler("OnClick", OnClickAllLootBtn)
  local function OnHideWindow()
    HideTooltip()
    window:ReleaseHandler("OnUpdate")
    X2Loot:CloseLoot()
    lootWindow = nil
  end
  window:SetHandler("OnHide", OnHideWindow)
  function window:OnUpdateWindow(dt)
    window.showingTime = window.showingTime + dt
    if LOOT_ALL_SHOW_TIME < window.showingTime then
      window:ReleaseHandler("OnUpdate")
      window:Show(false)
    end
  end
  return window
end
local function OnLootBagChanged(setTime)
  if lootWindow == nil then
    lootWindow = CreateLootWindow("lootWindow", "UIParent")
    lootWindow.allLootBtn:Show(not setTime)
    FillLootList()
    if setTime then
      lootWindow.showingTime = 0
      lootWindow:SetHandler("OnUpdate", lootWindow.OnUpdateWindow)
    end
  else
    lootWindow:Show(true)
    FillLootList()
  end
end
UIParent:SetEventHandler("LOOT_BAG_CHANGED", OnLootBagChanged)
