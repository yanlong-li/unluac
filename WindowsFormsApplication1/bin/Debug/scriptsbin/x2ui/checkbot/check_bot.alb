checkBotWnd = nil
function CreateBotCheckWindow(id, parent)
  local w = SetViewOfCheckBotWindow(id, parent)
  w.btnDelay = 0
  function w:UpdateInfos(remainTime, count, question)
    if remainTime == 0 and count == 0 and question == 0 then
      w:Show(false)
      return
    end
    w.time = remainTime
    w.answerCount:SetText(GetCommonText("check_bot_answer_cnt", 3 - count))
    if w.oldQuestion ~= question then
      local values = {
        math.floor(question / 100),
        math.floor(question % 100 / 10),
        math.floor(question % 10)
      }
      for i = 1, 3 do
        local imgKey = string.format("security_%d", values[i])
        w.questionNumImg[i]:SetTextureInfo(imgKey)
      end
      local scratchImgKey = string.format("pattern_%02d", math.random(1, 3))
      w.questionScratchImg:SetTextureInfo(scratchImgKey)
      w.oldQuestion = question
    end
  end
  function w:SetEnableButtons(enable)
    if w.send:IsEnabled() == enable then
      return
    end
    w.send:Enable(enable)
    w.refresh:Enable(enable)
    if enable == false then
      w.btnDelay = 1000
    end
  end
  local function Update(self, dt)
    if w.btnDelay > 0 then
      w.btnDelay = w.btnDelay - dt
    else
      w.btnDelay = 0
      w:SetEnableButtons(true)
    end
    if w.time == 0 then
      return
    end
    w.time = w.time - dt
    if 0 > w.time then
      w.time = 0
    end
    local strTime = GetCommonText("remain_time", FormatTime(w.time, false))
    w.remainTime:SetText(strTime)
  end
  w:SetHandler("OnUpdate", Update)
  local function SendAnswer()
    local answer = tonumber(w.editAnswer:GetText())
    if answer and answer > 0 and answer < 1000 then
      X2Player:AnswerBotCheck(answer)
      w:SetEnableButtons(false)
    else
      UIParent:Warning(string.format("[Lua Error] fail send answer.. (%s) ", tostring(answer)))
    end
    w.editAnswer:SetText("")
  end
  w.send:SetHandler("OnClick", SendAnswer)
  local function RefreshBotCheckInfo()
    X2Player:RefreshBotCheckInfo()
    w:SetEnableButtons(false)
  end
  w.refresh:SetHandler("OnClick", RefreshBotCheckInfo)
  local function OnEndAlphaAnime()
    w:SetAlpha(math.random(70, 95) / 100)
  end
  w:SetHandler("OnAlphaAnimeEnd", OnEndAlphaAnime)
  local function OnEndFadeIn()
    local textWidgets = {
      w.desc,
      w.remainTime,
      w.answerCount,
      w.restrictDesc
    }
    local fontColors = {
      FONT_COLOR.DEFAULT,
      FONT_COLOR.MIDDLE_TITLE,
      FONT_COLOR.GREEN,
      FONT_COLOR.BLUE,
      FONT_COLOR.BLACK,
      FONT_COLOR.RED
    }
    for i = 1, #textWidgets do
      local widget = textWidgets[i]
      if widget then
        ApplyTextColor(widget, fontColors[math.random(1, #fontColors)])
      end
    end
    local rangeWidth = (UIParent:GetScreenWidth() - w:GetWidth()) / 2
    local rangeHeight = (UIParent:GetScreenHeight() - w:GetHeight()) / 2
    local GetRandSign = function()
      local randSign = 1
      if math.random(1, 2) % 2 == 0 then
        randSign = -1
      end
      return randSign
    end
    local posX = math.random(1, rangeWidth) * GetRandSign()
    local posY = math.random(1, rangeHeight) * GetRandSign()
    w:RemoveAllAnchors()
    w:AddAnchor("CENTER", parent, posX, posY)
  end
  w:SetHandler("OnEndFadeIn", OnEndFadeIn)
  return w
end
function ShowCheckBotWindow()
  if checkBotWnd == nil then
    checkBotWnd = CreateBotCheckWindow("checkBotWnd", "UIParent")
    do
      local checkBotEvents = {
        UPDATE_BOT_CHECK_INFO = function(remainTime, count, question)
          checkBotWnd:UpdateInfos(remainTime, count, question)
        end
      }
      checkBotWnd:SetHandler("OnEvent", function(this, event, ...)
        checkBotEvents[event](...)
      end)
      RegistUIEvent(checkBotWnd, checkBotEvents)
    end
  end
  if checkBotWnd:IsVisible() == false then
    checkBotWnd:Show(true)
    checkBotWnd:SetEnableButtons(true)
    ADDON:RegisterContentWidget(UIC_CHECK_BOT_WND, checkBotWnd, ShowCheckBotWindow)
  end
end
ADDON:RegisterContentTriggerFunc(UIC_CHECK_BOT_WND, ShowCheckBotWindow)
