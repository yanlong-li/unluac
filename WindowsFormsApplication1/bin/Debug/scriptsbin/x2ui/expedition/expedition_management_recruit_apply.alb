local applyWnd
function ShowApply(parent, subItem)
  if applyWnd == nil then
    applyWnd = SetViewOfApply("apply", parent)
    function applyWnd.registerButton:OnClick()
      X2Faction:RequestExpeditionApplicantAdd(applyWnd.expeditionId, applyWnd.memo:GetText())
      applyWnd:Show(false)
    end
    applyWnd.registerButton:SetHandler("OnClick", applyWnd.registerButton.OnClick)
    function applyWnd.cancelButton:OnClick()
      applyWnd:Show(false)
    end
    applyWnd.cancelButton:SetHandler("OnClick", applyWnd.cancelButton.OnClick)
    function applyWnd.memo:OnTextChanged()
      applyWnd.memoTextLenth:SetText(string.format("%d/%d", self:GetTextLength(), self:MaxTextLength()))
    end
    applyWnd.memo:SetHandler("OnTextChanged", applyWnd.memo.OnTextChanged)
  end
  local str = string.format([[
%s%s|r
%s]], FONT_COLOR_HEX.GREEN, subItem.expeditionName:GetText(), GetUIText(COMMON_TEXT, "expedition_do_you_apply"))
  applyWnd.content:SetText(str)
  applyWnd.memo:SetText("")
  applyWnd:SetResizeHeight()
  applyWnd.expeditionId = subItem.expeditionId
  applyWnd.expeditionName = subItem.expeditionName:GetText()
  applyWnd:Show(true)
  applyWnd:Raise()
  return applyWnd
end
