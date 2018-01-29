function CreateGemEnchantWindow(tabWindow)
  SetViewOfGemEnchantWindow(tabWindow)
  local info = {
    leftButtonStr = WINDOW_ENCHANT.GEM_ENCHANT_TAB.BOTTOM_BUTTON_STR.LEFT,
    rightButtonStr = WINDOW_ENCHANT.GEM_ENCHANT_TAB.BOTTOM_BUTTON_STR.RIGHT,
    leftButtonLeftClickFunc = function()
      LockUnvisibleTab()
      X2ItemEnchant:Execute()
    end,
    rightButtonLeftClickFunc = function()
      if X2ItemEnchant:IsWorkingEnchant() then
        X2ItemEnchant:StopEnchanting()
      else
        X2ItemEnchant:LeaveItemEnchantMode()
      end
    end
  }
  CreateWindowDefaultTextButtonSet(tabWindow, info)
  tabWindow.leftButton:Enable(false)
  function tabWindow.slotTargetItem:Update()
    for i = 1, #tabWindow.enchantListFrame.item do
      tabWindow.enchantListFrame.item[i]:Show(false)
    end
    local itemInfo = X2ItemEnchant:GetTargetItemInfo()
    UpdateSlot(self, itemInfo, true)
    if itemInfo ~= nil then
      local item = tabWindow.enchantListFrame.item[1]
      item:Show(false)
      local str = ""
      local warningText = WINDOW_ENCHANT.GEM_ENCHANT_TAB.WARNING_TEXT.TEXT.UNEQUIPED
      if itemInfo.gemModifireTable ~= nil then
        local maxCount = math.min(#tabWindow.enchantListFrame.item, #itemInfo.gemModifireTable)
        for i = 1, maxCount do
          local item = tabWindow.enchantListFrame.item[i]
          item:Show(true)
          for index = 1, #itemInfo.gemModifireTable do
            local modifier = itemInfo.gemModifireTable[index]
            if str ~= "" then
              str = string.format([[
%s
%s]], str, GetAddModifierText(modifier))
            else
              str = GetAddModifierText(modifier)
            end
          end
        end
        warningText = WINDOW_ENCHANT.GEM_ENCHANT_TAB.WARNING_TEXT.TEXT.EQUIPED
      end
      if itemInfo.gemInfo ~= nil and itemInfo.gemInfo ~= 0 then
        local gemItemInfo = X2Item:GetItemInfoByType(itemInfo.gemInfo, 0, IIK_SOCKET_MODIFIER)
        if gemItemInfo.skill_modifier_tooltip ~= nil and gemItemInfo.skill_modifier_tooltip ~= "" then
          if str ~= "" then
            str = string.format([[
%s
%s%s]], str, FONT_COLOR_HEX.GREEN, gemItemInfo.skill_modifier_tooltip)
          else
            str = string.format("%s%s", FONT_COLOR_HEX.GREEN, gemItemInfo.skill_modifier_tooltip)
          end
        end
        if gemItemInfo.buff_modifier_tooltip ~= nil and gemItemInfo.buff_modifier_tooltip ~= "" then
          if str ~= "" then
            str = string.format([[
%s
%s%s]], str, FONT_COLOR_HEX.GREEN, gemItemInfo.buff_modifier_tooltip)
          else
            str = string.format("%s%s", FONT_COLOR_HEX.GREEN, gemItemInfo.buff_modifier_tooltip)
          end
        end
      end
      if itemInfo.gem_procs ~= nil then
        local maxCount = math.min(#tabWindow.enchantListFrame.item, #itemInfo.gem_procs)
        for i = 0, maxCount do
          local item = tabWindow.enchantListFrame.item[i + 1]
          item:Show(true)
          for index = 0, #itemInfo.gem_procs do
            local gemProc = itemInfo.gem_procs[index]
            if str ~= "" then
              str = string.format([[
%s
%s%s]], str, FONT_COLOR_HEX.DEFAULT, FormatDurationTimeInDesc(gemProc.desc))
            else
              str = string.format("%s%s|r", FONT_COLOR_HEX.DEFAULT, FormatDurationTimeInDesc(gemProc.desc))
            end
          end
        end
        warningText = WINDOW_ENCHANT.GEM_ENCHANT_TAB.WARNING_TEXT.TEXT.EQUIPED
      end
      item:SetWidth(360)
      item:SetText(str)
      item:SetExtent(item:GetLongestLineWidth() + 5, item:GetTextHeight())
      tabWindow:SetWarningText(WINDOW_ENCHANT.GEM_ENCHANT_TAB.WARNING_TEXT.TEXT.EQUIPED)
      tabWindow:SetWarningText(warningText)
      return true
    else
      return false
    end
  end
  SetTargetItemClickFunc(tabWindow.slotTargetItem)
  SetEnchantItemClickFunc(tabWindow.slotEnchantItem)
  function tabWindow.slotEnchantItem:Update()
    local itemInfo = X2ItemEnchant:GetEnchantItemInfo()
    UpdateSlot(self, itemInfo)
    if itemInfo ~= nil then
      local exist = false
      local str = ""
      if itemInfo.modifier ~= nil and #itemInfo.modifier ~= 0 then
        for index = 1, #itemInfo.modifier do
          local modifier = itemInfo.modifier[index]
          if str ~= "" then
            str = string.format([[
%s
%s]], str, GetAddModifierText(modifier))
          else
            str = GetAddModifierText(modifier)
          end
        end
        exist = true
      end
      if itemInfo.skill_modifier_tooltip ~= nil and itemInfo.skill_modifier_tooltip ~= "" then
        if str ~= "" then
          str = string.format([[
%s
%s%s]], str, FONT_COLOR_HEX.GREEN, itemInfo.skill_modifier_tooltip)
        else
          str = string.format("%s%s", FONT_COLOR_HEX.GREEN, itemInfo.skill_modifier_tooltip)
        end
      end
      if itemInfo.buff_modifier_tooltip ~= nil and itemInfo.buff_modifier_tooltip ~= "" then
        if str ~= "" then
          str = string.format([[
%s
%s%s]], str, FONT_COLOR_HEX.GREEN, itemInfo.buff_modifier_tooltip)
        else
          str = string.format("%s%s", FONT_COLOR_HEX.GREEN, itemInfo.buff_modifier_tooltip)
        end
      end
      if itemInfo.procs ~= nil then
        for index = 0, #itemInfo.procs do
          local procs = itemInfo.procs[index]
          if str ~= "" then
            str = string.format([[
%s
%s%s|r]], str, FONT_COLOR_HEX.DEFAULT, FormatDurationTimeInDesc(procs.desc))
          else
            str = string.format("%s%s|r", FONT_COLOR_HEX.DEFAULT, FormatDurationTimeInDesc(procs.desc))
          end
        end
        exist = true
      end
      tabWindow.curEnchantInfoFrame.info:Show(true)
      tabWindow.curEnchantInfoFrame.info:SetInfo(itemInfo.itemType, str)
      local height = tabWindow.curEnchantInfoFrame.info.itemName:GetHeight()
      local curFrameHeight = FORM_ENCHANT_WINDOW.UPPER_FRAME_HEIGHT
      local listHeight = FORM_ENCHANT_WINDOW.BOTTOM_FRAME_HEIGHT
      if height > curFrameHeight then
        tabWindow.curEnchantInfoFrame.info:SetHeight(height)
        tabWindow.curEnchantInfoFrame:SetHeight(height + 20)
        local gap = height - curFrameHeight
        tabWindow.enchantListFrame:SetHeight(listHeight - gap - 20)
      else
        tabWindow.curEnchantInfoFrame.info:SetHeight(curFrameHeight)
        tabWindow.curEnchantInfoFrame:SetHeight(curFrameHeight)
        tabWindow.enchantListFrame:SetHeight(listHeight)
      end
      self.procOnEnter = nil
      return exist
    else
      tabWindow.curEnchantInfoFrame.info:Show(false)
      tabWindow.curEnchantInfoFrame.info:SetInfo(nil, "")
      function self:procOnEnter()
        if self.tooltip == nil then
          return
        end
        SetTooltip(self.tooltip, self)
      end
      return false
    end
  end
  function tabWindow:SlotAllUpdate(isExcutable, isLock)
    local targetInfoExist = self.slotTargetItem:Update()
    local enchatInfoExist = self.slotEnchantItem:Update()
    self.leftButton:Enable(isExcutable)
    self.slotTargetItem:Enable(not isLock)
    self.slotEnchantItem:Enable(not isLock)
    self.warningText:Show(targetInfoExist and enchatInfoExist)
    if isLock then
      self.slotTargetItem:SetOverlayColor(ICON_BUTTON_OVERLAY_COLOR.BLACK)
      self.slotEnchantItem:SetOverlayColor(ICON_BUTTON_OVERLAY_COLOR.BLACK)
    else
      self.slotTargetItem:SetOverlay(self.slotTargetItem:GetInfo())
      self.slotEnchantItem:SetOverlay(self.slotEnchantItem:GetInfo())
    end
  end
end
