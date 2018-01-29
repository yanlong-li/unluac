AI.LogEvent("[AISYSTEM] AIConfig starts  -------------------------------------")
Script.ReloadScript("Scripts/AI/anchor.lua")
Script.ReloadScript("Scripts/AI/Characters/AICharacter.lua")
Script.ReloadScript("Scripts/AI/Logic/AI_BoredManager.lua")
Script.ReloadScript("Scripts/AI/GoalPipes/PipeManager.lua")
Script.ReloadScript("Scripts/AI/Behaviors/AIBehaviour.lua")
Script.ReloadScript("Scripts/AI/Logic/IdleManager.lua")
Script.ReloadScript("Scripts/AI/Formations/FormationManager.lua")
Script.ReloadScript("Scripts/AI/TrackPatterns/TrackPatternManager.lua")
Script.ReloadScript("Scripts/AI/Packs/Animation/PACKS.lua")
ANIMATIONPACK:LoadAll()
AIBehaviour:LoadAll()
AICharacter:LoadAll()
Script.ReloadScript("Scripts/AI/Logic/SideSelector.lua")
Script.ReloadScript("Scripts/AI/Logic/SoundPackRandomizer.lua")
Script.ReloadScript("Scripts/AI/Logic/x2ai.lua")
Script.ReloadScript("Scripts/AI/Packs/AI_IdleTable.lua")
Script.ReloadScript("SCRIPTS/AI/Logic/NOCOVER.lua")
Script.ReloadScript("SCRIPTS/AI/Logic/BlackBoard.lua")
if PipeManager then
  PipeManager:OnInit()
end
if FormationManager then
  FormationManager:OnInit()
end
if TrackPatternManager then
  TrackPatternManager:OnInit()
