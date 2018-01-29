local alarm_extraValue1 = {CMF_SELF_STATUS_INFO, CMF_SELF_SKILL_INFO}
local alarm_extraValue2 = {
  CMF_RELATION_GROUP,
  subExtraValue = {
    CMF_FRIEND_INFO,
    CMF_BLOCK_INFO,
    CMF_FAMILY_INFO
  }
}
local alarm_extraValue3 = {
  CMF_ADDED_ITEM_GROUP,
  subExtraValue = {CMF_ADDED_ITEM_SELF, CMF_ADDED_ITEM_TEAM}
}
local alarm_extraValue4 = {
  CMF_ACQ_CONSUME_GROUP,
  subExtraValue = {
    CMF_LOOT_METHOD_CHANGED,
    CMF_SELF_MONEY_CHANGED,
    CMF_SELF_HONOR_POINT_CHANGED,
    CMF_SELF_LIVING_POINT_CHANGED,
    CMF_SELF_LEADERSHIP_POINT_CHANGED,
    CMF_SELF_CONTRIBUTION_POINT_CHANGED
  }
}
local alarm_extraValue5 = {
  CMF_PARTY_AND_RAID_INFO,
  CMF_DOMINION_AND_SIEGE_INFO,
  CMF_TRADE_STORE_MSG,
  CMF_EMOTIOIN_EXPRESS
}
local alarm_extraValue6 = {
  CMF_ETC_GROUP,
  subExtraValue = {
    CMF_WEB_CAST_INFO,
    CMF_QUEST_INFO,
    CMF_CHANNEL_INFO
  }
}
function CreateAlarmPageWindow(id, parent, checkBoxs)
  local window = CreateEmptyWindow(id, parent)
  window:Show(true)
  window:AddAnchor("TOPLEFT", parent, 0, 0)
  window:AddAnchor("BOTTOMRIGHT", parent, 0, 0)
  window:SetTitleText(locale.chatFiltering.menuName[2])
  window.titleStyle:SetAlign(ALIGN_TOP_LEFT)
  window.titleStyle:SetFont(FONT_PATH.DEFAULT, FONT_SIZE.LARGE)
  ApplyTitleFontColor(window, FONT_COLOR.TITLE)
  local sideMargin, titleMargin, bottomMargin = GetWindowMargin()
  local info = {}
  info.menuTexts = locale.chatFiltering.alarm_group1
  info.subMenuTexts = nil
  info.extraValue = alarm_extraValue1
  info.subExtraValue = nil
  info.menuChkboxOffset = chatLocale.chatOption.alarmPage.leftMenuChkOffset
  local width = chatLocale.chatOption.alarmPage.colWidth
  window.groupWindow = {}
  window.groupWindow[1] = CreateColorPickCheckGroup(id .. "groupWindow[1]", window, info, checkBoxs)
  window.groupWindow[1]:AddAnchor("TOPLEFT", window, 0, sideMargin)
  window.groupWindow[1]:SetWidth(width)
  info.menuTexts = locale.chatFiltering.alarm_group2
  info.subMenuTexts = locale.chatFiltering.alarm_group2.subMenu
  info.extraValue = alarm_extraValue2
  info.subExtraValue = alarm_extraValue2.subExtraValue
  info.menuChkboxOffset = chatLocale.chatOption.alarmPage.rightMenuChkOffset
  info.submenuChkboxOffset = chatLocale.chatOption.alarmPage.rightSubmenuChkOffset
  window.groupWindow[2] = CreateColorPickCheckGroup(id .. "groupWindow[2]", window, info, checkBoxs)
  window.groupWindow[2]:AddAnchor("TOPLEFT", window.groupWindow[1], "TOPRIGHT", 5, 0)
  window.groupWindow[2]:AddAnchor("TOPRIGHT", window, 0, 0)
  DrawLineDoubleTarget(window.groupWindow[1], window.groupWindow[2])
  info.menuTexts = locale.chatFiltering.alarm_group3
  info.subMenuTexts = locale.chatFiltering.alarm_group3.subMenu
  info.extraValue = alarm_extraValue3
  info.subExtraValue = alarm_extraValue3.subExtraValue
  info.menuChkboxOffset = chatLocale.chatOption.alarmPage.leftMenuChkOffset
  info.submenuChkboxOffset = chatLocale.chatOption.alarmPage.leftSubmenuChkOffset
  window.groupWindow[3] = CreateColorPickCheckGroup(id .. "groupWindow[3]", window, info, checkBoxs)
  window.groupWindow[3]:AddAnchor("TOPLEFT", window.groupWindow[1].line, "BOTTOMLEFT", 0, 5)
  window.groupWindow[3]:SetWidth(width)
  info.menuTexts = locale.chatFiltering.alarm_group4
  info.subMenuTexts = locale.chatFiltering.alarm_group4.subMenu
  info.extraValue = alarm_extraValue4
  info.subExtraValue = alarm_extraValue4.subExtraValue
  info.menuChkboxOffset = chatLocale.chatOption.alarmPage.rightMenuChkOffset
  info.submenuChkboxOffset = chatLocale.chatOption.alarmPage.rightSubmenuChkOffset
  window.groupWindow[4] = CreateColorPickCheckGroup(id .. "groupWindow[4]", window, info, checkBoxs)
  window.groupWindow[4]:AddAnchor("TOPLEFT", window.groupWindow[3], "TOPRIGHT", 5, 0)
  window.groupWindow[4]:AddAnchor("TOPRIGHT", window, 0, 0)
  DrawLineDoubleTarget(window.groupWindow[3], window.groupWindow[4])
  info.menuTexts = locale.chatFiltering.alarm_group5
  info.subMenuTexts = nil
  info.extraValue = alarm_extraValue5
  info.menuChkboxOffset = chatLocale.chatOption.alarmPage.leftMenuChkOffset
  info.subExtraValue = nil
  window.groupWindow[5] = CreateColorPickCheckGroup(id .. "groupWindow[5]", window, info, checkBoxs)
  window.groupWindow[5]:AddAnchor("TOPLEFT", window.groupWindow[3].line, "BOTTOMLEFT", 0, 5)
  window.groupWindow[5]:SetWidth(width)
  info.menuTexts = locale.chatFiltering.alarm_group6
  info.subMenuTexts = locale.chatFiltering.alarm_group6.subMenu
  info.extraValue = alarm_extraValue6
  info.menuChkboxOffset = chatLocale.chatOption.alarmPage.rightMenuChkOffset
  info.subExtraValue = alarm_extraValue6.subExtraValue
  window.groupWindow[6] = CreateColorPickCheckGroup(id .. "groupWindow[6]", window, info, checkBoxs)
  window.groupWindow[6]:AddAnchor("TOPLEFT", window.groupWindow[5], "TOPRIGHT", 5, 0)
  window.groupWindow[6]:AddAnchor("TOPRIGHT", window, 0, 0)
  return window
end
