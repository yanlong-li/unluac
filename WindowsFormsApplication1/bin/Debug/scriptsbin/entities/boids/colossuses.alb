Script.ReloadScript("Scripts/Entities/Boids/Chickens.lua")
Colossuses = {
  Properties = {
    Boid = {
      object_Model = "objects/characters/monster/wandering_piece_colossus/wandering_piece_colossus.chr"
    },
    Movement = {MaxAnimSpeed = 1}
  },
  Animations = {
    "wandering_piece_colossus_idle",
    "wandering_piece_colossus_idle",
    "wandering_piece_colossus_attackunarmed",
    "wandering_piece_colossus_idle",
    "wandering_piece_colossus_idle",
    "wandering_piece_colossus_idle"
  },
  Editor = {Icon = "Bird.bmp"}
}
mergef(Colossuses, Chickens, 1)
function Colossuses:OnSpawn()
  self:SetFlags(ENTITY_FLAG_CLIENT_ONLY, 0)
end
function Colossuses:GetFlockType()
  return Boids.FLOCK_TURTLES
end
