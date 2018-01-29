local combat_extraValue1 = {CMF_COMBAT_SRC_GROUP}
local combat_extraValue2 = {CMF_COMBAT_DST_GROUP}
local combat_extraValue3 = {
  CMF_COMBAT_MELEE_GROUP,
  subExtraValue = {CMF_COMBAT_MELEE_DAMAGE, CMF_COMBAT_MELEE_MISSED}
}
local combat_extraValue4 = {
  CMF_COMBAT_SPELL_GROUP,
  subExtraValue = {
    CMF_COMBAT_SPELL_DAMAGE,
    CMF_COMBAT_SPELL_MISSED,
    CMF_COMBAT_SPELL_HEALED,
    CMF_COMBAT_SPELL_ENERGIZE
  }
}
local combat_extraValue5 = {
  CMF_COMBAT_SPELL_AURA,
  CMF_COMBAT_ENVIRONMENTAL_DMANAGE,
  CMF_COMBAT_DEAD
}
function CreateCombatPageWindow(id, parent, checkBoxs)
  local window = CreateEmptyWindow(id, parent)
  window:Show(true)
  window:AddAnchor("TOPLEFT", parent, 0, 0)
  window:AddAnchor("BOTTOMRIGHT", parent, 0, 0)
  window:SetTitleText(locale.chatFiltering.menuName[3])
  window.titleStyle:SetAlign(ALIGN_TOP_LEFT)
  window.titleStyle:SetFont(FONT_PATH.DEFAULT, FONT_SIZE.LARGE)
  ApplyTitleFontColor(window, FONT_COLOR.TITLE)
  local sideMargin, titleMargin, bottomMargin = GetWindowMargin()
  local width = chatLocale.chatOption.combatPage.colWidth
  local info = {}
  info.menuTexts = locale.chatFiltering.combat_group1
  info.extraValue = combat_extraValue1
  info.menuChkboxOffset = chatLocale.chatOption.combatPage.menuChkOffset
  window.groupWindow = {}
  window.groupWindow[1] = CreateColorPickCheckGroup(id .. "groupWindow[1]", window, info, checkBoxs)
  window.groupWindow[1]:SetWidth(width)
  window.groupWindow[1]:AddAnchor("TOPLEFT", window, 0, sideMargin)
  info.menuTexts = locale.chatFiltering.combat_group2
  info.extraValue = combat_extraValue2
  window.groupWindow[2] = CreateColorPickCheckGroup(id .. "groupWindow[2]", window, info, checkBoxs)
  window.groupWindow[2]:AddAnchor("TOPLEFT", window.groupWindow[1], "TOPRIGHT", 5, 0)
  window.groupWindow[2]:AddAnchor("TOPRIGHT", window, 0, 0)
  checkBoxs.boxMapUseExtraValue[CMF_COMBAT_SRC_GROUP].colorPick:Show(false)
  checkBoxs.boxMapUseExtraValue[CMF_COMBAT_DST_GROUP].colorPick:Show(false)
  info.menuTexts = locale.chatFiltering.combat_group3
  info.subMenuTexts = locale.chatFiltering.combat_group3.subMenu
  info.extraValue = combat_extraValue3
  info.subExtraValue = combat_extraValue3.subExtraValue
  info.submenuChkboxOffset = chatLocale.chatOption.combatPage.subMenuChkOffset
  DrawLineDoubleTarget(window.groupWindow[1], window.groupWindow[2])
  window.groupWindow[3] = CreateColorPickCheckGroup(id .. "groupWindow[3]", window, info, checkBoxs)
  window.groupWindow[3]:SetWidth(width)
  window.groupWindow[3]:AddAnchor("TOPLEFT", window.groupWindow[1], "BOTTOMLEFT", 0, 10)
  info.menuTexts = locale.chatFiltering.combat_group4
  info.subMenuTexts = locale.chatFiltering.combat_group4.subMenu
  info.extraValue = combat_extraValue4
  info.subExtraValue = combat_extraValue4.subExtraValue
  info.submenuChkboxOffset = chatLocale.chatOption.combatPage.subMenuChkOffset
  window.groupWindow[4] = CreateColorPickCheckGroup(id .. "groupWindow[4]", window, info, checkBoxs)
  window.groupWindow[4]:AddAnchor("TOPLEFT", window.groupWindow[3], "TOPRIGHT", 5, 0)
  window.groupWindow[4]:AddAnchor("TOPRIGHT", window, 0, 0)
  DrawLineDoubleTarget(window.groupWindow[3], window.groupWindow[4])
  info.menuTexts = locale.chatFiltering.combat_group5
  info.subMenuTexts = locale.chatFiltering.combat_group5.subMenu
  info.extraValue = combat_extraValue5
  window.groupWindow[5] = CreateColorPickCheckGroup(id .. "groupWindow[5]", window, info, checkBoxs)
  window.groupWindow[5]:AddAnchor("TOPLEFT", window.groupWindow[3].line, 0, 5)
  window.groupWindow[5]:SetWidth(width)
  return window
end
