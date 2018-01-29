SENSITIVE_OPERATION_OFFSET_X = -265
SENSITIVE_OPERATION_OFFSET_Y = 3
function SetViewOfSensitiveOpeartion(id)
  local emptyWindow = UIParent:CreateWidget("emptywidget", id, "UIParent")
  emptyWindow:SetExtent(20, 20)
  emptyWindow:AddAnchor("TOPRIGHT", "UIParent", SENSITIVE_OPERATION_OFFSET_X, SENSITIVE_OPERATION_OFFSET_Y)
  local sensitiveOperationTimeButton = emptyWindow:CreateImageDrawable(TEXTURE_PATH.HUD, "background")
  sensitiveOperationTimeButton:SetTextureInfo("shield")
  sensitiveOperationTimeButton:AddAnchor("CENTER", emptyWindow, 0, 0)
  return emptyWindow
end
