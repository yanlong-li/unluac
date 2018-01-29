AreaTrigger = {
  type = "Trigger",
  Properties = {
    bEnabled = 1,
    bTriggerOnce = 0,
    bOnlyPlayers = 0,
    bOnlyLocalPlayer = 0,
    ScriptCommand = "",
    species = -1,
    bInVehicleOnly = 0
  },
  States = {"Empty", "Occupied"},
  Editor = {
    Model = "Editor/Objects/T.cgf",
    Icon = "AreaTrigger.bmp",
    ShowBounds = 1
  },
  Who = nil,
  Enabled = 1,
  Entered = 0,
  triggeredIn = 0,
  triggeredOut = 0
}
function AreaTrigger:OnPropertyChange()
  self.Enabled = self.Properties.bEnabled
  self.Who = nil
  self.Entered = 0
  self.triggeredIn = 0
  self.triggeredOut = 0
  self:GotoState("Empty")
end
function AreaTrigger:OnInit()
  self:Activate(0)
  self:OnPropertyChange()
end
function AreaTrigger:OnSave(props)
  props.Who = self.Who
  props.Enabled = self.Enabled
  props.triggeredIn = self.triggeredIn
  props.triggeredOut = self.triggeredOut
end
function AreaTrigger:OnLoad(props)
  self.Who = props.Who
  self.Enabled = props.Enabled
  self.triggeredIn = props.triggeredIn
  self.triggeredOut = props.triggeredOut
end
function AreaTrigger:OnReset()
  self:OnPropertyChange()
end
function AreaTrigger:OnShutDown()
end
function AreaTrigger:Event_Enter(sender)
  if self.Enabled ~= 1 then
    return
  end
  self.Entered = 1
  if sender ~= nil and self.Properties.species ~= -1 then
    local senderSpecies, bIsLeader
    if sender and sender.actor then
      if sender.actor:IsPlayer() then
        senderSpecies = 0
        bIsLeader = true
      elseif sender.Properties and sender.Properties.species then
        senderSpecies = sender.Properties.species
        if sender.AI then
          bIsLeader = sender.AI.bIsLeader
        end
      end
      if bIsLeader then
        g_SignalData.iValue = senderSpecies
        if senderSpecies ~= self.Properties.species then
          AI.Signal(SIGNALFILTER_LEADER, 1, "OnEnterEnemyArea", sender.id, g_SignalData)
        else
          AI.Signal(SIGNALFILTER_LEADER, 1, "OnEnterFriendlyArea", sender.id, g_SignalData)
        end
      end
    end
  end
  BroadcastEvent(self, "Enter")
end
function AreaTrigger:Event_Leave(sender)
  if self.Enabled ~= 1 then
    return
  end
  if self.Properties.species ~= -1 then
    local senderSpecies, bIsLeader
    if sender and sender.actor then
      if sender.actor:IsPlayer() then
        senderSpecies = 0
        bIsLeader = true
      elseif sender.Properties and sender.Properties.species then
        senderSpecies = sender.Properties.species
        if sender.AI then
          bIsLeader = sender.AI.bIsLeader
        end
      end
      if bIsLeader then
        g_SignalData.iValue = senderSpecies
        if senderSpecies ~= self.Properties.species then
          AI.Signal(SIGNALFILTER_LEADER, 1, "OnLeaveEnemyArea", sender.id, g_SignalData)
        else
          AI.Signal(SIGNALFILTER_LEADER, 1, "OnLeaveFriendlyArea", sender.id, g_SignalData)
        end
      end
    end
  end
  BroadcastEvent(self, "Leave")
end
function AreaTrigger:Event_Enable(sender)
  self.Enabled = 1
  BroadcastEvent(self, "Enable")
end
function AreaTrigger:Event_Disable(sender)
  self.Enabled = 0
  self:GotoState("Empty")
  BroadcastEvent(self, "Disable")
end
AreaTrigger.Empty = {
  OnBeginState = function(self)
    self.Who = nil
    self.Entered = 0
  end,
  OnEndState = function(self)
  end,
  OnEnterArea = function(self, player, areaId)
    if self.Enabled ~= 1 then
      return
    elseif self.Properties.bInVehicleOnly ~= 0 and g_localActor.vehicleId == nil then
      return
    end
    if self.Properties.bOnlyPlayers ~= 0 and player.type ~= "Player" then
      return
    end
    if self.Properties.bOnlyLocalPlayer ~= 0 and player ~= g_localActor then
      return
    end
    if self.Who == nil then
      self.Who = player
      self:GotoState("Occupied")
    end
  end
}
AreaTrigger.Occupied = {
  OnBeginState = function(self)
    if self.Properties.bTriggerOnce == 1 and self.triggeredIn == 1 then
      self.Entered = 1
      return
    end
    self.triggeredIn = 1
    self:Event_Enter(self.Who)
    if self.Properties.ScriptCommand and self.Properties.ScriptCommand ~= "" then
      local f = loadstring(self.Properties.ScriptCommand)
      if f ~= nil then
        f()
      end
    end
  end,
  OnEndState = function(self)
  end,
  OnLeaveArea = function(self, player, areaId)
    if self.Enabled ~= 1 then
      return
    elseif self.Properties.bInVehicleOnly ~= 0 and g_localActor.vehicleId == nil then
      return
    end
    if self.Properties.bOnlyPlayers ~= 0 and player.type ~= "Player" then
      return
    end
    if self.Properties.bOnlyLocalPlayer ~= 0 and player ~= g_localActor then
      return
    end
    if self.Properties.bOnlyLocalPlayer ~= 0 and player ~= g_localActor then
      return
    end
    if self.Properties.bTriggerOnce ~= 1 or self.triggeredOut ~= 1 then
      self.triggeredOut = 1
      self:Event_Leave(self.Who)
    end
    self.triggeredOut = 1
    self:GotoState("Empty")
  end
}
AreaTrigger.FlowEvents = {
  Inputs = {
    Disable = {
      AreaTrigger.Event_Disable,
      "bool"
    },
    Enable = {
      AreaTrigger.Event_Enable,
      "bool"
    },
    Enter = {
      AreaTrigger.Event_Enter,
      "bool"
    },
    Leave = {
      AreaTrigger.Event_Leave,
      "bool"
    }
  },
  Outputs = {
    Disable = "bool",
    Enable = "bool",
    Enter = "bool",
    Leave = "bool"
  }
}
