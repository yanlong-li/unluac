demo = {}
demo.wnd = CreateEmptyWindow("demo.wnd", "UIParent")
demo.wnd:Show(false)
demo.wnd:SetExtent(250, 800)
demo.wnd:AddAnchor("TOPRIGHT", "UIParent", 100, 100)
demo.btns = {}
demo.cntResetBtn = 30
demo.startBtn = CreateEmptyButton("demo.startBtn", demo.wnd)
demo.startBtn:Show(false)
demo.startBtn:AddAnchor("TOPLEFT", demo.wnd, 100, 0)
demo.startBtn:SetText("demo")
ApplyButtonSkin(demo.startBtn, BUTTON_BASIC.DEFAULT)
for k = 1, demo.cntResetBtn do
  do
    local btn = demo.wnd:CreateChildWidget("button", "btns", k, true)
    btn:SetExtent(80, 30)
    btn:AddAnchor("TOPLEFT", demo.wnd, "TOPLEFT", 10, (k - 1) * 30 + 30)
    btn:SetText("Set (" .. k .. ")")
    btn:Show(false)
    btn.idx = k
    function btn:OnClick()
      X2Demo:ResetCharacter(btn.idx)
      HideDemoResetBtns()
      UIParent:LogAlways("demo reset clicked")
    end
    btn:SetHandler("OnClick", btn.OnClick)
    demo.btns[k] = btn
  end
end
demo.isShowingResetBtns = false
function ShowDemoResetBtns()
  demo.isShowingResetBtns = true
  for k = 1, demo.cntResetBtn do
    demo.btns[k]:Show(true)
  end
end
function HideDemoResetBtns()
  demo.isShowingResetBtns = false
  for k = 1, demo.cntResetBtn do
    demo.btns[k]:Show(false)
  end
end
function demo.startBtn:OnClick()
  demo.cntResetBtn = X2Demo:GetDemoOptionCount()
  UIParent:LogAlways("start button clicked: " .. demo.cntResetBtn)
  if demo.isShowingResetBtns == true then
    HideDemoResetBtns()
    UIParent:LogAlways("start button clicked: hide")
  else
    ShowDemoResetBtns()
    UIParent:LogAlways("start button clicked: show")
  end
end
demo.startBtn:SetHandler("OnClick", demo.startBtn.OnClick)
local demoEvents = {
  DEMO_MODE = function(showing)
    if showing == true then
      demo.wnd:Show(true)
      demo.startBtn:Show(true)
      HideDemoResetBtns()
    end
  end,
  DEMO_CHAR_RESET = function()
    demo.wnd:Show(true)
    HideDemoResetBtns()
  end
}
demo.wnd:SetHandler("OnEvent", function(this, event, ...)
  demoEvents[event](...)
end)
demo.wnd:RegisterEvent("DEMO_MODE")
demo.wnd:RegisterEvent("DEMO_CHAR_RESET")
