Explosion = {
  Properties = {
    bActive = 1,
    soclasses_SmartObjectClass = "",
    Explosion = {
      Effect = "explosions.grenade_air.explosion",
      EffectScale = 1,
      Radius = 10,
      Pressure = 1000,
      Damage = 1000,
      Decal = "textures/decal/explo_decal.dds",
      HoleSize = 10,
      TerrainHoleSize = 5,
      Direction = {
        x = 0,
        y = 0,
        z = 1
      }
    }
  },
  Editor = {Icon = "Item.bmp", IconOnTop = 1}
}
function Explosion:OnPropertyChange()
  self:OnReset()
end
function Explosion:OnInit()
  self:OnReset()
  self:PreLoadParticleEffect(self.Properties.Explosion.Effect)
end
function Explosion:OnReset()
  self.bActive = self.Properties.bActive
end
function Explosion:OnSave(save)
  save.bActive = self.bActive
end
function Explosion:OnLoad(saved)
  self.bActive = saved.bActive
end
function Explosion:Explode(shooterId)
  local expl = self.Properties.Explosion
  g_gameRules:CreateExplosion(NULL_ENTITY, self.id, expl.Damage, self:GetWorldPos(), expl.Direction, expl.Radius, nil, expl.Pressure, expl.HoleSize, expl.Effect, expl.EffectScale)
  BroadcastEvent(self, "Explode")
  System.RemoveEntity(self.id)
end
function Explosion:Event_Explode(sender)
  if tonumber(self.bActive) ~= 0 then
    self:Explode(NULL_ENTITY)
  end
end
function Explosion:Event_Activate(sender)
  self.bActive = 1
  BroadcastEvent(self, "Activate")
end
function Explosion:Event_Deactivate(sender)
  self.bActive = 0
  BroadcastEvent(self, "Deactivate")
end
Explosion.FlowEvents = {
  Inputs = {
    Activate = {
      Explosion.Event_Activate,
      "bool"
    },
    Deactivate = {
      Explosion.Event_Deactivate,
      "bool"
    },
    Explode = {
      Explosion.Event_Explode,
      "bool"
    }
  },
  Outputs = {
    Activate = "bool",
    Deactivate = "bool",
    Explode = "bool"
  }
}