end
AICombatClasses = {}
AI.AddCombatClass()
AICombatClasses.Player = 0
AICombatClasses.PlayerRPG = 1
AICombatClasses.Infantry = 2
AICombatClasses.InfantryRPG = 3
AICombatClasses.Tank = 4
AICombatClasses.TankHi = 5
AICombatClasses.Heli = 6
AICombatClasses.VehicleGunner = 7
AICombatClasses.Hunter = 8
AICombatClasses.Civilian = 9
AICombatClasses.Car = 10
AICombatClasses.Warrior = 11
AICombatClasses.AAA = 12
AICombatClasses.BOAT = 13
AICombatClasses.APC = 14
AICombatClasses.Squadmate = 15
AICombatClasses.Scout = 16
AICombatClasses.ascensionScout = 17
AICombatClasses.ascensionVTOL = 18
AICombatClasses.ascensionScout2 = 19
AI.AddCombatClass(AICombatClasses.Player, {
  1,
  1,
  1,
  1,
  1.1,
  1.1,
  1,
  1,
  2,
  0,
  0,
  0,
  1,
  1,
  1,
  0.1,
  1,
  1,
  0,
  0
})
AI.AddCombatClass(AICombatClasses.PlayerRPG, {
  1,
  1,
  1,
  1,
  1.5,
  1.5,
  1,
  1,
  2,
  0,
  1.2,
  0,
  1.2,
  1,
  1.2,
  0.1,
  1,
  1,
  0,
  0
})
AI.AddCombatClass(AICombatClasses.Infantry, {
  1,
  1,
  1,
  1,
  1.1,
  1.1,
  1,
  1,
  2,
  0,
  0,
  0,
  1,
  1,
  1,
  0.1,
  1,
  1,
  0,
  0
})
AI.AddCombatClass(AICombatClasses.InfantryRPG, {
  1,
  1,
  1,
  1,
  1.5,
  1.5,
  1,
  1,
  2,
  0,
  1.2,
  0,
  1.2,
  1,
  1.2,
  0.1,
  1,
  1,
  0,
  0
})
AI.AddCombatClass(AICombatClasses.Tank, {
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  0,
  1,
  0,
  1,
  1,
  1,
  1,
  1,
  1,
  0,
  1
}, "OnTankSeen")
AI.AddCombatClass(AICombatClasses.TankHi, {
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  0,
  1,
  0,
  1,
  1,
  1,
  1,
  1,
  1,
  0,
  1
}, "OnTankSeen")
AI.AddCombatClass(AICombatClasses.Heli, {
  1,
  1,
  1,
  1,
  0.9,
  0.9,
  1,
  1,
  1,
  0,
  1,
  0,
  0.9,
  1,
  0.9,
  1,
  1,
  1,
  0,
  1
}, "OnHeliSeen")
AI.AddCombatClass(AICombatClasses.VehicleGunner, {
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  0,
  1,
  0,
  1,
  1,
  1,
  1,
  1,
  1,
  0,
  1
})
AI.AddCombatClass(AICombatClasses.Hunter, {
  1,
  1.2,
  0.8,
  1,
  2,
  2,
  2,
  1,
  2,
  0,
  1,
  0,
  2,
  2,
  2,
  0.8,
  1.5,
  0,
  0,
  0
})
AI.AddCombatClass(AICombatClasses.Civilian, {
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  0,
  0.5,
  0,
  1,
  1,
  1,
  0.1,
  1,
  0,
  0,
  0
})
AI.AddCombatClass(AICombatClasses.Car, {
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0
})
AI.AddCombatClass(AICombatClasses.Warrior, {
  0.05,
  0.2,
  0.05,
  0.2,
  0.2,
  1.5,
  2,
  1.5,
  0.05,
  0.05,
  1.5,
  1.5,
  1.5,
  0.2,
  1.5,
  0.05,
  0.05,
  0,
  0,
  0
})
AI.AddCombatClass(AICombatClasses.AAA, {
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  0,
  1,
  0,
  1,
  1,
  1,
  1,
  1,
  1,
  0,
  1
}, "OnTankSeen")
AI.AddCombatClass(AICombatClasses.BOAT, {
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  0,
  1,
  0,
  1,
  1,
  1,
  1,
  1,
  1,
  0,
  1
}, "OnBoatSeen")
AI.AddCombatClass(AICombatClasses.APC, {
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  1,
  0,
  1,
  0,
  1,
  1,
  1,
  1,
  1,
  1,
  0,
  1
}, "OnTankSeen")
AI.AddCombatClass(AICombatClasses.Squadmate, {
  1,
  1,
  1,
  1,
  1.1,
  1.1,
  1,
  1,
  2,
  0,
  0,
  0,
  1,
  1,
  1,
  1,
  1,
  0,
  0,
  0
})
AI.AddCombatClass(AICombatClasses.Scout, {
  1,
  1,
  0.7,
  0.7,
  0.9,
  0.9,
  1,
  0.7,
  1,
  0,
  1,
  0,
  0.9,
  0.9,
  0.9,
  0.7,
  1,
  1,
  0,
  0
})
AI.AddCombatClass(AICombatClasses.ascensionScout, {
  1,
  1,
  0,
  0.5,
  0.5,
  0,
  1,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0
})
AI.AddCombatClass(AICombatClasses.ascensionVTOL, {
  1,
  1,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0
})
AI.AddCombatClass(AICombatClasses.ascensionScout2, {
  1,
  1,
  0,
  0.7,
  0.7,
  0,
  1,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0
})
function AIReset()
  AISideSelector:Reset()
  AIBlackBoard_Reset()
end
function AI:OnSave(save)
  save.AIBlackBoard = AIBlackBoard
  save.AISideSelector = AISideSelector
end
function AI:OnLoad(saved)
  AIBlackBoard_Reset()
  AISideSelector:Reset()
  merge(AIBlackBoard, saved.AIBlackBoard)
  merge(AISideSelector, saved.AISideSelector)
end
AI.LogEvent("[AISSYSTEM] CONFIG SCRIPT FILE LOADED. --------------------------")
