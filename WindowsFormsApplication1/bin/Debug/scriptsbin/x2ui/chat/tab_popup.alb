local chatContextPopupProc = {}
chatContextPopupProc.tabId = nil
function chatContextPopupProc:popupProc_LockTab()
  if chatContextPopupProc.tabId == nil then
    return
  end
  X2Chat:LockChatWindowByTabId(chatContextPopupProc.tabId, true)
end
function chatContextPopupProc:popupProc_UnlockTab()
  if chatContextPopupProc.tabId == nil then
    return
  end
  X2Chat:LockChatWindowByTabId(chatContextPopupProc.tabId, false)
end
function chatContextPopupProc:popupProc_ChangeTabName()
  if chatContextPopupProc.tabId == nil then
    return
  end
  ShowChangeChatTabName(chatContextPopupProc.tabId)
end
function chatContextPopupProc:popupProc_ClearTabContent()
  if chatContextPopupProc.tabId == nil then
    return
  end
  X2Chat:ClearChatContentByUser(chatContextPopupProc.tabId)
end
function chatContextPopupProc:popupProc_TabSetting()
  if chatContextPopupProc.tabId == nil then
    return
  end
  ShowChatOptionWindow(chatContextPopupProc.tabId)
end
function chatContextPopupProc:popupProc_RemoveTab()
  if chatContextPopupProc.tabId == nil then
    return
  end
  X2Chat:DeleteChatTabByUser(chatContextPopupProc.tabId)
end
function PopupChatTabContext(stickTo, targetWidget, chatTabId, hideProc)
  if chatTabId == nil then
    return
  end
  local popupInfo = GetDefaultPopupInfoTable()
  popupInfo.hideProcedure = hideProc
  chatContextPopupProc.tabId = chatTabId
  if X2Chat:IsLockedChatWindowByChatTabId(chatTabId) then
    popupInfo:AddInfo(locale.chat.unlockTab, chatContextPopupProc.popupProc_UnlockTab)
  else
    popupInfo:AddInfo(locale.chat.lockTab, chatContextPopupProc.popupProc_LockTab)
  end
  if chatTabId ~= 0 then
    popupInfo:AddInfo(locale.chat.tabnNameChange, chatContextPopupProc.popupProc_ChangeTabName)
  end
  popupInfo:AddInfo(locale.chat.eraseTabContent, chatContextPopupProc.popupProc_ClearTabContent)
  popupInfo:AddInfo(locale.chatFiltering.title, chatContextPopupProc.popupProc_TabSetting)
  if chatTabId == 3 then
    popupInfo:AddInfo(locale.chat.removeTab, chatContextPopupProc.popupProc_RemoveTab)
  elseif chatTabId ~= 0 then
    popupInfo:AddInfo(locale.chat.removeTab, chatContextPopupProc.popupProc_RemoveTab)
  end
  ShowPopUpMenu("popupMenu", targetWidget, popupInfo, nil, "TOPLEFT", "TOPRIGHT", -13, 13)
end
