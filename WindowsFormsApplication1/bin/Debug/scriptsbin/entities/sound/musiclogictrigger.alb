MusicLogicTrigger = {
  type = "MusicLogicTrigger",
  Editor = {Icon = "Music.bmp"},
  Properties = {
    bEnable = 1,
    bIndoorOnly = 0,
    bOutdoorOnly = 0
  },
  InsideArea = 0
}
function MusicLogicTrigger:OnSave(stm)
  stm.InsideArea = self.InsideArea
end
function MusicLogicTrigger:OnLoad(stm)
  self.InsideArea = stm.InsideArea
end
function MusicLogicTrigger:CliSrv_OnInit()
end
function MusicLogicTrigger:OnShutDown()
end
function MusicLogicTrigger:Client_OnEnterArea(player, areaId)
  if g_localActorId ~= player.id then
    return
  end
  local bActivate = 1
  local Indoor = System.IsPointIndoors(System.GetViewCameraPos())
  if self.Properties.bIndoorOnly == 1 and Indoor == nil then
    bActivate = 0
  elseif self.Properties.bOutdoorOnly == 1 and Indoor ~= nil then
    bActivate = 0
  end
  if bActivate == 1 then
    self.InsideArea = 1
    if self.Properties.bEnable == 1 then
      MusicLogic.StartLogic()
    else
      MusicLogic.StopLogic()
    end
  elseif bActivate == 0 and self.InsideArea == 1 then
    self.InsideArea = 0
  end
end
function MusicLogicTrigger:Client_OnLeaveArea(player, areaId)
  if g_localActorId ~= player.id then
    return
  end
  local bActivate = 1
  local Indoor = System.IsPointIndoors(System.GetViewCameraPos())
  if self.Properties.bIndoorOnly == 1 and Indoor == nil then
    bActivate = 0
  elseif self.Properties.bOutdoorOnly == 1 and Indoor ~= nil then
    bActivate = 0
  end
  if bActivate == 1 then
    self.InsideArea = 0
    if self.Properties.bEnable == 1 then
      MusicLogic.StopLogic()
    else
      MusicLogic.StartLogic()
    end
  elseif bActivate == 0 and self.InsideArea == 0 then
    self.InsideArea = 1
  end
end
function MusicLogicTrigger:Event_Enable()
  MusicLogic.StartLogic()
end
function MusicLogicTrigger:Event_Disable()
  MusicLogic.StopLogic()
end
MusicLogicTrigger.Server = {
  OnInit = function(self)
    self:CliSrv_OnInit()
  end,
  OnShutDown = function(self)
  end,
  Inactive = {},
  Active = {}
}
MusicLogicTrigger.Client = {
  OnInit = function(self)
    self:CliSrv_OnInit()
  end,
  OnShutDown = function(self)
  end,
  OnEnterArea = MusicLogicTrigger.Client_OnEnterArea,
  OnLeaveArea = MusicLogicTrigger.Client_OnLeaveArea
}
MusicLogicTrigger.FlowEvents = {
  Inputs = {
    Enable = {
      MusicLogicTrigger.Event_Enable,
      "bool"
    },
    Disable = {
      MusicLogicTrigger.Event_Disable,
      "bool"
    }
  },
  Outputs = {}
}
