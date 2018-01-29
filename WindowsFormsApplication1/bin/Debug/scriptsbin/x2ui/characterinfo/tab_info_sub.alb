function CreateCharacterTabInfoSubWindow(id, parent, tabIndex)
  local window = CreateEmptyWindow(id, parent)
  local sideMargin, titleMargin, bottomMargin = GetWindowMargin()
  local max_width = characetInfoLocale.windowWidth - sideMargin * 2
  local item_height = 16
  local item_offset = 20
  window.popupItemWindow = {}
  window.popupSubtitle = {}
  window.popupValue = {}
  window.group = {}
  for i = 1, #locale.character.popupSubtitle[tabIndex] do
    local group = window:CreateChildWidget("window", "group", i, true)
    group:Show(true)
    group:SetWidth(max_width)
    if tabIndex == INDEX_REGEN then
      local title = X2Locale:LocalizeUiText(CHARACTER_SUBTITLE_TEXT, locale.character.subtitle[i])
      group:SetTitleText(title)
      group.titleStyle:SetFont(FONT_PATH.DEFAULT, FONT_SIZE.XLARGE)
      group.titleStyle:SetAlign(ALIGN_TOP_LEFT)
      ApplyTitleFontColor(group, FONT_COLOR.MIDDLE_TITLE)
    end
    if i == 1 then
      group:AddAnchor("TOPLEFT", window, 0, 10)
    else
      group:AddAnchor("TOPLEFT", window.group[i - 1], "BOTTOMLEFT", 0, sideMargin)
    end
    if i ~= #locale.character.popupSubtitle[tabIndex] then
      local line = CreateLine(group, "TYPE1")
      line:SetColor(1, 1, 1, 0.5)
      line:AddAnchor("TOPLEFT", group, "BOTTOMLEFT", 0, sideMargin / 2)
      line:AddAnchor("TOPRIGHT", group, "BOTTOMRIGHT", 0, sideMargin / 2)
    end
    window.group[i] = group
  end
  local itemIndex = 1
  for i = 1, #locale.character.popupSubtitle[tabIndex] do
    for k = 1, #locale.character.popupSubtitle[tabIndex][i] do
      local subTitleInfo = locale.character.popupSubtitle[tabIndex][i][k]
      local itemWindow = window:CreateChildWidget("emptywidget", "popupItemWindow", itemIndex, true)
      itemWindow:Show(true)
      itemWindow:SetExtent(max_width, item_height)
      local label = window:CreateChildWidget("label", "popupSubtitle", itemIndex, true)
      label:SetExtent(max_width / 2, item_height)
      label:AddAnchor("LEFT", itemWindow, 0, 0)
      local strTitle
      if subTitleInfo.category then
        strTitle = X2Locale:LocalizeUiText(subTitleInfo.category, subTitleInfo.key)
      else
        strTitle = locale.attribute(subTitleInfo.key)
      end
      label:SetText(strTitle)
      label.tooltipText = GetCharInfoTooltip(subTitleInfo.key)
      ApplyTextColor(label, FONT_COLOR.DEFAULT)
      label.style:SetAlign(ALIGN_LEFT)
      local widgetType = "label"
      local width = max_width / 4
      local offset = 0
      if subTitleInfo.widgetType then
        widgetType = subTitleInfo.widgetType
        width = max_width / 3 - 10
        offset = max_width / 4 - width
      end
      local value = window:CreateChildWidget(widgetType, "popupValue", itemIndex, true)
      value:SetExtent(width, item_height)
      value:AddAnchor("RIGHT", itemWindow, -offset, 0)
      ApplyTextColor(value, FONT_COLOR.BLUE)
      value.style:SetAlign(ALIGN_LEFT)
      itemIndex = itemIndex + 1
    end
  end
  SetTooltipHandlers(window.popupSubtitle)
  local popup_offset = 0
  local height_offset = 0
  if tabIndex == INDEX_REGEN then
    popup_offset = 25
    height_offset = sideMargin
  end
  local group_height = {}
  local lastNum = 1
  for m = 1, #locale.character.popupSubtitle[tabIndex] do
    local info = locale.character.popupSubtitle[tabIndex][m]
    local startIdx = lastNum
    local endIdx = lastNum + #info - 1
    lastNum = endIdx + 1
    local height = 0
    for i = startIdx, endIdx do
      window.popupItemWindow[i]:AddAnchor("TOPLEFT", window.group[m], 0, (i - startIdx) * item_offset + popup_offset)
      height = height + item_offset
    end
    group_height[m] = height + height_offset
  end
  local windowHeight = sideMargin * 7 + titleMargin
  for i = 1, #group_height do
    windowHeight = windowHeight + group_height[i]
    window.group[i]:SetHeight(group_height[i])
  end
  window:SetHeight(windowHeight)
  return window, windowHeight
end
