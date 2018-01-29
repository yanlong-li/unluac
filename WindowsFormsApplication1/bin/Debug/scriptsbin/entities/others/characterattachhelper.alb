CharacterAttachHelper = {
  Editor = {Icon = "Magnet.bmp"},
  Properties = {BoneName = "Bip01 Head"}
}
function CharacterAttachHelper:OnInit()
  self:EnableInheritXForm(0)
  self:MakeAttachment()
end
function CharacterAttachHelper:OnStartGame()
  self:MakeAttachment()
end
function CharacterAttachHelper:OnShutDown()
  local parent = self:GetParent()
  if parent then
    parent:DestroyAttachment(0, self:GetName())
  end
end
function CharacterAttachHelper:OnPropertyChange()
  local parent = self:GetParent()
  if parent then
    parent:DestroyAttachment(0, self:GetName())
    self:MakeAttachment()
  end
end
function CharacterAttachHelper:MakeAttachment()
  local parent = self:GetParent()
  if parent then
    parent:DestroyAttachment(0, self:GetName())
    parent:CreateBoneAttachment(0, self.Properties.BoneName, self:GetName())
    parent:SetAttachmentObject(0, self:GetName(), self.id, -1, 0)
  end
end
