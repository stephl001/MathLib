// include Fake lib
#r "packages/FAKE/tools/FakeLib.dll"
open Fake
open Fake.Testing

RestorePackages()

// Properties
let testDir = @"./MathLibTests/bin/debug/"
let artifactsDir = @".\artifacts\"
let artifactsBuildDir = "./artifacts/build/"

let buildMode = getBuildParamOrDefault "buildMode" "Release"
let setParams defaults =
        { defaults with
            Verbosity = Some(Quiet)
            Targets = ["Build"]
            Properties =
                [
                    "Optimize", "True"
                    "DebugSymbols", "True"
                    "Configuration", buildMode
                ]
         }

// Targets
Target "Clean" (fun _ ->
    trace "Cleanup..."
    CleanDirs [artifactsDir]
)

Target "BuildApp" (fun _ ->
   trace "Building App..."
   build setParams "MathLib.sln"
)

Target "Default" (fun _ ->
    trace "Default Target invoked."
)

Target "Test" (fun _ ->
    !! (testDir @@ "*Tests.dll")
    |> xUnit2 (fun p -> { p with HtmlOutputPath = Some (testDir @@ "xunit.html") })
)

// Dependencies
"Clean"
  ==> "BuildApp"
  ==> "Test"
  ==> "Default"

// start build
RunTargetOrDefault "Default"