Rain = {
  type = "Rain",
  Properties = {
    bEnabled = 1,
    fRadius = 1000,
    fAmount = 1,
    fLevel = 1,
    fReflectionAmount = 1,
    fFakeGlossiness = 1,
    fPuddlesAmount = 3,
    bRainDrops = 1,
    fRainDropsSpeed = 1,
    clrColor = {
      x = 1,
      y = 1,
      z = 1
    },
    fUmbrellaRadius = 0,
    fSnowAmount = 1,
    fSnowMinAmount = 0,
    fSnowNormalPress = 0,
    fSnowDropAmount = 1,
    fSnowScatter = 10,
    bIsSnow = 0,
    fMaxViewDist = 1000,
    fSnowDownwardOffset = 100,
    texture_Texture = "Textures/Defaults/rainfall.tif"
  },
  Editor = {Icon = "shake.bmp"}
}
function Rain:OnInit()
  self:OnReset()
end
function Rain:OnPropertyChange()
  self:OnReset()
end
function Rain:OnReset()
end
function Rain:OnSave(tbl)
end
function Rain:OnLoad(tbl)
end
function Rain:OnShutDown()
end
function Rain:Event_Enable(sender)
  self.Properties.bEnabled = 1
end
function Rain:Event_Disable(sender)
  self.Properties.bEnabled = 0
end
function Rain:Event_SetAmount(i, val)
  self.Properties.fAmount = val
end
function Rain:Event_SetUmbrella(i, val)
  self.Properties.fUmbrellaRadius = val
end
function Rain:Event_SetSnowAmount(i, val)
  self.Properties.fSnowAmount = val
end
Rain.FlowEvents = {
  Inputs = {
    Enable = {
      Rain.Event_Enable,
      "bool"
    },
    Disable = {
      Rain.Event_Disable,
      "bool"
    },
    Amount = {
      Rain.Event_SetAmount,
      "float"
    },
    UmbrellaRadius = {
      Rain.Event_SetUmbrella,
      "float"
    },
    SnowAmount = {
      Rain.Event_SetSnowAmount,
      "float"
    }
  },
  Outputs = {Disable = "bool", Enable = "bool"}
}
