Frogs = {
  type = "Boids",
  Properties = {
    Movement = {
      SpeedMin = 2,
      SpeedMax = 4,
      MaxAnimSpeed = 1
    },
    Boid = {
      nCount = 10,
      object_Model = "objects/characters/animals/frog/frog_green/frog_green_boid.chr",
      Size = 1,
      SizeRandom = 0,
      Mass = 1
    },
    Options = {
      bFollowPlayer = 0,
      bObstacleAvoidance = 1,
      VisibilityDist = 30,
      bActivate = 1,
      Radius = 10
    },
    Sounds = {
      soundS0_cluck = "",
      soundS1_scared = "",
      soundS2_die = "",
      soundS3_pickup = "",
      soundS4_throw = ""
    }
  },
  Animations = {
    "frog_walk_01",
    "frog_idle_01"
  },
  Editor = {Icon = "Bug.bmp"},
  params = {
    x = 0,
    y = 0,
    z = 0
  }
}
function Frogs:OnSpawn()
  self:SetFlags(ENTITY_FLAG_CLIENT_ONLY, 0)
end
function Frogs:OnInit()
  self.flock = 0
  self.currpos = {
    x = 0,
    y = 0,
    z = 0
  }
  if self.Properties.Options.bActivate == 1 then
    self:CreateFlock()
  end
end
function Frogs:OnShutDown()
end
function Frogs:GetSounds()
  local Sounds = self.Properties.Sounds
  local r = {}
  if Sounds.soundS0_cluck ~= "" then
    table.insert(r, Sounds.soundS0_cluck)
  end
  if Sounds.soundS1_scared ~= "" then
    table.insert(r, Sounds.soundS1_scared)
  end
  if Sounds.soundS2_die ~= "" then
    table.insert(r, Sounds.soundS2_die)
  end
  if Sounds.soundS3_pickup ~= "" then
    table.insert(r, Sounds.soundS3_pickup)
  end
  if Sounds.soundS4_throw ~= "" then
    table.insert(r, Sounds.soundS4_throw)
  end
  return r
end
function Frogs:CreateFlock()
  local Movement = self.Properties.Movement
  local Boid = self.Properties.Boid
  local Options = self.Properties.Options
  local params = self.params
  params.count = Boid.nCount
  params.model = Boid.object_Model
  params.boid_size = Boid.Size
  params.boid_size_random = Boid.SizeRandom
  params.min_speed = Movement.SpeedMin
  params.max_speed = Movement.SpeedMax
  params.factor_align = 0
  params.spawn_radius = Options.Radius
  params.gravity_at_death = -9.81
  params.boid_mass = Boid.Mass
  params.max_anim_speed = Movement.MaxAnimSpeed
  params.follow_player = Options.bFollowPlayer
  params.avoid_obstacles = Options.bObstacleAvoidance
  params.max_view_distance = Options.VisibilityDist
  params.Sounds = self:GetSounds()
  params.Animations = self.Animations
  if self.flock == 0 then
    self.flock = 1
    Boids.CreateFlock(self, Boids.FLOCK_FROGS, params)
  end
  if self.flock ~= 0 then
    Boids.SetFlockParams(self, params)
  end
end
function Frogs:OnPropertyChange()
  self:OnShutDown()
  if self.Properties.Options.bActivate == 1 then
    self:CreateFlock()
  end
end
function Frogs:Event_Activate()
  if self.Properties.Options.bActivate == 0 and self.flock == 0 then
    self:CreateFlock()
  end
  if self.flock ~= 0 then
    Boids.EnableFlock(self, 1)
  end
end
function Frogs:Event_Deactivate()
  if self.flock ~= 0 then
    Boids.EnableFlock(self, 0)
  end
end
function Frogs:OnProceedFadeArea(player, areaId, fadeCoeff)
  if self.flock ~= 0 then
    Boids.SetFlockPercentEnabled(self, fadeCoeff * 100)
  end
end
Frogs.FlowEvents = {
  Inputs = {
    Activate = {
      Frogs.Event_Activate,
      "bool"
    },
    Deactivate = {
      Frogs.Event_Deactivate,
      "bool"
    }
  },
  Outputs = {Activate = "bool", Deactivate = "bool"}
}
