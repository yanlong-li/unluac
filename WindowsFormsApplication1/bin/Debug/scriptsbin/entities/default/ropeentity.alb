Script.ReloadScript("scripts/Utils/EntityUtils.lua")
RopeEntity = {}
function RopeEntity:OnPhysicsBreak(vPos, nPartId, nOtherPartId)
  self:ActivateOutput("Break", nPartId + 1)
end
function RopeEntity:Event_Remove()
  self:DrawSlot(0, 0)
  self:DestroyPhysics()
  self:ActivateOutput("Remove", true)
end
function RopeEntity:Event_Hide()
  self:Hide(1)
  self:ActivateOutput("Hide", true)
end
function RopeEntity:Event_UnHide()
  self:Hide(0)
  self:ActivateOutput("UnHide", true)
end
function RopeEntity:Event_Break(vPos, nPartId, nOtherPartId)
  local RopeParams = {}
  RopeParams.entity_name_2 = "#unattached"
  RopeParams.entity_name_1 = "#unattached"
  self:SetPhysicParams(0, PHYSICPARAM_ROPE, RopeParams)
end
RopeEntity.FlowEvents = {
  Inputs = {
    Hide = {
      RopeEntity.Event_Hide,
      "bool"
    },
    UnHide = {
      RopeEntity.Event_UnHide,
      "bool"
    },
    Remove = {
      RopeEntity.Event_Remove,
      "bool"
    },
    Break = {
      RopeEntity.Event_Break,
      "bool"
    }
  },
  Outputs = {
    Hide = "bool",
    UnHide = "bool",
    Remove = "bool",
    Break = "int"
  }
}
