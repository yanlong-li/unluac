Script.ReloadScript("scripts/entities/sound/soundeventspot.lua")
TeamSoundSpot = new(SoundEventSpot)
TeamSoundSpot.Properties.soundName = nil
TeamSoundSpot.MAX_TEAM_COUNT = 4
for i = 1, TeamSoundSpot.MAX_TEAM_COUNT do
  TeamSoundSpot.Properties["TeamSound" .. i] = {teamName = "", soundName = ""}
end
function TeamSoundSpot:OnSpawn()
end
function TeamSoundSpot:OnSetTeam(teamId)
  local teamName = g_gameRules.game:GetTeamName(teamId)
  for i = 1, TeamSoundSpot.MAX_TEAM_COUNT do
    local soundProps = self.Properties["TeamSound" .. i]
    if soundProps and soundProps.teamName == teamName then
      self:Stop()
      if soundProps.soundName ~= "" then
        self:Play(soundProps.soundName)
      end
      break
    end
  end
  if self.teamId then
    self.teamId = teamId
  end
end
function TeamSoundSpot:Play(soundName)
  if soundName then
    self.Properties.soundName = soundName
    SoundEventSpot.Play(self)
    self.Properties.soundName = nil
  end
end
