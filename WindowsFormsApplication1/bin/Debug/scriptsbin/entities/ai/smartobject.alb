SmartObject = {
  type = "SmartObject",
  Properties = {soclasses_SmartObjectClass = ""}
}
function SmartObject:OnInit()
end
function SmartObject:OnReset()
end
function SmartObject:OnUsed()
  BroadcastEvent(self, "Used")
end
function SmartObject:Event_Used(sender)
  BroadcastEvent(self, "Used")
end
SmartObject.FlowEvents = {
  Inputs = {
    Used = {
      SmartObject.Event_Used,
      "bool"
    }
  },
  Outputs = {Used = "bool"}
}
