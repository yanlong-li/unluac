MusicStinger = {
  type = "MusicStinger",
  Editor = {Icon = "Music.bmp"},
  Properties = {bIndoorOnly = 0, bOutdoorOnly = 0},
  InsideArea = 0
}
function MusicStinger:OnSave(stm)
  stm.InsideArea = self.InsideArea
end
function MusicStinger:OnLoad(stm)
  self.InsideArea = stm.InsideArea
end
function MusicStinger:CliSrv_OnInit()
end
function MusicStinger:OnShutDown()
end
function MusicStinger:Client_OnEnterArea(player, areaId)
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
    Sound.PlayStinger()
  elseif bActivate == 0 and self.InsideArea == 1 then
    self.InsideArea = 0
  end
end
function MusicStinger:Event_Play()
  Sound.PlayStinger()
end
MusicStinger.Server = {
  OnInit = function(self)
    self:CliSrv_OnInit()
  end,
  OnShutDown = function(self)
  end,
  Inactive = {},
  Active = {}
}
MusicStinger.Client = {
  OnInit = function(self)
    self:CliSrv_OnInit()
  end,
  OnShutDown = function(self)
  end,
  OnEnterArea = MusicStinger.Client_OnEnterArea,
  OnLeaveArea = MusicStinger.Client_OnLeaveArea,
  OnProceedFadeArea = MusicStinger.Client_OnProceedFadeArea
}
MusicStinger.FlowEvents = {
  Inputs = {
    Play = {
      MusicStinger.Event_Play,
      "bool"
    }
  },
  Outputs = {}
}
