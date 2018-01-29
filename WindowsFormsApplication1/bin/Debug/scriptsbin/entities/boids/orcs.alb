Script.ReloadScript("Scripts/Entities/Boids/Chickens.lua")
Orcs = {
  Properties = {
    Boid = {
      object_Model = "objects/characters/monster/flathead_orc_warrior/flathead_orc_warrior.chr"
    },
    Movement = {MaxAnimSpeed = 1}
  },
  Animations = {
    "walk",
    "relaxed_idle_nw_01",
    "relaxed_idle_nw_01",
    "relaxed_idle_nw_01",
    "relaxed_idle_nw_01",
    "relaxed_idle_nw_01"
  },
  Editor = {Icon = "Bird.bmp"}
}
mergef(Orcs, Chickens, 1)
function Orcs:OnSpawn()
  self:SetFlags(ENTITY_FLAG_CLIENT_ONLY, 0)
end
function Orcs:GetFlockType()
  return Boids.FLOCK_TURTLES
end
