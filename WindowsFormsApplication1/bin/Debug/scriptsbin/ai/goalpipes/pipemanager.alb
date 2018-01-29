PipeManager = {}
function PipeManager:OnInit()
  AI.LogEvent("PipeManager initialized")
  Script.ReloadScript("Scripts/AI/GoalPipes/pipe_manager_x2.lua")
  PipeManager:InitX2()
  AI.CreateGoalPipe("_action_")
  AI.PushGoal("_action_", "timeout", 1, 0.1)
  AI.PushGoal("_action_", "branch", 1, -1, BRANCH_ALWAYS)
  AI.CreateGoalPipe("_last_")
end
