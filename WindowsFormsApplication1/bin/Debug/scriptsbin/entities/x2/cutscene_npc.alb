Cutscene_Npc = {
  Properties = {npctype_NpcType = 1}
}
MakeCutsceneEventsAcceptable(Cutscene_Npc)
function Cutscene_Npc:OnSpawn()
  self:CustomizeEntityIfNeeded()
end
function Cutscene_Npc:OnPropertyChange()
  self:CustomizeEntityIfNeeded()
end
function Cutscene_Npc:OnReset()
  self:CustomizeEntityIfNeeded()
end
function Cutscene_Npc:CustomizeEntityIfNeeded()
  if self.npcType ~= self.Properties.npctype_NpcType then
    self.npcType = self.Properties.npctype_NpcType
    self:CreateCutsceneNpc(self.npcType)
  end
end
