function CreateRebuyWindow(id, parent)
  local window = SetViewOfRebuyWindow(id, parent)
  local rebuyList = window.rebuyList
  function window:Init()
    local maxItem = STORE_MAX_BACK_COL * STORE_MAX_BACK_ROW
    for i = 1, maxItem do
      local item = rebuyList[i]
      item:Init()
      function item:OnClick(arg)
        AddMessageToSysMsgWindow(locale.store.noBuyList)
      end
      item:SetHandler("OnClick", item.OnClick)
    end
  end
  function window:Update(soldItems)
    if soldItems == nil then
      soldItems = SOLDITEMLIST
    end
    if soldItems ~= nil then
      local soldItemCount = #soldItems
      for i = 1, #rebuyList do
        do
          local itemIdx = soldItemCount - (i - 1)
          local item = soldItems[itemIdx]
          local back = rebuyList[i]
          if item == nil then
            back:Init()
            function back:OnClick(arg)
              AddMessageToSysMsgWindow(locale.store.noBuyList)
            end
            back:SetHandler("OnClick", back.OnClick)
          else
            back:Init()
            back:SetItemInfo(item)
            if item.isStackable == true then
              back:SetStack(tostring(item.stack))
            end
            function back:OnClick(arg)
              X2Store:BuyBackItem(itemIdx)
            end
            back:SetHandler("OnClick", back.OnClick)
          end
        end
      end
    end
  end
  return window
end
