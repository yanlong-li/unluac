function EXPECT(expr)
  if expr then
    Test:LogPass()
  else
    local info
    local depth = 2
    repeat
      info = debug.getinfo(depth, "lS")
      depth = depth + 1
    until not info.source:find("unit_test_lib.lua")
    Test:LogFail(info.source, info.currentline)
  end
end
function EXPECT_NOT(expr)
  EXPECT(not expr)
end
function LOG(str)
  Test:Log("  " .. str)
end
function RunAllTests()
  for testName, test in pairs(_G) do
    if type(test) == "function" and type(testName) == "string" and testName:find("^test_") then
      Test:Log("Testing " .. testName:gsub("^test_", ""))
      test()
    end
  end
end
