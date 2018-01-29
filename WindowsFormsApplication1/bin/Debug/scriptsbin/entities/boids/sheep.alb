Script.ReloadScript("Scripts/Entities/Boids/Chickens.lua")
Sheep = {
  Properties = {
    Boid = {
      object_Model = "objects/characters/animals/sheep/sheep.chr"
    }
  },
  Animations = {
    "sheep_walkf",
    "sheep_idle_a",
    "sheep_idle_b",
    "sheep_idle",
    "sheep_idle",
    "sheep_idle"
  },
  Editor = {Icon = "Bird.bmp"}
}
mergef(Sheep, Chickens, 1)
function Sheep:OnSpawn()
  self:SetFlags(ENTITY_FLAG_CLIENT_ONLY, 0)
end
function Sheep:GetFlockType()
  return Boids.FLOCK_TURTLES
end
