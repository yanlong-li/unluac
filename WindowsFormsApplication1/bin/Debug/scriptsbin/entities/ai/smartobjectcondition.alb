System.Log("Loading SmartObjectCondition.lua")
SmartObjectCondition = {
  type = "SmartObjectCondition",
  Properties = {
    bEnabled = true,
    bIncludeInNavigation = true,
    bRelativeToTarget = false,
    Object = {
      soclass_Class = "BasicEntity",
      sostate_State = "Idle",
      object_Model = ""
    },
    User = {
      soclass_Class = "AIOBJECT_PUPPET",
      sostate_State = "Idle",
      object_Model = ""
    },
    Limits = {fDistance = 10, fOrientation = 360},
    Delay = {
      fMinimum = 0.5,
      fMaximum = 15,
      fMemory = 1
    },
    Multipliers = {
      fProximity = 1,
      fOrientation = 0,
      fVisibility = 0,
      fRandomness = 0.25
    },
    Action = {
      soaction_Name = "",
      sostate_ObjectState = "Busy",
      sostate_UserState = "Busy"
    }
  },
  Editor = {
    Model = "Editor/Objects/Pyramid.cgf"
  }
}
function SmartObjectCondition:OnPropertyChange()
end
function SmartObjectCondition:OnInit()
end
function SmartObjectCondition:OnReset()
  self:Register()
end
function SmartObjectCondition:Register()
end
