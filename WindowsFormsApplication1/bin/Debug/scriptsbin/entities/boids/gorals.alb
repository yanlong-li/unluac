Script.ReloadScript("Scripts/Entities/Boids/Chickens.lua")
Gorals = {
  Properties = {
    Boid = {
      object_Model = "objects/characters/animals/goral/goral.chr"
    },
    Movement = {MaxAnimSpeed = 1}
  },
  Animations = {
    "goral_walk",
    "goral_stand_a",
    "goral_stand_b",
    "goral_stand_a",
    "goral_stand_a",
    "goral_stand_a"
  },
  Editor = {Icon = "Bird.bmp"}
}
mergef(Gorals, Chickens, 1)
function Gorals:OnSpawn()
  self:SetFlags(ENTITY_FLAG_CLIENT_ONLY, 0)
end
function Gorals:GetFlockType()
  return Boids.FLOCK_TURTLES
end
