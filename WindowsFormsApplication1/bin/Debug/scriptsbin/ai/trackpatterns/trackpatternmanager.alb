TrackPatternManager = {}
function TrackPatternManager:OnInit()
  local patSize = 10
  AI.BeginTrackPattern("test2", AITRACKPAT_VALIDATE_SWEPTSPHERE, 1, 2)
  AI.AddPatternNode("root", 0, 0, -0.5, AITRACKPAT_NODE_ABSOLUTE)
  AI.AddPatternNode("pt1", 0, 0.6666667 * patSize, 0, AITRACKPAT_NODE_START, "root")
  AI.AddPatternNode("pt2", 0.6666667 * patSize, -0.33333334 * patSize, 0, AITRACKPAT_NODE_START, "root")
  AI.AddPatternNode("pt3", -0.6666667 * patSize, -0.33333334 * patSize, 0, AITRACKPAT_NODE_START, "root")
  AI.AddPatternBranch("pt1", AITRACKPAT_CHOOSE_LESS_DEFORMED, "pt2", "pt3")
  AI.AddPatternBranch("pt2", AITRACKPAT_CHOOSE_LESS_DEFORMED, "pt3", "pt1")
  AI.AddPatternBranch("pt3", AITRACKPAT_CHOOSE_LESS_DEFORMED, "pt1", "pt2")
  AI.EndTrackPattern()
  local innerSize = 2.5
  local midSize = 10
  local outerSize = 25
  AI.BeginTrackPattern("test", AITRACKPAT_VALIDATE_SWEPTSPHERE + AITRACKPAT_ALIGN_LEVEL_TO_TARGET, 2, 3, 0.35, 0.4, 0, 0.1, 0.5)
  AI.AddPatternNode("root", 0, 0, -0.5, AITRACKPAT_NODE_ABSOLUTE)
  AI.AddPatternNode("b1_p1", 0 * innerSize, 1 * innerSize, 0, AITRACKPAT_NODE_ABSOLUTE + AITRACKPAT_NODE_SIGNAL, "root", 0)
  AI.AddPatternNode("b1_p2", 0.866025 * midSize, 0.5 * midSize, 0, 0, "b1_p1")
  AI.AddPatternNode("b1_p3", 0.866025 * outerSize, 0.5 * outerSize, 0, AITRACKPAT_NODE_ABSOLUTE + AITRACKPAT_NODE_STOP + AITRACKPAT_NODE_SIGNAL, "b1_p2", 1)
  AI.AddPatternNode("b1_p4", 0.866025 * midSize, 0.5 * midSize, 0, AITRACKPAT_NODE_START, "b2_p1")
  AI.AddPatternNode("b2_p1", 0.866025 * innerSize, -0.5 * innerSize, 0, AITRACKPAT_NODE_ABSOLUTE + AITRACKPAT_NODE_SIGNAL, "root", 0)
  AI.AddPatternNode("b2_p2", 0 * midSize, -1 * midSize, 0, 0, "b2_p1")
  AI.AddPatternNode("b2_p3", 0 * outerSize, -1 * outerSize, 0, AITRACKPAT_NODE_ABSOLUTE + AITRACKPAT_NODE_STOP + AITRACKPAT_NODE_SIGNAL, "b2_p2", 2)
  AI.AddPatternNode("b2_p4", 0 * midSize, -1 * midSize, 0, AITRACKPAT_NODE_START, "b3_p1")
  AI.AddPatternNode("b3_p1", -0.866025 * innerSize, -0.5 * innerSize, 0, AITRACKPAT_NODE_ABSOLUTE + AITRACKPAT_NODE_SIGNAL, "root", 0)
  AI.AddPatternNode("b3_p2", -0.866025 * midSize, 0.5 * midSize, 0, 0, "b3_p1")
  AI.AddPatternNode("b3_p3", -0.866025 * outerSize, 0.5 * outerSize, 0, AITRACKPAT_NODE_ABSOLUTE + AITRACKPAT_NODE_STOP + AITRACKPAT_NODE_SIGNAL, "b3_p2", 3)
  AI.AddPatternNode("b3_p4", -0.866025 * midSize, 0.5 * midSize, 0, AITRACKPAT_NODE_START, "b1_p1")
  AI.AddPatternBranch("b1_p1", AITRACKPAT_CHOOSE_LESS_DEFORMED, "b1_p2", "b2_p1")
  AI.AddPatternBranch("b1_p2", AITRACKPAT_CHOOSE_ALWAYS, "b1_p3")
  AI.AddPatternBranch("b1_p3", AITRACKPAT_CHOOSE_ALWAYS, "b1_p4")
  AI.AddPatternBranch("b1_p4", AITRACKPAT_CHOOSE_ALWAYS, "b2_p1")
  AI.AddPatternBranch("b2_p1", AITRACKPAT_CHOOSE_LESS_DEFORMED, "b2_p2", "b3_p1")
  AI.AddPatternBranch("b2_p2", AITRACKPAT_CHOOSE_ALWAYS, "b2_p3")
  AI.AddPatternBranch("b2_p3", AITRACKPAT_CHOOSE_ALWAYS, "b2_p4")
  AI.AddPatternBranch("b2_p4", AITRACKPAT_CHOOSE_ALWAYS, "b3_p1")
  AI.AddPatternBranch("b3_p1", AITRACKPAT_CHOOSE_LESS_DEFORMED, "b3_p2", "b1_p1")
  AI.AddPatternBranch("b3_p2", AITRACKPAT_CHOOSE_ALWAYS, "b3_p3")
  AI.AddPatternBranch("b3_p3", AITRACKPAT_CHOOSE_ALWAYS, "b3_p4")
  AI.AddPatternBranch("b3_p4", AITRACKPAT_CHOOSE_ALWAYS, "b1_p1")
  AI.EndTrackPattern()
  for aa = 1, 2 do
    AI.BeginTrackPattern("melee_circle" .. aa, AITRACKPAT_VALIDATE_SWEPTSPHERE + AITRACKPAT_ALIGN_RANDOM, 0.5, 2, 0.35, 0.4, 0, 0.1, -1, {
      x = 5,
      y = 5,
      z = 90
    })
    local h = 1.5
    if aa == 2 then
      h = -h
    end
    AI.AddPatternNode("root", 0, 0, h, AITRACKPAT_NODE_ABSOLUTE)
    local r = 7
    local pts = 9
    for i = 1, pts do
      local a = i / pts * math.pi * 2
      local x = math.sin(a)
      AI.AddPatternNode("p" .. i, math.sin(a) * r, math.cos(a) * r, h, AITRACKPAT_NODE_ABSOLUTE + AITRACKPAT_NODE_SIGNAL + AITRACKPAT_NODE_START, "root", i)
    end
    for i = 1, pts do
      local n = i + 1
      local p = i - 1
      if pts < n then
        n = 1
      end
      if p < 1 then
        p = pts
      end
      AI.AddPatternBranch("p" .. i, AITRACKPAT_CHOOSE_MOST_EXPOSED, "p" .. n)
      AI.AddPatternBranch("p" .. i, AITRACKPAT_CHOOSE_MOST_EXPOSED, "p" .. p)
    end
    AI.EndTrackPattern()
  end
  AI.BeginTrackPattern("test_buffa_attack", AITRACKPAT_VALIDATE_SWEPTSPHERE + AITRACKPAT_ALIGN_TARGET_DIR, 0.5, 2, 0.35, 0.4, 0, 0.1, 1)
  AI.AddPatternNode("p1", 0, -3, -2, AITRACKPAT_NODE_ABSOLUTE + AITRACKPAT_NODE_START + AITRACKPAT_NODE_SIGNAL, "", 1001)
  AI.AddPatternNode("p2", 0, 0, -5, AITRACKPAT_NODE_ABSOLUTE)
  AI.AddPatternNode("p3", 0, 4, -5, AITRACKPAT_NODE_ABSOLUTE)
  AI.AddPatternNode("p4", 0, 5, 1.5, AITRACKPAT_NODE_ABSOLUTE + AITRACKPAT_NODE_SIGNAL, "", 1000)
  AI.AddPatternBranch("p1", AITRACKPAT_CHOOSE_ALWAYS, "p2")
  AI.AddPatternBranch("p2", AITRACKPAT_CHOOSE_ALWAYS, "p3")
  AI.AddPatternBranch("p3", AITRACKPAT_CHOOSE_ALWAYS, "p4")
  AI.EndTrackPattern()
  AI.BeginTrackPattern("test_buffa_attack2", AITRACKPAT_VALIDATE_SWEPTSPHERE + AITRACKPAT_ALIGN_TARGET_DIR, 0.5, 2, 0.35, 0.4, 0, 0.1, 1)
  AI.AddPatternNode("p1", 0, 0, 4, AITRACKPAT_NODE_ABSOLUTE + AITRACKPAT_NODE_START)
  AI.AddPatternNode("p2", 1, 4, 4, AITRACKPAT_NODE_ABSOLUTE)
  AI.AddPatternNode("p3", 0, 4.5, -1, AITRACKPAT_NODE_ABSOLUTE + AITRACKPAT_NODE_SIGNAL, "", 1000)
  AI.AddPatternBranch("p1", AITRACKPAT_CHOOSE_ALWAYS, "p2")
  AI.AddPatternBranch("p2", AITRACKPAT_CHOOSE_ALWAYS, "p3")
  AI.EndTrackPattern()
  AI.BeginTrackPattern("melee_circle_flyby1", AITRACKPAT_VALIDATE_SWEPTSPHERE + AITRACKPAT_ALIGN_TARGET_DIR + AITRACKPAT_ALIGN_RANDOM, 0.5, 2, 0.35, 0.4, 0, 0.1, 1, {
    x = 0,
    y = 60,
    z = 0
  })
  AI.AddPatternNode("p0", 0, -5, 1, AITRACKPAT_NODE_ABSOLUTE + AITRACKPAT_NODE_START + AITRACKPAT_NODE_SIGNAL, "", 1001)
  AI.AddPatternNode("p1", 0, -2, -5, AITRACKPAT_NODE_ABSOLUTE + AITRACKPAT_NODE_START + AITRACKPAT_NODE_SIGNAL, "", 1001)
  AI.AddPatternNode("p2", 0, 4, -5, AITRACKPAT_NODE_ABSOLUTE)
  AI.AddPatternNode("p3", 0, 5, -1, AITRACKPAT_NODE_ABSOLUTE)
  AI.AddPatternNode("p4", 0, 5, 1, AITRACKPAT_NODE_ABSOLUTE)
  AI.AddPatternNode("p5", 0, 4, 5, AITRACKPAT_NODE_ABSOLUTE)
  AI.AddPatternNode("p6", 0, -2, 5, AITRACKPAT_NODE_ABSOLUTE + AITRACKPAT_NODE_SIGNAL, "", 1002)
  AI.AddPatternBranch("p0", AITRACKPAT_CHOOSE_ALWAYS, "p1")
  AI.AddPatternBranch("p1", AITRACKPAT_CHOOSE_ALWAYS, "p2")
  AI.AddPatternBranch("p2", AITRACKPAT_CHOOSE_ALWAYS, "p3")
  AI.AddPatternBranch("p3", AITRACKPAT_CHOOSE_ALWAYS, "p4")
  AI.AddPatternBranch("p4", AITRACKPAT_CHOOSE_ALWAYS, "p5")
  AI.AddPatternBranch("p5", AITRACKPAT_CHOOSE_ALWAYS, "p6")
  AI.EndTrackPattern()
  AI.LogEvent("TRACKPATTERNS LOADED")
end
