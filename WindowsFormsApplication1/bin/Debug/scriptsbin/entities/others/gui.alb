GUI = {
  Properties = {
    objModel = "objects/box.cgf",
    bRigidBody = 1,
    bResting = 1,
    bUsable = 1,
    bPhysicalized = 1,
    fMass = 1,
    GUIMaterial = "",
    GUIUsageDistance = 1.5,
    GUIUsageTolerance = 0.75,
    GUIWidth = 512,
    GUIHeight = 512,
    GUIDefaultScreen = "main",
    GUIMouseCursor = "textures/gui/mouse.dds",
    GUIPreUpdate = 1,
    GUIMouseCursorSize = 18,
    GUIHasFocus = 0,
    color_GUIBackgroundColor = {
      0,
      0,
      0
    },
    fileGUIScript = ""
  },
  Client = {}
}
function GUI:OnSpawn()
  self:OnReset()
end
function GUI:OnDestroy()
end
function GUI:OnReset()
  self:Activate(1)
  self:SetUpdatePolicy(ENTITY_UPDATE_VISIBLE)
  self:LoadObject(0, self.Properties.objModel)
  self:DrawSlot(0, 1)
  if tonumber(self.Properties.bPhysicalized) ~= 0 then
    local physType = PE_STATIC
    local physParam = {
      mass = self.Properties.fMass
    }
    if tonumber(self.Properties.bRigidBody) ~= 0 then
      physType = PE_RIGID
    end
    self:Physicalize(0, physType, physParam)
    if tonumber(self.Properties.bResting) ~= 0 then
      self:AwakePhysics(0)
    else
      self:AwakePhysics(1)
    end
  end
  self:CreateUI()
end
function GUI.Client:OnUpdate(frameTime)
  if not g_localActor or not self.ui then
    return
  end
  if self.Properties.bUsable ~= 0 then
    local stats = g_localActor.actor:GetStats()
    if not self.using and stats.flatSpeed > 0.5 then
      return
    end
    local using = false
    local distance = self.Properties.GUIUsageDistance
    local guiMaterial = self.Properties.GUIMaterial
    if guiMaterial and string.len(guiMaterial) < 1 then
      guiMaterial = nil
    end
    if self.using then
      distance = distance + self.Properties.GUIUsageTolerance
    end
    local hits = self:IntersectRay(-1, System.GetViewCameraPos(), System.GetViewCameraDir(), distance)
    if hits then
      for i = 1, table.getn(hits) do
        local hit = hits[i]
        if hit and not hit.backface and 0 < hit.distance and (not guiMaterial or guiMaterial == hit.material) then
          local ui = self.ui
          local s = hit.baricentric.x
          local t = hit.baricentric.y
          local q = hit.baricentric.z
          local x = (hit.uv0.x * s + hit.uv1.x * t + hit.uv2.x * q) * 800
          local y = (hit.uv0.y * s + hit.uv1.y * t + hit.uv2.y * q) * 600
          local mx, my = self.ui:GetMouseXY()
          if mx ~= x or my ~= y then
            self.ui:Message("MouseMove", x - mx, y - my, 0)
          end
          using = true
          break
        end
      end
    end
    if using and not self.using then
      self:OnStartUsing()
    elseif not using and self.using then
      self:OnStopUsing()
    end
    self.using = using
  end
end
function GUI:SetUI(name)
  self:DestroyUI()
  local gui = UI:GetUI(name)
  gui:Focus(self.Properties.GUIHasFocus ~= 0)
  gui:EnablePreUpdate(self.Properties.GUIPreUpdate ~= 0)
  self.ui = gui
  self:RenderUI(true)
end
function GUI:CreateUI()
  self:DestroyUI()
  local gui
  if self.Properties.fileGUIScript and string.len(self.Properties.fileGUIScript) > 0 then
    ui = UI:BeginUI(self:GetName() .. "_gui", self.Properties.GUIWidth, self.Properties.GUIHeight)
    Script.ReloadScript(self.Properties.fileGUIScript)
    UI:EndUI()
  end
  if gui then
    self.ui = gui
    gui:Focus(self.Properties.GUIHasFocus ~= 0)
    gui:EnablePreUpdate(self.Properties.GUIPreUpdate ~= 0)
    gui.mouseCursor = self.Properties.GUIMouseCursor
    gui.mouseSize = tonumber(self.Properties.GUIMouseCursorSize)
    gui.backgroundColor = {}
    gui.backgroundColor[1] = self.Properties.color_GUIBackgroundColor[1] * 255
    gui.backgroundColor[2] = self.Properties.color_GUIBackgroundColor[2] * 255
    gui.backgroundColor[3] = self.Properties.color_GUIBackgroundColor[3] * 255
    gui.backgroundColor[4] = 255
  end
  self:DefaultScreen()
  self:RenderUI(true)
end
function GUI:DestroyUI()
  if self.ui then
    self.ui:Activate(false)
    self.ui:DestroyRenderTarget()
  end
end
function GUI:RenderUI(render)
  if not self.ui then
    return
  end
  if render then
    self.ui:Activate(true)
    local texId = self.ui:GetRenderTargetId()
    texId = texId or self.ui:CreateRenderTarget()
    self:SetPublicParam("texture", texId)
  else
    self.ui:Activate(false)
    if self.ui:GetRenderTargetId() then
      self.ui:DestroyRenderTarget()
    end
  end
end
function GUI:DefaultScreen()
  if not self.ui then
    return
  end
  local initalScreen = self.Properties.GUIDefaultScreen
  if not initialScreen or string.len(initialScreen) < 1 then
    initialScreen = "main"
  end
  local screen = self.ui:GetScreen(initialScreen)
  if screen then
    screen:Activate(true)
  end
end
function GUI:OnPropertyChange()
  self:OnReset()
end
function GUI:OnHit(hit)
end
function GUI:OnStartUsing()
  if not self.ui then
    return
  end
  self.ui:Focus(true)
  if g_localActor then
    g_localActor:HolsterItem(true)
  end
end
function GUI:OnStopUsing()
  if not self.ui then
    return
  end
  self.ui:Focus(false)
  if g_localActor then
    g_localActor:HolsterItem(false)
  end
end
