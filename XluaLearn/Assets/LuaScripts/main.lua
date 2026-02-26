print("helloworld")
main = {}
local GameApp = require("Game.GameApp")

local function init()
    print("init")

    --进入游戏逻辑
    GameApp.EnterGAme()
end
main.init = init

local function update()
    print("update")
end
main.update = update

local function fixedUpdate()
    print("fixedUpdate")
end
main.fixedUpdate = fixedUpdate

local function lateUpdate()
    print("lateUpdate")
end
main.lateUpdate = lateUpdate