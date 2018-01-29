local taskQuest = {}
taskQuest.journal = nil
taskQuest.report = nil
local CreateTaskQuestJournal = function(id, parent)
  local widget = CreateSmallJournalFrame(id, parent, "task")
  widget.button:SetText(locale.common.ok)
  ApplyButtonSkin(widget.button, BUTTON_BASIC.DEFAULT)
  local function ButtonLeftClickFunc()
    widget:Show(false)
  end
  ButtonOnClickHandler(widget.button, ButtonLeftClickFunc)
  return widget
end
function ShowTaskQuestJournal(questType)
  if taskQuest.journal == nil then
    taskQuest.journal = CreateTaskQuestJournal("taskQuest.journal", "UIParent")
  end
  taskQuest.journal:FillJournal("task", questType)
  MoveWindowTo(taskQuest.journal, GetNotifierWnd())
  taskQuest.journal:Show(true)
end
function HideTaskQuestJournal()
  if taskQuest.journal == nil then
    return
  end
  if not taskQuest.journal:IsVisible() then
    return
  end
  taskQuest.journal:Show(false)
end
local CreateTaskQuestReport = function(id, parent)
  local widget = CreateSmallJournalFrame(id, parent, "task")
  widget.button:SetText(locale.questContext.takeReward)
  ApplyButtonSkin(widget.button, BUTTON_BASIC.DEFAULT)
  local function ButtonLeftClickFunc()
    local selectIdx = widget:GetSelectiveItemIndex()
    if selectIdx == nil then
      selectIdx = 0
    end
    local questType = widget:GetCurQuestType()
    if questType == nil then
      return
    end
    local selectiveRewardCount = X2Quest:GetQuestContextRewardSelectiveItemAllCount(questType)
    if selectiveRewardCount > 0 and (selectIdx < 1 or selectIdx == nil) then
      local message = locale.questContext.errors[10]
      if message == nil then
        message = locale.questContext.invalid_reward
      end
      AddMessageToSysMsgWindow(message)
      return
    end
    X2Quest:TryCompleteQuestContext(0, questType, "", "", selectIdx)
    widget:Show(false)
  end
  ButtonOnClickHandler(widget.button, ButtonLeftClickFunc)
  function widget:GetRelateButton()
    return widget.button
  end
  return widget
end
function ShowTaskQuestReport(questType)
  if taskQuest.report == nil then
    taskQuest.report = CreateTaskQuestReport("taskQuest.report", "UIParent")
    do
      local taskQuestReportEvent = {
        QUEST_TASK_READY = function(qtype)
          ShowTaskQuestReport(qtype)
        end
      }
      taskQuest.report:SetHandler("OnEvent", function(this, event, ...)
        taskQuestReportEvent[event](...)
      end)
      taskQuest.report:RegisterEvent("QUEST_TASK_READY")
    end
  end
  taskQuest.report:FillJournal("task", questType)
  MoveWindowTo(taskQuest.report, GetNotifierWnd())
  taskQuest.report:Show(true)
end
