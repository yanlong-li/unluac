aggroMeterWnd = CreateWindow("aggroMeterWnd", "UIParent")
aggroMeterWnd:AddAnchor("RIGHT", "UIParent", 0, 0)
aggroMeterWnd:SetExtent(280, 280)
aggroMeterWnd:SetTitle("aggro meter")
aggroMeterWnd:SetCloseOnEscape(false)
aggroMeterWnd.titleBar.closeButton:Show(false)
aggroMeterWnd.child = {}
local offsetX = 40
local offsetY = 70
local labelHeight = 18
for k = 1, 10 do
  local id = "label" .. tostring(k)
  aggroMeterWnd.child[k] = UIParent:CreateWidget("label", id, aggroMeterWnd)
  local child = aggroMeterWnd.child[k]
  child:AddAnchor("TOPLEFT", offsetX, offsetY)
  child:SetExtent(200, labelHeight)
  child:SetText(id)
  child.style:SetColor(0.3, 0.2, 0, 1)
  child.style:SetAlign(ALIGN_LEFT)
  offsetY = offsetY + labelHeight
end
local nextButton = aggroMeterWnd:CreateChildWidget("button", "nextButton", 0, true)
nextButton:AddAnchor("TOPRIGHT", aggroMeterWnd, "TOPRIGHT", -30, 40)
nextButton:SetText("\226\150\183")
nextButton:Show(true)
ApplyButtonSkin(nextButton, BUTTON_BASIC.DEFAULT)
local prevButton = aggroMeterWnd:CreateChildWidget("button", "prevButton", 0, true)
prevButton:SetExtent(55, 26)
prevButton:AddAnchor("TOPLEFT", aggroMeterWnd, "TOPLEFT", 30, 40)
prevButton:SetText("\226\151\129")
prevButton:Show(true)
ApplyButtonSkin(prevButton, BUTTON_BASIC.DEFAULT)
aggroMeterWnd:Show(false)
