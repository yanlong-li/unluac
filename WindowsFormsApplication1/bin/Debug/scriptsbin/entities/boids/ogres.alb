Script.ReloadScript("Scripts/Entities/Boids/Chickens.lua")
Ogres = {
  Properties = {
    Boid = {
      object_Model = "objects/characters/monster/bulky_ogre_miner/bulky_ogre_miner.chr"
    },
    Movement = {MaxAnimSpeed = 1}
  },
  Animations = {
    "walk",
    "relaxed_idle_nw_01",
    "relaxed_idle_nw_02",
    "relaxed_idle_nw_03",
    "relaxed_idle_nw_01",
    "relaxed_idle_nw_01"
  },
  Editor = {Icon = "Bird.bmp"}
}
mergef(Ogres, Chickens, 1)
function Ogres:OnSpawn()
  self:SetFlags(ENTITY_FLAG_CLIENT_ONLY, 0)
end
function Ogres:GetFlockType()
  return Boids.FLOCK_TURTLES
end
